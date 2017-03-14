using System;
using LinqToGraphQL.Generation.Models;
using LinqToGraphQL.Introspection;
using Xunit;

namespace LinqToGraphQL.Generation.UnitTests
{
    public class InputObjectGenerationTests
    {
        const string MemberTemplate = @"namespace Test
{{
    using System.Linq;

    public class InputObject
    {{{0}
    }}
}}";
        [Fact]
        public void Generates_Property_For_Scalar_Field()
        {
            var expected = FormatMemberTemplate("public int? Foo { get; set; }");

            var model = new TypeModel
            {
                Name = "InputObject",
                Kind = TypeKind.InputObject,
                InputFields = new[]
                {
                    new InputValueModel
                    {
                        Name = "foo",
                        Type = TypeModel.Int()
                    },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        private string FormatMemberTemplate(string members)
        {
            if (members != null)
            {
                members = "\r\n        " + members;
            }

            return string.Format(MemberTemplate, members);
        }
    }
}
