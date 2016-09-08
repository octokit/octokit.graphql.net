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

                foreach (var arg in field.Arguments)
                {
                    builder.Append(arg.Name.Value).Append(':').Append(arg.Value);
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
                Serialize(field, builder);
            }

            builder.Append('}');
        }
    }
}
