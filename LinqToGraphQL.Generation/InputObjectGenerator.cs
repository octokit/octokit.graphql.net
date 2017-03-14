using System;
using System.Text;
using LinqToGraphQL.Generation.Models;
using LinqToGraphQL.Generation.Utilities;
using LinqToGraphQL.Introspection;

namespace LinqToGraphQL.Generation
{
    internal static class InputObjectGenerator
    {
        public static string Generate(TypeModel type, string rootNamespace)
        {
            var className = TypeUtilities.GetClassName(type);

            return $@"namespace {rootNamespace}
{{
    using System.Linq;

    {GenerateDocComments(type)}public class {className}
    {{{GenerateFields(type)}
    }}
}}";
        }

        private static string GenerateFields(TypeModel type)
        {
            var builder = new StringBuilder();

            if (type.InputFields?.Count > 0)
            {
                var first = true;

                builder.AppendLine();

                foreach (var field in type.InputFields)
                {
                    if (!first)
                    {
                        builder.AppendLine();
                        builder.AppendLine();
                    }

                    builder.Append(GenerateField(field));

                    first = false;
                }
            }

            return builder.ToString();
        }

        private static string GenerateField(InputValueModel field)
        {
            var name = TypeUtilities.PascalCase(field.Name);
            var typeName = TypeUtilities.GetCSharpType(field.Type);
            return $"        public {typeName} {name} {{ get; set; }}";
        }

        private static string GenerateDocComments(TypeModel type)
        {
            if (!string.IsNullOrWhiteSpace(type.Description))
            {
                return $@"/// <summary>
    /// {type.Description}
    /// </summary>
    ";
            }
            else
            {
                return null;
            }
        }

        private static string GenerateDocComments(FieldModel field)
        {
            if (!string.IsNullOrWhiteSpace(field.Description))
            {
                var builder = new StringBuilder($@"        /// <summary>
        /// {field.Description}
        /// </summary>
");

                if (field.Args != null)
                {
                    foreach (var arg in field.Args)
                    {
                        if (!string.IsNullOrWhiteSpace(arg.Description))
                        {
                            builder.AppendLine($"        /// <param name=\"{arg.Name}\">{arg.Description}</param>");
                        }
                    }
                }

                return builder.ToString();
            }
            else
            {
                return null;
            }
        }

        private static string GenerateScalarField(FieldModel field, TypeModel type)
        {
            var name = TypeUtilities.PascalCase(field.Name);
            return $"        {TypeUtilities.GetCSharpType(type)} {name} {{ get; }}";
        }

        private static string GenerateObjectField(FieldModel field, TypeModel type)
        {
            var name = TypeUtilities.PascalCase(field.Name);
            var typeName = TypeUtilities.GetCSharpType(type);
            return $"        {typeName} {name} {{ get; }}";
        }

        private static string GenerateScalarMethod(FieldModel field, TypeModel type)
        {
            var name = TypeUtilities.PascalCase(field.Name);
            var nonNullType = TypeModel.NonNull(TypeUtilities.ReduceNonNull(type));
            var csharpType = TypeUtilities.GetCSharpType(nonNullType);
            var arguments = GenerateArguments(field);

            return $"        IQueryable<{csharpType}> {name}({arguments});";
        }

        private static string GenerateObjectMethod(FieldModel field, TypeModel type)
        {
            var name = TypeUtilities.PascalCase(field.Name);
            var typeName = TypeUtilities.GetCSharpType(type);
            var arguments = GenerateArguments(field);

            return $"        {typeName} {name}({arguments});";
        }

        private static string GenerateListField(FieldModel field, TypeModel type)
        {
            var name = TypeUtilities.PascalCase(field.Name);
            var typeName = TypeUtilities.GetCSharpType(type);
            return $"        IQueryable<{typeName}> {name} {{ get; }}";
        }

        private static string GenerateListMethod(FieldModel field, TypeModel type)
        {
            var name = TypeUtilities.PascalCase(field.Name);
            var typeName = TypeUtilities.GetCSharpType(type);
            var arguments = GenerateArguments(field);

            return $"        IQueryable<{typeName}> {name}({arguments});";
        }

        private static string GenerateArguments(FieldModel field)
        {
            var argBuilder = new StringBuilder();
            var paramBuilder = new StringBuilder();
            var first = true;

            foreach (var arg in field.Args)
            {
                if (!first)
                {
                    argBuilder.Append(", ");
                    paramBuilder.Append(", ");
                }

                var argName = TypeUtilities.GetArgName(arg);
                argBuilder.Append(TypeUtilities.GetCSharpType(arg.Type));
                argBuilder.Append(' ');
                argBuilder.Append(argName);
                paramBuilder.Append(argName);

                if (arg.DefaultValue != null)
                {
                    argBuilder.Append(" = ");
                    argBuilder.Append(TypeUtilities.GetCSharpLiteral(arg.DefaultValue, arg.Type));
                }

                first = false;
            }

            return argBuilder.ToString();
        }

        private static string GenerateStub(TypeModel type, string rootNamespace)
        {
            var stubType = type.Clone();
            stubType.Name = "Stub" + TypeUtilities.GetInterfaceName(type);
            stubType.Kind = TypeKind.Object;
            stubType.Interfaces = new[] { type };
            return EntityGenerator.Generate(stubType, rootNamespace + ".Internal", "internal ", false);
        }
    }
}
