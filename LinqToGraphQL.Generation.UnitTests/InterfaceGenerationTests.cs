using System;
using LinqToGraphQL.Generation.Models;
using LinqToGraphQL.Introspection;
using Xunit;

namespace LinqToGraphQL.Generation.UnitTests
{
    public class InterfaceGenerationTests
    {
        const string MemberTemplate = @"namespace Test
{{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    public interface IEntity
    {{
        {0}
    }}
}}

namespace Test.Internal
{{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    internal class StubIEntity : QueryEntity, IEntity
    {{
        public StubIEntity(IQueryProvider provider, Expression expression) : base(provider, expression)
        {{
        }}
{1}
        internal static StubIEntity Create(IQueryProvider provider, Expression expression)
        {{
            return new StubIEntity(provider, expression);
        }}
    }}
}}";

        [Fact]
        public void Generates_Property_For_Scalar_Field()
        {
            var expected = FormatMemberTemplate(
                "int? Foo { get; }",
                "public int? Foo { get; }");

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
            var expected = FormatMemberTemplate(
                "int FooBar { get; }",
                "public int FooBar { get; }");

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
        public void Generates_Property_For_NonNull_Enum_Field()
        {
            var expected = FormatMemberTemplate(
                "Baz FooBar { get; }",
                "public Baz FooBar { get; }");

            var model = new TypeModel
            {
                Name = "Entity",
                Kind = TypeKind.Interface,
                Fields = new[]
                {
                    new FieldModel
                    {
                        Name = "fooBar",
                        Type = TypeModel.NonNull(TypeModel.Enum("Baz")),
                    },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Property_For_Object_Field()
        {
            var expected = FormatMemberTemplate(
                "Other Foo { get; }",
                "public Other Foo => this.CreateProperty(x => x.Foo, Other.Create);");

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
            var expected = FormatMemberTemplate(
                "Other Foo { get; }",
                "public Other Foo => this.CreateProperty(x => x.Foo, Other.Create);");

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
            var expected = FormatMemberTemplate(
                "IQueryable<Other> Foo { get; }",
                "public IQueryable<Other> Foo => this.CreateProperty(x => x.Foo);");

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
            var expected = FormatMemberTemplate(
                "Other Foo(int bar);",
                "public Other Foo(int bar) => this.CreateMethodCall(x => x.Foo(bar), Other.Create);");

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
            var expected = FormatMemberTemplate(
                "Other Foo(int bar);",
                "public Other Foo(int bar) => this.CreateMethodCall(x => x.Foo(bar), Other.Create);");

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
            var expected = FormatMemberTemplate(
                "IQueryable<Other> Foo(int? bar);",
                "public IQueryable<Other> Foo(int? bar) => this.CreateMethodCall(x => x.Foo(bar));");

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
            var expected = FormatMemberTemplate(
                "IQueryable<int> Foo(int? bar);",
                "public IQueryable<int> Foo(int? bar) => this.CreateMethodCall(x => x.Foo(bar));");

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
            var expected = FormatMemberTemplate(
                "IQueryable<int> Foo(int? bar);",
                "public IQueryable<int> Foo(int? bar) => this.CreateMethodCall(x => x.Foo(bar));");

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
            var expected = @"namespace Test
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

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
            var expected = FormatMemberTemplate(
                @"/// <summary>
        /// Testing if doc comments are generated.
        /// </summary>
        Other Foo { get; }",
                "public Other Foo => this.CreateProperty(x => x.Foo, Other.Create);");

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
            var expected = FormatMemberTemplate(
                @"/// <summary>
        /// Testing if doc comments are generated.
        /// </summary>
        /// <param name=""arg1"">The first argument.</param>
        Other Foo(int? arg1, int? arg2);",
                "public Other Foo(int? arg1, int? arg2) => this.CreateMethodCall(x => x.Foo(arg1, arg2), Other.Create);");

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

        private string FormatMemberTemplate(string interfaceMembers, string stubMembers)
        {
            if (stubMembers != null)
            {
                stubMembers = "\r\n        " + stubMembers + "\r\n";
            }

            return string.Format(MemberTemplate, interfaceMembers, stubMembers);
        }
    }
}
