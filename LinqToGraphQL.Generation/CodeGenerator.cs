using System;
using System.Text;
using LinqToGraphQL.Generation.Models;
using LinqToGraphQL.Introspection;

namespace LinqToGraphQL.Generation
{
    public static class CodeGenerator
    {
        public static string GenerateType(TypeModel type, string rootNamespace)
        {
            var className = type.Name;

            return $@"using System.Linq;
using System.Linq.Expressions;
using LinqToGraphQL;
using LinqToGraphQL.Builders;

namespace {rootNamespace}
{{
    {GenerateDocComments(type)}public class {className} : QueryEntity
    {{
        public {className}(IQueryProvider provider, Expression expression) : base(provider, expression)
        {{
        }}

{GenerateFields(type)}
    }}
}}";
        }

        private static string GenerateFields(TypeModel type)
        {
            var builder = new StringBuilder();
            var first = true;

            foreach (var field in type.Fields)
            {
                if (!first)
                {
                    builder.AppendLine();
                }

                builder.Append(GenerateField(field));
                first = false;
            }

            return builder.ToString();
        }

        private static string GenerateField(FieldModel field)
        {
            var method = field.Args?.Count > 0;
            var result = GenerateDocComments(field);

            switch (field.Type.Kind)
            {
                case TypeKind.Object:
                    result += method ?
                        GenerateObjectMethod(field, field.Type) :
                        GenerateObjectField(field, field.Type);
                    break;
                case TypeKind.NonNull:
                    result += method ?
                        GenerateObjectMethod(field, field.Type.OfType) :
                        GenerateObjectField(field, field.Type.OfType);
                    break;
                case TypeKind.List:
                    result += method ?
                        GenerateListMethod(field, field.Type.OfType) :
                        GenerateListField(field, field.Type.OfType);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return result;
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

        private static string GenerateObjectField(FieldModel field, TypeModel type)
        {
            return $"        public {type.Name} {field.Name} => this.CreateProperty(x => x.{field.Name}, {type.Name}.Create);";
        }

        private static string GenerateObjectMethod(FieldModel field, TypeModel type)
        {
            string arguments;
            string parameters;
            GenerateArguments(field, out arguments, out parameters);

            return $"        public {type.Name} {field.Name}({arguments}) => this.CreateMethodCall(x => x.{field.Name}({parameters}), {type.Name}.Create);";
        }

        private static string GenerateListField(FieldModel field, TypeModel type)
        {
            return $"        public IQueryable<{type.Name}> {field.Name} => this.CreateProperty(x => x.{field.Name});";
        }

        private static string GenerateListMethod(FieldModel field, TypeModel type)
        {
            string arguments;
            string parameters;
            GenerateArguments(field, out arguments, out parameters);

            return $"        public IQueryable<{type.Name}> {field.Name}({arguments}) => this.CreateMethodCall(x => x.{field.Name}({parameters}));";
        }

        private static string GetCSharpType(TypeModel type)
        {
            switch (type.Kind)
            {
                case TypeKind.Scalar:
                    switch (type.Name)
                    {
                        case "Boolean": return "bool";
                        case "ID": return "string";
                        default: return type.Name.ToLowerInvariant();
                    }
                case TypeKind.Object:
                case TypeKind.Enum:
                case TypeKind.Interface:
                    return type.Name;
                case TypeKind.NonNull:
                    return GetCSharpType(type.OfType);
                case TypeKind.List:
                    return $"IQueryable<{GetCSharpType(type.OfType)}>";
                default:
                    throw new NotImplementedException();
            }
        }

        private static string GetCSharpLiteral(string value, TypeModel type)
        {
            if (type.Kind == TypeKind.Scalar)
            {
                if (type.Name == "String" || type.Name == "ID")
                {
                    return value == null ? "null" : $"\"{value}\"";
                }
                else
                {
                    return value;
                }
            }
            else if (type.Kind == TypeKind.Enum)
            {
                return $"{type.Name}.{value}";
            }

            throw new NotImplementedException();
        }

        private static void GenerateArguments(FieldModel field, out string arguments, out string parameters)
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

                argBuilder.Append(GetCSharpType(arg.Type));
                argBuilder.Append(' ');
                argBuilder.Append(arg.Name);
                paramBuilder.Append(arg.Name);

                if (arg.DefaultValue != null)
                {
                    argBuilder.Append(" = ");
                    argBuilder.Append(GetCSharpLiteral(arg.DefaultValue, arg.Type));
                }

                first = false;
            }

            arguments = argBuilder.ToString();
            parameters = paramBuilder.ToString();
        }
    }
}
