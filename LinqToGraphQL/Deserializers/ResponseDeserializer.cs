using System;
using System.Collections.Generic;
using System.Linq;
using LinqToGraphQL.Syntax;
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

                    if (field.ResultType != null)
                    {
                        yield return resultField.ToObject(field.ResultType);
                    }
                    else if (field.ResultConstructor != null)
                    {
                        var args = DeserializeSelectionSet(field, resultField).ToArray();
                        yield return field.ResultConstructor.Invoke(args);
                    }
                    else
                    {
                        yield return DeserializeSelectionSet(field, resultField).Single();
                    }
                }
            }
        }
    }
}
