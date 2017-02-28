using System;
using LinqToGraphQL.Generation.Models;
using LinqToGraphQL.Introspection;
using Xunit;

namespace LinqToGraphQL.Generation.UnitTests
{
    public class CodeGeneratorTests
    {
        [Fact]
        public void Generates_Property_For_Field()
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

        public Other Foo => this.CreateProperty(x => x.Foo, Other.Create);
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
                        Name = "Foo",
                        Type = TypeModel.Object("Other")
                    },
                }
            };

            var result = CodeGenerator.GenerateType(model, "Test");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Property_For_NonNull_Field()
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

        public Other Foo => this.CreateProperty(x => x.Foo, Other.Create);
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
                        Name = "Foo",
                        Type = TypeModel.NonNull(TypeModel.Object("Other")),
                    },
                }
            };

            var result = CodeGenerator.GenerateType(model, "Test");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Property_For_List_Field()
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

        public IQueryable<Other> Foo => this.CreateProperty(x => x.Foo);
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
                        Name = "Foo",
                        Type = TypeModel.List(TypeModel.Object("Other")),
                    },
                }
            };

            var result = CodeGenerator.GenerateType(model, "Test");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Method_For_Field_With_Args()
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

        public Other Foo(int bar) => this.CreateMethodCall(x => x.Foo(bar), Other.Create);
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
                        Name = "Foo",
                        Type = TypeModel.Object("Other"),
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

            var result = CodeGenerator.GenerateType(model, "Test");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Method_For_NonNull_Field_With_Args()
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

        public Other Foo(int bar) => this.CreateMethodCall(x => x.Foo(bar), Other.Create);
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
                        Name = "Foo",
                        Type = TypeModel.NonNull(TypeModel.Object("Other")),
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

            var result = CodeGenerator.GenerateType(model, "Test");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Generates_Method_For_List_Field_With_Args()
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

        public IQueryable<Other> Foo(int bar) => this.CreateMethodCall(x => x.Foo(bar));
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
                        Name = "Foo",
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

            var result = CodeGenerator.GenerateType(model, "Test");

            Assert.Equal(expected, result);
        }
    }
}
