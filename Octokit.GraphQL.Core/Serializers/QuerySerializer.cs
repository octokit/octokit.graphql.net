using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Octokit.GraphQL.Core.Syntax;
using Octokit.GraphQL.Core.Utilities;

namespace Octokit.GraphQL.Core.Serializers
{
    public class QuerySerializer
    {
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
            else if (value is IQueryable)
            {
                throw new NotImplementedException();
            }
            else
            {
                var propertyInfos = value.GetType().GetRuntimeProperties()
                    .Where(info => info.GetMethod.IsPublic);

                using (var enumerator = propertyInfos.GetEnumerator())
                {
                    var i = 0;

                    while (enumerator.MoveNext())
                    {
                        var publicProperty = enumerator.Current;

                        if (i == 0)
                        {
                            OpenBrace(builder);
                        }
                        else
                        {
                            builder.Append(",");
                        }

                        builder.Append(publicProperty.Name.LowerFirstCharacter()).Append(colon);
                        SerializeValue(builder, publicProperty.GetMethod.Invoke(value, null));
                        i++;
                    }

                    if (i > 0)
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
