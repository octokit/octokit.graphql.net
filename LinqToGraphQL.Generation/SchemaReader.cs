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
                            Type = f.Type.Select((SchemaType t1) => new TypeModel
                            {
                                Kind = t1.Kind,
                                Name = t1.Name,
                                OfType = t1.OfType.Select((SchemaType t2) => new TypeModel
                                {
                                    Kind = t2.Kind,
                                    Name = t2.Name,
                                    OfType = t2.OfType.Select((SchemaType t3) => new TypeModel
                                    {
                                        Kind = t3.Kind,
                                        Name = t3.Name,
                                        OfType = t3.OfType.Select((SchemaType t4) => new TypeModel
                                        {
                                            Kind = t4.Kind,
                                            Name = t4.Name,
                                        }).SingleOrDefault(),
                                    }).SingleOrDefault(),
                                }).SingleOrDefault(),
                            }).Single(),
                            Args = f.Args.Select((InputValue a) => new InputValueModel
                            {
                                Name = a.Name,
                                Description = a.Description,
                                DefaultValue = a.DefaultValue,
                                Type = a.Type.Select((SchemaType t1) => new TypeModel
                                {
                                    Kind = t1.Kind,
                                    Name = t1.Name,
                                    OfType = t1.OfType.Select((SchemaType t2) => new TypeModel
                                    {
                                        Kind = t2.Kind,
                                        Name = t2.Name,
                                        OfType = t2.OfType.Select((SchemaType t3) => new TypeModel
                                        {
                                            Kind = t3.Kind,
                                            Name = t3.Name,
                                            OfType = t3.OfType.Select((SchemaType t4) => new TypeModel
                                            {
                                                Kind = t4.Kind,
                                                Name = t4.Name,
                                            }).SingleOrDefault(),
                                        }).SingleOrDefault(),
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
