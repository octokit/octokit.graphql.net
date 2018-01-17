using System;
using Octokit.GraphQL.Core.Generation.Models;
using Octokit.GraphQL.Core.Introspection;
using Xunit;

namespace Octokit.GraphQL.Core.Generation.UnitTests
{
    public class UnionGenerationTests
    {
        const string MemberTemplate = @"namespace Test
{{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    public class Union : QueryEntity, IUnion
    {{
        public Union(IQueryProvider provider, Expression expression) : base(provider, expression)
        {{
        }}
{0}
        internal static Union Create(IQueryProvider provider, Expression expression)
        {{
            return new Union(provider, expression);
        }}
    }}
}}";

        [Fact]
        public void Generates_Property_For_PossibleType()
        {
            var expected = FormatMemberTemplate("public Other Other => this.CreateProperty(x => x.Other, Test.Other.Create);");

            var model = new TypeModel
            {
                Name = "Union",
                Kind = TypeKind.Union,
                PossibleTypes = new[]
                {
                    TypeModel.Object("Other"),
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(new Tuple<string, string>(@"Model\Union.cs", expected), result);
        }

        [Fact]
        public void Generates_Doc_Comments_For_PossibleType()
        {
            var expected = FormatMemberTemplate(@"/// <summary>
        /// Testing if doc comments are generated.
        /// </summary>
        public Other Other => this.CreateProperty(x => x.Other, Test.Other.Create);");

            var model = new TypeModel
            {
                Name = "Union",
                Kind = TypeKind.Union,
                PossibleTypes = new[]
                {
                    new TypeModel
                    {
                        Kind = TypeKind.Object,
                        Name = "Other",
                        Description = "Testing if doc comments are generated.",
                    }
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(new Tuple<string, string>(@"Model\Union.cs", expected), result);
        }

        private string FormatMemberTemplate(string members, string interfaces = null)
        {
            if (members != null)
            {
                members = "\r\n        " + members + "\r\n";
            }

            return string.Format(MemberTemplate, members, interfaces);
        }
    }
}
