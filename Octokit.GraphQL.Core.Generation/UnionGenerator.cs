using System;
using System.Text;
using Octokit.GraphQL.Core.Generation.Models;
using Octokit.GraphQL.Core.Generation.Utilities;

namespace Octokit.GraphQL.Core.Generation
{
    internal static class UnionGenerator
    {
        public static string Generate(
            TypeModel type,
            string entityNamespace)
        {
            var className = TypeUtilities.GetClassName(type);

            return $@"namespace {entityNamespace}
{{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    {GenerateUnionDocComments(type)}public class {className} : QueryableValue<{className}>, IUnion
    {{
        public {className}(Expression expression) : base(expression)
        {{
        }}

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {{{GeneratePossibleTypes(type, entityNamespace)}
        }}

        internal static {className} Create(Expression expression)
        {{
            return new {className}(expression);
        }}
    }}
}}";
        }

        private static string GeneratePossibleTypes(TypeModel type, string entityNamespace)
        {
            var builder = new StringBuilder();

            if (type.PossibleTypes?.Count > 0)
            {
                var first = true;

                foreach (var field in type.PossibleTypes)
                {
                    if (!first)
                    {
                        builder.AppendLine();
                    }

                    builder.AppendLine();
                    builder.Append(GenerateField(field, entityNamespace));

                    first = false;
                }
            }

            return builder.ToString();
        }

        private static string GenerateField(TypeModel possibleType, string entityNamespace)
        {
            var comments = GenerateDocComments(possibleType);
            var typeName = TypeUtilities.GetClassName(possibleType);
            var implName = entityNamespace + '.' + typeName;
            var name = possibleType.Name;
            return comments + $"            public Selector<T> {name}(Func<{name}, T> selector) => default;";
        }

        private static string GenerateUnionDocComments(TypeModel type)
        {
            if (!string.IsNullOrWhiteSpace(type.Description))
            {
                var builder = new StringBuilder();
                DocCommentGenerator.GenerateSummary(type.Description, 4, builder);
                builder.Append("    ");
                return builder.ToString().TrimStart();
            }
            else
            {
                return null;
            }
        }

        private static string GenerateDocComments(TypeModel possibleType)
        {
            if (!string.IsNullOrWhiteSpace(possibleType.Description))
            {
                var builder = new StringBuilder();
                DocCommentGenerator.GenerateSummary(possibleType.Description, 12, builder);
                return builder.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
