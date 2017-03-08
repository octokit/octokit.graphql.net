using System;
using LinqToGraphQL.Generation.Models;
using LinqToGraphQL.Introspection;
using Xunit;

namespace LinqToGraphQL.Generation.UnitTests
{
    public class InterfaceGenerationTests
    {
        const string MemberTemplate = @"using System.Linq;
using System.Linq.Expressions;
using LinqToGraphQL;
using LinqToGraphQL.Builders;

namespace Test
{{
    public interface IEntity
    {{
        {0}
    }}
}}";

        [Fact]
        public void Generates_Property_For_Scalar_Field()
        {
            var expected = string.Format(
                MemberTemplate,
                "int? Foo { get; }");

            var model = new TypeModel
            {
                Name = "Entity",
                Kind = TypeKind.Interface,
                Fields = new[]
                {
                    new FieldModel
                    {
                        Name = "foo",
                        Type = TypeModel.Int()
                    },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Property_For_NonNull_Scalar_Field()
        {
            var expected = string.Format(
                MemberTemplate,
                "int FooBar { get; }");

            var model = new TypeModel
            {
                Name = "Entity",
                Kind = TypeKind.Interface,
                Fields = new[]
                {
                    new FieldModel
                    {
                        Name = "fooBar",
                        Type = TypeModel.NonNull(TypeModel.Int()),
                    },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Property_For_Object_Field()
        {
            var expected = string.Format(
                MemberTemplate,
                "Other Foo { get; }");

            var model = new TypeModel
            {
                Name = "Entity",
                Kind = TypeKind.Interface,
                Fields = new[]
                {
                    new FieldModel
                    {
                        Name = "foo",
                        Type = TypeModel.Object("Other")
                    },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Property_For_NonNull_Object_Field()
        {
            var expected = string.Format(
                MemberTemplate,
                "Other Foo { get; }");

            var model = new TypeModel
            {
                Name = "Entity",
                Kind = TypeKind.Interface,
                Fields = new[]
                {
                    new FieldModel
                    {
                        Name = "foo",
                        Type = TypeModel.NonNull(TypeModel.Object("Other")),
                    },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Property_For_List_Field()
        {
            var expected = string.Format(
                MemberTemplate,
                "IQueryable<Other> Foo { get; }");

            var model = new TypeModel
            {
                Name = "Entity",
                Kind = TypeKind.Interface,
                Fields = new[]
                {
                    new FieldModel
                    {
                        Name = "foo",
                        Type = TypeModel.List(TypeModel.Object("Other")),
                    },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Method_For_Object_Field_With_Args()
        {
            var expected = string.Format(
                MemberTemplate,
                "Other Foo(int bar);");

            var model = new TypeModel
            {
                Name = "Entity",
                Kind = TypeKind.Interface,
                Fields = new[]
                {
                    new FieldModel
                    {
                        Name = "foo",
                        Type = TypeModel.Object("Other"),
                        Args = new[]
                        {
                            new InputValueModel
                            {
                                Name = "bar",
                                Type = TypeModel.NonNull(TypeModel.Int()),
                            }
                        }
                    },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Method_For_NonNull_Object_Field_With_Args()
        {
            var expected = string.Format(
                MemberTemplate,
                "Other Foo(int bar);");

            var model = new TypeModel
            {
                Name = "Entity",
                Kind = TypeKind.Interface,
                Fields = new[]
                {
                    new FieldModel
                    {
                        Name = "foo",
                        Type = TypeModel.NonNull(TypeModel.Object("Other")),
                        Args = new[]
                        {
                            new InputValueModel
                            {
                                Name = "bar",
                                Type = TypeModel.NonNull(TypeModel.Int()),
                            }
                        }
                    },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Method_For_List_Field_With_Args()
        {
            var expected = string.Format(
                MemberTemplate,
                "IQueryable<Other> Foo(int? bar);");

            var model = new TypeModel
            {
                Name = "Entity",
                Kind = TypeKind.Interface,
                Fields = new[]
                {
                    new FieldModel
                    {
                        Name = "foo",
                        Type = TypeModel.List(TypeModel.Object("Other")),
                        Args = new[]
                        {
                            new InputValueModel
                            {
                                Name = "bar",
                                Type = TypeModel.Int(),
                            }
                        }
                    },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Method_For_Scalar()
        {
            var expected = string.Format(
                MemberTemplate,
                "IQueryable<int> Foo(int? bar);");

            var model = new TypeModel
            {
                Name = "Entity",
                Kind = TypeKind.Interface,
                Fields = new[]
                {
                    new FieldModel
                    {
                        Name = "foo",
                        Type = TypeModel.Int(),
                        Args = new[]
                        {
                            new InputValueModel
                            {
                                Name = "bar",
                                Type = TypeModel.Int(),
                            }
                        }
                    },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Method_For_NonNull_Scalar()
        {
            var expected = string.Format(
                MemberTemplate,
                "IQueryable<int> Foo(int? bar);");

            var model = new TypeModel
            {
                Name = "Entity",
                Kind = TypeKind.Interface,
                Fields = new[]
                {
                    new FieldModel
                    {
                        Name = "foo",
                        Type = TypeModel.NonNull(TypeModel.Int()),
                        Args = new[]
                        {
                            new InputValueModel
                            {
                                Name = "bar",
                                Type = TypeModel.Int(),
                            }
                        }
                    },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Doc_Comments_For_Class()
        {
            var expected = @"using System.Linq;
using System.Linq.Expressions;
using LinqToGraphQL;
using LinqToGraphQL.Builders;

namespace Test
{
    /// <summary>
    /// Testing if doc comments are generated.
    /// </summary>
    public class Entity : QueryEntity
    {
        public Entity(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        internal static Entity Create(IQueryProvider provider, Expression expression)
        {
            return new Entity(provider, expression);
        }
    }
}";

            var model = new TypeModel
            {
                Name = "Entity",
                Description = "Testing if doc comments are generated.",
                Kind = TypeKind.Object,
                Fields = new FieldModel[0],
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Doc_Comments_For_Property()
        {
            var expected = string.Format(
                MemberTemplate,
                @"/// <summary>
        /// Testing if doc comments are generated.
        /// </summary>
        Other Foo { get; }");

            var model = new TypeModel
            {
                Name = "Entity",
                Kind = TypeKind.Interface,
                Fields = new[]
                {
                    new FieldModel
                    {
                        Name = "Foo",
                        Description = "Testing if doc comments are generated.",
                        Type = TypeModel.Object("Other")
                    },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Doc_Comments_For_Method()
        {
            var expected = string.Format(
                MemberTemplate,
                @"/// <summary>
        /// Testing if doc comments are generated.
        /// </summary>
        /// <param name=""arg1"">The first argument.</param>
        Other Foo(int? arg1, int? arg2);");

            var model = new TypeModel
            {
                Name = "Entity",
                Kind = TypeKind.Interface,
                Fields = new[]
                {
                    new FieldModel
                    {
                        Name = "foo",
                        Description = "Testing if doc comments are generated.",
                        Type = TypeModel.Object("Other"),
                        Args = new[]
                        {
                            new InputValueModel
                            {
                                Name = "arg1",
                                Description = "The first argument.",
                                Type = TypeModel.Int(),
                            },
                            new InputValueModel
                            {
                                Name = "arg2",
                                Type = TypeModel.Int(),
                            },
                        }
                    },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }
    }
}
