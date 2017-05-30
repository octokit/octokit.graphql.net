using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Octokit.GraphQL.Core.Syntax;
using Octokit.GraphQL.Core.Utilities;

namespace Octokit.GraphQL.Core.Serializers
{
    public class QuerySerializer
    {
        private static readonly ConcurrentDictionary<Type, Tuple<string, MethodInfo>[]> typeCache = new ConcurrentDictionary<Type, Tuple<string, MethodInfo>[]>();

        private readonly int indentation;
        private readonly string comma = ",";
        private readonly string colon = ":";

        private int currentIndent;

        public QuerySerializer(int indentation = 0)
        {
            this.indentation = indentation;

            if (indentation > 0)
            {
                comma = ", ";
                colon = ": ";
            }
        }

        public string Serialize(FieldSelection field)
        {
            StringBuilder builder = new StringBuilder();
            Serialize(field, builder);
            return builder.ToString();
        }

        public string Serialize(OperationDefinition operation)
        {
            StringBuilder builder = new StringBuilder();

            if (operation.Type != OperationType.Query || operation.Name != null)
            {
                builder.Append("query ").Append(operation.Name);
            }

            SerializeSelections(operation, builder);
            return builder.ToString();
        }

        private string SerializeSelections(ISelectionSet selectionSet)
        {
            StringBuilder builder = new StringBuilder();
            SerializeSelections(selectionSet, builder);
            return builder.ToString();
        }

        private void Serialize(FieldSelection field, StringBuilder builder)
        {
            if (field.Alias != null)
            {
                builder.Append(field.Alias);
                builder.Append(": ");
            }

            builder.Append(field.Name);

            if (field.Arguments?.Any() == true)
            {
                builder.Append('(');

                var first = true;
                foreach (var arg in field.Arguments)
                {
                    if (!first) builder.Append(comma);
                    builder.Append(arg.Name).Append(colon);
                    SerializeValue(builder, arg.Value);
                    first = false;
                }

                builder.Append(')');
            }

            if (field.Selections.Any() == true)
            {
                SerializeSelections(field, builder);
            }
        }

        private void Serialize(InlineFragment fragment, StringBuilder builder)
        {
            builder.Append("... on ");
            builder.Append(fragment.TypeCondition.Name);

            if (fragment.Selections.Any() == true)
            {
                SerializeSelections(fragment, builder);
            }
        }

        private void SerializeSelections(ISelectionSet selectionSet, StringBuilder builder)
        {
            OpenBrace(builder);

            bool first = true;

            if (selectionSet.Selections != null)
            {
                foreach (var s in selectionSet.Selections)
                {
                    if (!first) Separator(builder);

                    var field = s as FieldSelection;
                    var fragment = s as InlineFragment;

                    if (field != null)
                    {
                        Serialize(field, builder);
                    }
                    else if (fragment != null)
                    {
                        Serialize(fragment, builder);
                    }

                    first = false;
                }
            }

            CloseBrace(builder);
        }

        private void SerializeValue(StringBuilder builder, object value)
        {
            if (value is string)
            {
                builder.Append('"' + ((string)value) + '"');
            }
            else if (value is Enum)
            {
                builder.Append(value.ToString().PascalCaseToSnakeCase());
            }
            else if (value is bool)
            {
                builder.Append((bool)value ? "true" : "false");
            }
            else if (value is int || value is float)
            {
                builder.Append(value);
            }
            else
            {
                var objectType = value.GetType();

                Tuple<string, MethodInfo>[] properties;
                if (!typeCache.TryGetValue(objectType, out properties))
                {
                    properties = objectType.GetRuntimeProperties()
                        .Where(info => info.GetMethod.IsPublic)
                        .Select(info => new Tuple<string, MethodInfo>(info.Name.LowerFirstCharacter(), info.GetMethod))
                        .ToArray();

                    typeCache.TryAdd(objectType, properties);
                }
                else
                {
                    //Cache Hit
                }

                for (var index = 0; index < properties.Length; index++)
                {
                    var property = properties[index];
                    
                    if (index == 0)
                    {
                        OpenBrace(builder);
                    }
                    else
                    {
                        builder.Append(",");
                    }

                    builder.Append(property.Item1.LowerFirstCharacter()).Append(colon);
                    SerializeValue(builder, property.Item2.Invoke(value, null));

                    if (index + 1 == properties.Length)
                    {
                        CloseBrace(builder);
                    }
                }
            }
        }

        private void OpenBrace(StringBuilder builder)
        {
            if (indentation == 0)
            {
                builder.Append('{');
            }
            else
            {
                if (builder.Length > 0)
                {
                    builder.Append(' ');
                }

                builder.Append("{\r\n");
                currentIndent += indentation;
                Indent(builder);
            }
        }

        private void CloseBrace(StringBuilder builder)
        {
            if (indentation == 0)
            {
                builder.Append('}');
            }
            else
            {
                currentIndent -= indentation;
                builder.Append("\r\n");
                Indent(builder);
                builder.Append('}');
            }
        }

        private void Separator(StringBuilder builder)
        {
            if (indentation == 0)
            {
                builder.Append(' ');
            }
            else
            {
                builder.Append("\r\n");
                Indent(builder);
            }
        }

        private void Indent(StringBuilder builder)
        {
            for (var i = 0; i < currentIndent; ++i)
            {
                builder.Append(' ');
            }
        }
    }
}
