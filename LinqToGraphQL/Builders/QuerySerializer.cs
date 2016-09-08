using System;
using System.Linq;
using System.Text;
using GraphQLParser.AST;

namespace LinqToGraphQL.Builders
{
    public class QuerySerializer
    {
        public string Serialize(GraphQLOperationDefinition operation)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("query ").Append(operation.Name.Value);
            Serialize(operation.SelectionSet, builder);
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
            builder.Append('{');

            foreach (var field in selectionSet.Selections.OfType<GraphQLFieldSelection>())
            {
                AppendSeparator(builder);
                Serialize(field, builder);
            }

            builder.Append('}');
        }

        private void AppendSeparator(StringBuilder builder)
        {
            if (builder.Length > 0)
            {
                var last = builder.ToString().Last();

                if (last != ' ' && last != '{' && last != '}')
                {
                    builder.Append(' ');
                }
            }
        }
    }
}
