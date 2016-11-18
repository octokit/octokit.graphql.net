using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LinqToGraphQL.Syntax;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LinqToGraphQL.Deserializers
{
    public class ResponseDeserializer
    {
        public object Deserialize(OperationDefinition operation, string data)
        {
            return Deserialize(operation, JObject.Parse(data));
        }

        public object Deserialize(OperationDefinition operation, JObject data)
        {
            var root = data["data"];
            return DeserializeSelectionSet(operation, root).Single();
        }

        private IEnumerable<object> DeserializeSelectionSet(ISelectionSet set, JToken data)
        {
            foreach (var s in set.Selections)
            {
                var field = s as FieldSelection;

                if (field != null)
                {
                    var resultField = data[field.Name];

                    if (field.ResultConstructor != null)
                    {
                        var args = DeserializeSelectionSet(field, resultField).ToArray();
                        PrepareArgs(field.ResultConstructor.GetParameters(), args);
                        yield return field.ResultConstructor.Invoke(args);
                    }
                    else if (field.ResultType != null)
                    {
                        var queryableType = GetQueryableType(field.ResultType);
                        var resultArray = resultField as JArray;

                        if (queryableType == null)
                        {
                            yield return resultField.ToObject(field.ResultType);
                        }
                        else if (queryableType != null && resultArray != null)
                        {
                            var itemType = field.ResultType.GetGenericArguments()[0];
                            var arrayType = itemType.MakeArrayType();
                            yield return resultField.ToObject(arrayType);
                        }
                        else
                        {
                            var result = resultField.ToObject(queryableType);
                            yield return ConstructQueryable(queryableType, result);
                        }
                    }
                    else
                    {
                        yield return DeserializeSelectionSet(field, resultField).Single();
                    }
                }
            }
        }

        private static Type GetQueryableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IQueryable<>) ?
                type.GetGenericArguments()[0] : null;
        }

        private object ConstructQueryable(Type queryableType, object item)
        {
            var enumerableType = typeof(IEnumerable<>).MakeGenericType(queryableType);
            var asQueryable = typeof(Queryable).GetMethod(nameof(Queryable.AsQueryable), new[] { enumerableType });
            var array = Array.CreateInstance(queryableType, 1);
            array.SetValue(item, 0);
            return asQueryable.Invoke(null, new[] { array });
        }

        private void PrepareArgs(ParameterInfo[] parameters, object[] args)
        {
            if (parameters.Length != args.Length)
            {
                throw new Exception("Wrong number of parameters.");
            }

            for (var i = 0; i < parameters.Length; ++i)
            {
                var p = parameters[i];
                var queryableType = GetQueryableType(p.ParameterType);

                if (queryableType == args[i].GetType())
                {
                    args[i] = ConstructQueryable(queryableType, args[i]);
                }
            }
        }
    }
}
