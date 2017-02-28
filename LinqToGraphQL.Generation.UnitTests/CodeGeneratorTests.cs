using System;
using LinqToGraphQL.Generation.Models;
using LinqToGraphQL.Introspection;
using Xunit;

namespace LinqToGraphQL.Generation.UnitTests
{
    public class CodeGeneratorTests
    {
        [Fact]
        public void Generates_QueryEntity_Class()
        {
            var expected = @"using System.Linq;
using System.Linq.Expressions;
using LinqToGraphQL;
using LinqToGraphQL.Builders;

namespace Test
{
    public class Entity : QueryEntity
    {
        public Entity(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public Other NullableField => this.CreateProperty(x => x.NullableField, Other.Create);
    }
}";
            var model = new TypeModel
            {
                Name = "Entity",
                Kind = TypeKind.Object,
                Fields = new[]
                {
                    new FieldModel
                    {
                        Name = "NullableField",
                        Type = new TypeModel
                        {
                            Kind = TypeKind.Object,
                            Name = "Other",
                        },
                    }
                }
            };

            var compilation = CodeGenerator.GenerateType(model, "Test");
            var formatted = CodeGenerator.Format(compilation);

            Assert.Equal(expected, formatted);
        }
    }
}
