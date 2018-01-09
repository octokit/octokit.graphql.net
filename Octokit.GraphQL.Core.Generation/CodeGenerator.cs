using System;
using System.Collections.Generic;
using System.IO;
using Octokit.GraphQL.Core.Generation.Models;
using Octokit.GraphQL.Core.Introspection;

namespace Octokit.GraphQL.Core.Generation
{
    public static class CodeGenerator
    {
        public static IEnumerable<GeneratedFile> Generate(SchemaModel schema, string rootNamespace, string entityNamespace = null)
        {
            foreach (var type in schema.Types)
            {
                if (!type.Name.StartsWith("__") && type.Kind != TypeKind.Scalar)
                {
                    var content = Generate(type, rootNamespace, schema.QueryType, entityNamespace);

                    if (content != null)
                    {
                        var fileName = type.Name + ".cs";

                        if (type.Name != schema.QueryType)
                        {
                            fileName = Path.Combine("Model", fileName);
                        }

                        yield return new GeneratedFile(fileName, content);
                    }
                }
            }
        }

        public static string Generate(TypeModel type, string rootNamespace, string queryType, string entityNamespace = null)
        {
            entityNamespace = entityNamespace ?? rootNamespace;

            switch (type.Kind)
            {
                case TypeKind.Object:
                    if (type.Name == queryType || type.Name == "Mutation")
                    {
                        var interfaceName = "IQuery";
                        if (type.Name != queryType)
                        {
                            interfaceName = "I" + type.Name;
                        }

                        return EntityGenerator.GenerateRoot(type, rootNamespace, entityNamespace, interfaceName, queryType);
                    }
                    else
                    {
                        return EntityGenerator.Generate(type, entityNamespace, queryType, entityNamespace: entityNamespace);
                    }

                case TypeKind.Interface:
                    return InterfaceGenerator.Generate(type, entityNamespace, queryType);

                case TypeKind.Enum:
                    return EnumGenerator.Generate(type, entityNamespace);

                case TypeKind.InputObject:
                    return InputObjectGenerator.Generate(type, entityNamespace);

                case TypeKind.Union:
                    return UnionGenerator.Generate(type, entityNamespace);

                default:
                    return null;
            }
        }
    }
}
