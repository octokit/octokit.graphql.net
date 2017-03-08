using System;
using LinqToGraphQL.Generation.Models;
using LinqToGraphQL.Introspection;

namespace LinqToGraphQL.Generation.Utilities
{
    internal static class TypeUtilities
    {
        public static string GetCSharpLiteral(string value, TypeModel type)
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

        public static string GetCSharpType(TypeModel type, bool nullable = true)
        {

            switch (type.Kind)
            {
                case TypeKind.Scalar:
                    var question = nullable ? "?" : "";
                    switch (type.Name)
                    {
                        case "Int": return "int" + question;
                        case "Float": return "double" + question;
                        case "String": return "string";
                        case "Boolean": return "bool" + question;
                        default: return "string";
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
                    return type.Name;
            }
        }

        public static string PascalCase(string value)
        {
            return value.Substring(0, 1).ToUpperInvariant() + value.Substring(1);
        }

        public static TypeKind ReduceKind(TypeModel type)
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

        public static TypeModel ReduceNonNull(TypeModel type)
        {
            return type.Kind == TypeKind.NonNull ? type.OfType : type;
        }
    }
}
