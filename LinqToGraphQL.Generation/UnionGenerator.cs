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
            string rootNamespace)
        {
            var className = TypeUtilities.GetClassName(type);

            return $@"namespace {rootNamespace}
{{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    {GenerateUnionDocComments(type)}public class {className} : QueryEntity, IUnion
    {{
        public {className}(IQueryProvider provider, Expression expression) : base(provider, expression)
        {{
        }}{GeneratePossibleTypes(type, rootNamespace)}

        internal static {className} Create(IQueryProvider provider, Expression expression)
        {{
            return new {className}(provider, expression);
        }}
    }}
}}";
        }

        private static string GeneratePossibleTypes(TypeModel type, string rootNamespace)
        {
            var builder = new StringBuilder();

            if (type.PossibleTypes?.Count > 0)
            {
                var first = true;

                builder.AppendLine();

                foreach (var field in type.PossibleTypes)
                {
                    if (!first)
                    {
                        builder.AppendLine();
                    }

                    builder.AppendLine();
                    builder.Append(GenerateField(field, rootNamespace));

                    first = false;
                }
            }

            return builder.ToString();
        }

        private static string GenerateField(TypeModel possibleType, string rootNamespace)
        {
            var comments = GenerateDocComments(possibleType);
            var typeName = TypeUtilities.GetClassName(possibleType);
            var implName = rootNamespace + '.' + typeName;
            var name = possibleType.Name;
            return comments + $"        public {typeName} {name} => this.CreateProperty(x => x.{name}, {implName}.Create);";
        }

        private static string GenerateUnionDocComments(TypeModel type)
        {
            if (!string.IsNullOrWhiteSpace(type.Description))
            {
                return $@"/// <summary>
    /// {type.Description}
    /// </summary>
    ";
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
                return $@"        /// <summary>
        /// {possibleType.Description}
        /// </summary>
";
            }
            else
            {
                return null;
            }
        }
    }
}
