using System;
using System.Linq;
using System.Text;
using GraphQLParser.AST;

namespace LinqToGraphQL.Builders
{
    public class QuerySerializer
    {
        private int indentation;
        private int currentIndent;

        public QuerySerializer(int indentation = 0)
        {
            this.indentation = indentation;
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

            if (field.Arguments?.Count() > 0)
            {
                builder.Append('(');

                var first = true;
                foreach (var arg in field.Arguments)
                {
                    if (!first) builder.Append(',');
                    builder.Append(arg.Name.Value).Append(':').Append(arg.Value);
                    first = false;
                }

                builder.Append(')');
            }

            if (field.SelectionSet != null)
            {
                Serialize(field.SelectionSet, builder);
            }
        }

        private void Serialize(GraphQLSelectionSet selectionSet, StringBuilder builder)
        {
            OpenBrace(builder);

            bool first = true;
            foreach (var field in selectionSet.Selections.OfType<GraphQLFieldSelection>())
            {
                if (!first) Separator(builder);
                Serialize(field, builder);
                first = false;
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
