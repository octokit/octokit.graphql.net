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
                        return GenerateRootEntity(type, rootNamespace);
                    }
                    else
                    {
                        return GenerateEntity(type, rootNamespace);
                    }

                case TypeKind.Enum:
                    return GenerateEnum(type, rootNamespace);

                default:
                    throw new NotImplementedException();
            }
        }

        private static string GenerateEntity(TypeModel type, string rootNamespace)
        {
            var className = PascalCase(type.Name);

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
        }}{GenerateFields(type)}
    }}
}}";
        }

        private static string GenerateRootEntity(TypeModel type, string rootNamespace)
        {
            var className = type.Name;

            return $@"using System.Linq;
using System.Linq.Expressions;
using LinqToGraphQL;
using LinqToGraphQL.Builders;

namespace {rootNamespace}
{{
    {GenerateDocComments(type)}public class {className} : QueryEntity, IRootQuery
    {{
        public {className}() : base(new QueryProvider())
        {{
        }}{GenerateFields(type)}
    }}
}}";
        }

        private static string GenerateEnum(TypeModel type, string rootNamespace)
        {
            return $@"using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Test
{{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum {type.Name}
    {{{GenerateEnumValues(type)}    }}
}}";
        }

        private static string GenerateFields(TypeModel type)
        {
            var builder = new StringBuilder();

            if (type.Fields?.Count > 0)
            {
                builder.AppendLine();

                foreach (var field in type.Fields)
                {
                    builder.AppendLine();
                    builder.Append(GenerateField(field));
                }
            }

            return builder.ToString();
        }

        private static string GenerateField(FieldModel field)
        {
            var method = field.Args?.Count > 0;
            var result = GenerateDocComments(field);
            var reduced = ReduceKind(field.Type);

            switch (reduced)
            {
                case TypeKind.Scalar:
                    result += method ?
                        GenerateScalarMethod(field, field.Type) :
                        GenerateScalarField(field, field.Type);
                    break;
                case TypeKind.Object:
                case TypeKind.Interface:
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

        private static string GenerateScalarField(FieldModel field, TypeModel type)
        {
            var name = PascalCase(field.Name);
            return $"        public {GetCSharpType(type)} {name} {{ get; }}";
        }

        private static string GenerateObjectField(FieldModel field, TypeModel type)
        {
            var name = PascalCase(field.Name);
            return $"        public {type.Name} {name} => this.CreateProperty(x => x.{name}, {type.Name}.Create);";
        }

        private static string GenerateScalarMethod(FieldModel field, TypeModel type)
        {
            var name = PascalCase(field.Name);
            type = TypeModel.NonNull(ReduceNonNull(type));

            string arguments;
            string parameters;
            GenerateArguments(field, out arguments, out parameters);

            return $"        public IQueryable<{GetCSharpType(type)}> {name}({arguments}) => this.CreateMethodCall(x => x.{name}({parameters}));";
        }

        private static string GenerateObjectMethod(FieldModel field, TypeModel type)
        {
            var name = PascalCase(field.Name);

            string arguments;
            string parameters;
            GenerateArguments(field, out arguments, out parameters);

            return $"        public {type.Name} {name}({arguments}) => this.CreateMethodCall(x => x.{name}({parameters}), {type.Name}.Create);";
        }

        private static string GenerateListField(FieldModel field, TypeModel type)
        {
            var name = PascalCase(field.Name);
            return $"        public IQueryable<{type.Name}> {name} => this.CreateProperty(x => x.{name});";
        }

        private static string GenerateListMethod(FieldModel field, TypeModel type)
        {
            var name = PascalCase(field.Name);

            string arguments;
            string parameters;
            GenerateArguments(field, out arguments, out parameters);

            return $"        public IQueryable<{type.Name}> {name}({arguments}) => this.CreateMethodCall(x => x.{name}({parameters}));";
        }

        private static string GenerateEnumValues(TypeModel type)
        {
            var builder = new StringBuilder();

            foreach (var value in type.EnumValues)
            {
                builder.AppendLine();
                builder.AppendLine(GenerateEnumValue(value));
            }

            return builder.ToString();
        }

        private static string GenerateEnumValue(EnumValueModel value)
        {
            return $@"        [EnumMember(Value = ""{value.Name}"")]
        {PascalCaseEnum(value.Name)},";
        }

        private static string GetCSharpType(TypeModel type, bool nullable = true)
        {

            switch (type.Kind)
            {
                case TypeKind.Scalar:
                    var question = nullable ? "?" : "";
                    switch (type.Name)
                    {
                        case "Boolean": return "bool" + question;
                        case "ID": return "string" + question;
                        default: return type.Name.ToLowerInvariant() + question;
                    }
                case TypeKind.Object:
                case TypeKind.Enum:
                case TypeKind.Interface:
                case TypeKind.InputObject:
                    return type.Name;
                case TypeKind.NonNull:
                    return GetCSharpType(type.OfType, false);
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

        private static string PascalCase(string value)
        {
            return value.Substring(0, 1).ToUpperInvariant() + value.Substring(1);
        }

        private static string PascalCaseEnum(string value)
        {
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(
                value.ToLowerInvariant().Replace('_', ' '))
                .Replace(" ", "");
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

        private static TypeKind ReduceKind(TypeModel type)
        {
            if (type.Kind == TypeKind.Scalar || 
                type.Kind == TypeKind.NonNull && type.OfType.Kind == TypeKind.Scalar)
            {
                return TypeKind.Scalar;
            }
            else
            {
                return type.Kind;
            }
        }

        private static TypeModel ReduceNonNull(TypeModel type)
        {
            return type.Kind == TypeKind.NonNull ? type.OfType : type;
        }
    }
}
