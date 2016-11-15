using System;
using System.Linq;
using System.Text;
using GraphQLParser.AST;

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

        public string Serialize(GraphQLFieldSelection field)
        {
            StringBuilder builder = new StringBuilder();
            Serialize(field, builder);
            return builder.ToString();
        }

        public string Serialize(GraphQLOperationDefinition operation)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("query ").Append(operation.Name.Value);
            Serialize(operation.SelectionSet, builder);
            return builder.ToString();
        }

        public string Serialize(GraphQLSelectionSet selectionSet)
        {
            StringBuilder builder = new StringBuilder();
            Serialize(selectionSet, builder);
            return builder.ToString();
        }

        private void Serialize(GraphQLFieldSelection field, StringBuilder builder)
        {
            builder.Append(field.Name.Value);

            if (field.Arguments?.Any() == true)
            {
                builder.Append('(');

                var first = true;
                foreach (var arg in field.Arguments)
                {
                    if (!first) builder.Append(comma);
                    builder.Append(arg.Name.Value).Append(colon).Append(arg.Value);
                    first = false;
                }

                builder.Append(')');
            }

            if (field.SelectionSet?.Selections?.Any() == true)
            {
                Serialize(field.SelectionSet, builder);
            }
        }

        private void Serialize(GraphQLInlineFragment fragment, StringBuilder builder)
        {
            builder.Append("... on ");
            builder.Append(fragment.TypeCondition.Name.Value);

            if (fragment.SelectionSet?.Selections?.Any() == true)
            {
                Serialize(fragment.SelectionSet, builder);
            }
        }

        private void Serialize(GraphQLSelectionSet selectionSet, StringBuilder builder)
        {
            OpenBrace(builder);

            bool first = true;

            if (selectionSet.Selections != null)
            {
                foreach (var s in selectionSet.Selections)
                {
                    if (!first) Separator(builder);

                    var field = s as GraphQLFieldSelection;
                    var fragment = s as GraphQLInlineFragment;

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
