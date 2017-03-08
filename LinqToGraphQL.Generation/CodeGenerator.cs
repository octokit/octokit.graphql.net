using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using LinqToGraphQL.Generation.Models;
using LinqToGraphQL.Introspection;

namespace LinqToGraphQL.Generation
{
    public static class CodeGenerator
    {
        public static IEnumerable<string> Generate(SchemaModel schema, string rootNamespace)
        {
            foreach (var type in schema.Types)
            {
                yield return Generate(type, rootNamespace, schema.QueryType);
            }
        }

        public static string Generate(TypeModel type, string rootNamespace, string queryType)
        {
            switch (type.Kind)
            {
                case TypeKind.Object:
                    if (type.Name == queryType)
                    {
                        return EntityGenerator.GenerateRoot(type, rootNamespace);
                    }
                    else
                    {
                        return EntityGenerator.Generate(type, rootNamespace);
                    }

                case TypeKind.Enum:
                    return EnumGenerator.Generate(type, rootNamespace);

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
