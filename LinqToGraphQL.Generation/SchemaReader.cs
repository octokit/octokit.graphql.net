using System;
using System.Linq;
using System.Threading.Tasks;
using LinqToGraphQL.Generation.Models;
using LinqToGraphQL.Introspection;

namespace LinqToGraphQL.Generation
{
    public static class SchemaReader
    {
        public static async Task<SchemaModel> ReadSchema(Connection connection)
        {
            var query = new IntrospectionQuery()
                .Schema
                .Select(x => new SchemaModel
                {
                    QueryType = x.QueryType.Name,
                    MutationType = x.MutationType.Name,
                    Types = x.Types.Select(t => new TypeModel
                    {
                        Kind = t.Kind,
                        Name = t.Name,
                        Description = t.Description,
                        Fields = t.Fields(true).Select((Field f) => new FieldModel
                        {
                            Name = f.Name,
                            Description = f.Description,
                            Type = f.Type.Select((SchemaType y) => new TypeModel
                            {
                                Kind = y.Kind,
                                Name = y.Name,
                                OfType = y.OfType.Select((SchemaType p) => new TypeModel
                                {
                                    Kind = p.Kind,
                                    Name = p.Name,
                                }).SingleOrDefault(),
                            }).Single(),
                            Args = f.Args.Select((InputValue a) => new InputValueModel
                            {
                                Name = a.Name,
                                Description = a.Description,
                                DefaultValue = a.DefaultValue,
                                Type = a.Type.Select((SchemaType y) => new TypeModel
                                {
                                    Kind = y.Kind,
                                    Name = y.Name,
                                    OfType = y.OfType.Select((SchemaType p) => new TypeModel
                                    {
                                        Kind = p.Kind,
                                        Name = p.Name,
                                    }).SingleOrDefault(),
                                }).Single(),
                            }).ToList(),
                            IsDeprecated = f.IsDeprecated,
                            DeprecationReason = f.DeprecationReason,
                        }).ToList(),
                    }).ToList()
                });

            return (await connection.Run(query)).Single();
        }
    }
}
