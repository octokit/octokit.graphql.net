using System;
using System.Linq;
using System.Text;
using LinqToGraphQL.Syntax;

namespace LinqToGraphQL.Builders
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
            builder.Append("query ").Append(operation.Name);
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
            builder.Append(field.Name);

            if (field.Arguments?.Any() == true)
            {
                builder.Append('(');

                var first = true;
                foreach (var arg in field.Arguments)
                {
                    if (!first) builder.Append(comma);
                    builder.Append(arg.Name).Append(colon).Append(SerializeValue(arg.Value));
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

        private string SerializeValue(object value)
        {
            if (value is string)
            {
                return '"' + ((string)value) + '"';
            }
            else if (value is Enum)
            {
                return value.ToString().ToUpperInvariant();
            }
            else
            {
                return value.ToString();
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
                builder.Append(" {\r\n");
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
