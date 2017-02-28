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
    public class {className} : QueryEntity
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
            switch (field.Type.Kind)
            {
                case TypeKind.Object:
                    return GenerateObjectField(field, field.Type);
                case TypeKind.NonNull:
                    return GenerateObjectField(field, field.Type.OfType);
                case TypeKind.List:
                    return GenerateListField(field, field.Type.OfType);
                default:
                    throw new NotImplementedException();
            }
        }

        private static string GenerateObjectField(FieldModel field, TypeModel type)
        {
            return $"        public {type.Name} {field.Name} => this.CreateProperty(x => x.{field.Name}, {type.Name}.Create);";
        }

        private static string GenerateListField(FieldModel field, TypeModel type)
        {
            return $"        public IQueryable<{type.Name}> {field.Name} => this.CreateProperty(x => x.{field.Name});";
        }
    }
}
