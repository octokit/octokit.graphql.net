using System;
using System.Collections.Generic;
using System.Linq;
using LinqToGraphQL.Builders;
using LinqToGraphQL.Introspection;
using LinqToGraphQL.Serializers;
using Xunit;

namespace LinqToGraphQL.UnitTests
{
    public class IntrospectionTests
    {
        [Fact]
        public void Select_Schema_QueryType_Kind()
        {
            var expected = "{__schema{queryType{kind}}}";

            var expression = new IntrospectionQuery()
                .Schema.QueryType
                .Select(x => x.Kind);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Select_Schema_Type_Kind_Name_Description()
        {
            var expected = "{__schema{types{kind name description}}}";

            var expression = new IntrospectionQuery()
                .Schema
                .Select(x => new SchemaModel
                {
                    Types = x.Types.Select(t => new TypeModel
                    {
                        Kind = t.Kind,
                        Name = t.Name,
                        Description = t.Description,
                    }).ToList()
                });

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        private class SchemaModel
        {
            public IList<TypeModel> Types { get; set; }
        }

        private class TypeModel
        {
            public TypeKind Kind { get; set; } 
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }
}
