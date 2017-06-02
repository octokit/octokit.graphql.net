using System;
using Octokit.GraphQL.Core.Generation.Models;
using Octokit.GraphQL.Core.Introspection;
using Xunit;

namespace Octokit.GraphQL.Core.Generation.UnitTests
{
    public class InterfaceGenerationTests
    {
        const string MemberTemplate = @"namespace Test
{{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    public interface IEntity : IQueryEntity
    {{
        {0}
    }}
}}

namespace Test.Internal
{{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

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
                "public Other Foo => this.CreateProperty(x => x.Foo, Test.Other.Create);");

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
                "public Other Foo => this.CreateProperty(x => x.Foo, Test.Other.Create);");

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
        public void Generates_Method_For_Object_Field_With_Int_Arg()
        {
            var expected = FormatMemberTemplate(
                "Other Foo(int bar);",
                "public Other Foo(int bar) => this.CreateMethodCall(x => x.Foo(bar), Test.Other.Create);");

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
        public void Generates_Method_For_NonNull_Object_Field_With_Int_Arg()
        {
            var expected = FormatMemberTemplate(
                "Other Foo(int bar);",
                "public Other Foo(int bar) => this.CreateMethodCall(x => x.Foo(bar), Test.Other.Create);");

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
        public void Generates_Method_For_List_Field_With_Nullable_Int_Arg()
        {
            var expected = FormatMemberTemplate(
                "IQueryable<Other> Foo(int? bar = null);",
                "public IQueryable<Other> Foo(int? bar = null) => this.CreateMethodCall(x => x.Foo(bar));");

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
        public void Generates_Method_For_List_Field_With_Nullable_Int_List_Arg()
        {
            var expected = FormatMemberTemplate(
                "IQueryable<Other> Foo(IEnumerable<int?> bar = null);",
                "public IQueryable<Other> Foo(IEnumerable<int?> bar = null) => this.CreateMethodCall(x => x.Foo(bar));");

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
                                Type = TypeModel.List(TypeModel.Int()),
                            }
                        }
                    },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Method_For_List_Field_With_NotNull_Int_List_Arg()
        {
            var expected = FormatMemberTemplate(
                "IQueryable<Other> Foo(IEnumerable<int> bar = null);",
                "public IQueryable<Other> Foo(IEnumerable<int> bar = null) => this.CreateMethodCall(x => x.Foo(bar));");

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
                                Type = TypeModel.List(TypeModel.NonNull(TypeModel.Int())),
                            }
                        }
                    },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Method_For_List_Field_With_Object_List_Arg()
        {
            var expected = FormatMemberTemplate(
                "IQueryable<Other> Foo(IEnumerable<Another> bar = null);",
                "public IQueryable<Other> Foo(IEnumerable<Another> bar = null) => this.CreateMethodCall(x => x.Foo(bar));");

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
                                Type = TypeModel.List(TypeModel.Object("Another")),
                            }
                        }
                    },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Method_For_List_Field_With_NonNull_Object_List_Arg()
        {
            var expected = FormatMemberTemplate(
                "IQueryable<Other> Foo(IEnumerable<Another> bar);",
                "public IQueryable<Other> Foo(IEnumerable<Another> bar) => this.CreateMethodCall(x => x.Foo(bar));");

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
                                Type = TypeModel.NonNull(TypeModel.List(TypeModel.Object("Another"))),
                            }
                        }
                    },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Method_For_List_Field_With_Nullable_Enum_List_Arg()
        {
            var expected = FormatMemberTemplate(
                "IQueryable<Other> Foo(IEnumerable<Another?> bar = null);",
                "public IQueryable<Other> Foo(IEnumerable<Another?> bar = null) => this.CreateMethodCall(x => x.Foo(bar));");

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
                                Type = TypeModel.List(TypeModel.Enum("Another")),
                            }
                        }
                    },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Method_For_List_Field_With_NonNull_Enum_List_Arg()
        {
            var expected = FormatMemberTemplate(
                "IQueryable<Other> Foo(IEnumerable<Another> bar = null);",
                "public IQueryable<Other> Foo(IEnumerable<Another> bar = null) => this.CreateMethodCall(x => x.Foo(bar));");

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
                                Type = TypeModel.List(TypeModel.NonNull(TypeModel.Enum("Another"))),
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
                "int? Foo(int? bar = null);",
                "public int? Foo(int? bar = null) => null;");

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
                "int Foo(int? bar = null);",
                "public int Foo(int? bar = null) => null;");

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

        [Theory]
        [InlineData(TypeKind.Scalar, "Int", "int")]
        [InlineData(TypeKind.Scalar, "Boolean", "bool")]
        [InlineData(TypeKind.Scalar, "String", "string")]
        [InlineData(TypeKind.InputObject, "InputObj", "InputObj")]
        public void NonNull_Arg_With_Null_DefaultValue_Has_No_Default(TypeKind argType, string type, string csharpType)
        {
            var expected = FormatMemberTemplate(
                $"IOther Foo({csharpType} bar);",
                $"public IOther Foo({csharpType} bar) => this.CreateMethodCall(x => x.Foo(bar), Test.Internal.StubIOther.Create);");

            var model = new TypeModel
            {
                Name = "Entity",
                Kind = TypeKind.Interface,
                Fields = new[]
                {
                    new FieldModel
                    {
                        Name = "foo",
                        Type = TypeModel.Interface("Other"),
                        Args = new[]
                        {
                            new InputValueModel
                            {
                                Name = "bar",
                                Type = TypeModel.NonNull(new TypeModel
                                {
                                    Kind = argType,
                                    Name = type,
                                }),
                            }
                        }
                    },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(TypeKind.Scalar, "Int", "int", "5", "5")]
        [InlineData(TypeKind.Scalar, "Boolean", "bool", "true", "true")]
        [InlineData(TypeKind.Scalar, "String", "string", "foo", "\"foo\"")]
        public void NonNull_Arg_With_DefaultValue_Has_Default(TypeKind argType, string type, string csharpType, string defaultValue, string csharpDefaultValue)
        {
            var expected = FormatMemberTemplate(
                $"IOther Foo({csharpType} bar = {csharpDefaultValue});",
                $"public IOther Foo({csharpType} bar = {csharpDefaultValue}) => this.CreateMethodCall(x => x.Foo(bar), Test.Internal.StubIOther.Create);");

            var model = new TypeModel
            {
                Name = "Entity",
                Kind = TypeKind.Interface,
                Fields = new[]
                {
                    new FieldModel
                    {
                        Name = "foo",
                        Type = TypeModel.Interface("Other"),
                        Args = new[]
                        {
                            new InputValueModel
                            {
                                Name = "bar",
                                Type = TypeModel.NonNull(new TypeModel
                                {
                                    Kind = argType,
                                    Name = type,
                                }),
                                DefaultValue = defaultValue,
                            }
                        }
                    },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(TypeKind.Scalar, "Int", "int?")]
        [InlineData(TypeKind.Scalar, "Boolean", "bool?")]
        [InlineData(TypeKind.Scalar, "String", "string")]
        [InlineData(TypeKind.InputObject, "InputObj", "InputObj")]
        public void Nullable_Arg_With_No_DefaultValue_Has_Default(TypeKind argType, string type, string csharpType)
        {
            var expected = FormatMemberTemplate(
                $"IOther Foo({csharpType} bar = null);",
                $"public IOther Foo({csharpType} bar = null) => this.CreateMethodCall(x => x.Foo(bar), Test.Internal.StubIOther.Create);");

            var model = new TypeModel
            {
                Name = "Entity",
                Kind = TypeKind.Interface,
                Fields = new[]
                {
                    new FieldModel
                    {
                        Name = "foo",
                        Type = TypeModel.Interface("Other"),
                        Args = new[]
                        {
                            new InputValueModel
                            {
                                Name = "bar",
                                Type = new TypeModel
                                {
                                    Kind = argType,
                                    Name = type,
                                },
                            }
                        }
                    },
                }
            };

            var result = CodeGenerator.Generate(model, "Test", null);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Args_With_Default_Values_Come_After_Args_With_No_Default_Values()
        {
            var expected = FormatMemberTemplate(
                "IOther Foo(int req1, int req2, int? opt1 = null, int opt2 = 5);",
                "public IOther Foo(int req1, int req2, int? opt1 = null, int opt2 = 5) => " +
                "this.CreateMethodCall(x => x.Foo(req1, req2, opt1, opt2), Test.Internal.StubIOther.Create);");

            var model = new TypeModel
            {
                Name = "Entity",
                Kind = TypeKind.Interface,
                Fields = new[]
                {
                    new FieldModel
                    {
                        Name = "foo",
                        Type = TypeModel.Interface("Other"),
                        Args = new[]
                        {
                            new InputValueModel { Name = "req1", Type = TypeModel.NonNull(TypeModel.Int()) },
                            new InputValueModel { Name = "opt1", Type = TypeModel.Int() },
                            new InputValueModel { Name = "req2", Type = TypeModel.NonNull(TypeModel.Int()) },
                            new InputValueModel { Name = "opt2", Type = TypeModel.NonNull(TypeModel.Int()), DefaultValue = "5" },
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

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
                "public Other Foo => this.CreateProperty(x => x.Foo, Test.Other.Create);");

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
        Other Foo(int? arg1 = null, int? arg2 = null);",
                "public Other Foo(int? arg1 = null, int? arg2 = null) => this.CreateMethodCall(x => x.Foo(arg1, arg2), Test.Other.Create);");

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
