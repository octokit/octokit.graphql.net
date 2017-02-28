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

        public Other NullableFieldProperty => this.CreateProperty(x => x.NullableFieldProperty, Other.Create);
        public Other NotNullFieldProperty => this.CreateProperty(x => x.NotNullFieldProperty, Other.Create);
        public IQueryable<Other> ListFieldProperty => this.CreateProperty(x => x.ListFieldProperty);
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
                        Name = "NullableFieldProperty",
                        Type = new TypeModel
                        {
                            Kind = TypeKind.Object,
                            Name = "Other",
                        },
                    },
                    new FieldModel
                    {
                        Name = "NotNullFieldProperty",
                        Type = new TypeModel
                        {
                            Kind = TypeKind.NonNull,
                            OfType = new TypeModel
                            {
                               Kind = TypeKind.Object,
                               Name = "Other",
                            }
                        },
                    },
                    new FieldModel
                    {
                        Name = "ListFieldProperty",
                        Type = new TypeModel
                        {
                            Kind = TypeKind.List,
                            OfType = new TypeModel
                            {
                               Kind = TypeKind.Object,
                               Name = "Other",
                            }
                        },
                    },
                }
            };

            var result = CodeGenerator.GenerateType(model, "Test");

            Assert.Equal(expected, result);
        }
    }
}
