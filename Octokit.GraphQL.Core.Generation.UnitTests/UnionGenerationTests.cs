using System;
using System.IO;
using Octokit.GraphQL.Core.Generation.Models;
using Octokit.GraphQL.Core.Introspection;
using Xunit;

namespace Octokit.GraphQL.Core.Generation.UnitTests
{
    public class UnionGenerationTests : TestBase
    {
        const string MemberTemplate = @"namespace Test
{{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    public class Union : QueryableValue<Union>, IUnion
    {{
        public Union(Expression expression) : base(expression)
        {{
        }}

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {{{0}
        }}

        internal static Union Create(Expression expression)
        {{
            return new Union(expression);
        }}
    }}
}}";

        [Fact]
        public void Generates_Property_For_PossibleType()
        {
            var expected = FormatMemberTemplate(@"public Selector<T> One(Func<One, T> selector) => default;

            public Selector<T> Another(Func<Another, T> selector) => default;");

            var model = new TypeModel
            {
                Name = "Union",
                Kind = TypeKind.Union,
                PossibleTypes = new[]
                {
                    TypeModel.Object("One"),
                    TypeModel.Object("Another"),
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            CompareModel("Union.cs", expected, result);
        }

        [Fact]
        public void Generates_Doc_Comments_For_PossibleType()
        {
            var expected = FormatMemberTemplate(@"/// <summary>
            /// Testing if doc comments are generated.
            /// </summary>
            public Selector<T> Other(Func<Other, T> selector) => default;

            public Selector<T> Another(Func<Another, T> selector) => default;");

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
                    },
                    TypeModel.Object("Another"),
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            CompareModel("Union.cs", expected, result);
        }

        private string FormatMemberTemplate(string members, string interfaces = null)
        {
            if (members != null)
            {
                members = "\r\n            " + members;
            }

            return string.Format(MemberTemplate, members, interfaces);
        }
    }
}
