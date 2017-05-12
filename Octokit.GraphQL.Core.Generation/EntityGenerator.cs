using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Octokit.GraphQL.Core.Generation.Models;
using Octokit.GraphQL.Core.Generation.Utilities;
using Octokit.GraphQL.Core.Introspection;

namespace Octokit.GraphQL.Core.Generation
{
    internal static class EntityGenerator
    {
        public static string Generate(
            TypeModel type,
            string rootNamespace,
            string modifiers = "public ",
            bool generateDocComments = true,
            string entityNamespace = null)
        {
            var className = TypeUtilities.GetClassName(type);

            entityNamespace = entityNamespace ?? rootNamespace;

            return $@"namespace {rootNamespace}
{{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    {GenerateDocComments(type, generateDocComments)}{modifiers}class {className} : QueryEntity{GenerateImplementedInterfaces(type)}
    {{
        public {className}(IQueryProvider provider, Expression expression) : base(provider, expression)
        {{
        }}{GenerateFields(type, generateDocComments, entityNamespace)}

        internal static {className} Create(IQueryProvider provider, Expression expression)
        {{
            return new {className}(provider, expression);
        }}
    }}
}}";
        }

        public static string GenerateRoot(TypeModel type, string rootNamespace)
        {
            var className = TypeUtilities.GetClassName(type);

            return $@"namespace {rootNamespace}
{{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    {GenerateDocComments(type, true)}public class {className} : QueryEntity, IRootQuery
    {{
        public {className}() : base(new QueryProvider())
        {{
        }}

        internal {className}(IQueryProvider provider, Expression expression) : base(provider, expression)
        {{
        }}{GenerateFields(type, true, rootNamespace)}

        internal static {className} Create(IQueryProvider provider, Expression expression)
        {{
            return new {className}(provider, expression);
        }}
    }}
}}";
        }

        private static string GenerateFields(TypeModel type, bool generateDocComments, string rootNamespace)
        {
            var builder = new StringBuilder();

            if (type.Fields?.Count > 0)
            {
                var first = true;

                builder.AppendLine();

                foreach (var field in type.Fields)
                {
                    if (!first)
                    {
                        builder.AppendLine();
                    }

                    builder.AppendLine();
                    builder.Append(GenerateField(field, generateDocComments, rootNamespace));

                    first = false;
                }
            }

            return builder.ToString();
        }

        private static string GenerateField(FieldModel field, bool generateDocComments, string rootNamespace)
        {
            var method = field.Args?.Count > 0;
            var result = GenerateDocComments(field, generateDocComments);
            var reduced = TypeUtilities.ReduceType(field.Type);

            if (TypeUtilities.IsCSharpPrimitive(reduced))
            {
                result += method ?
                    GenerateScalarMethod(field, reduced) :
                    GenerateScalarField(field, reduced);
            }
            else if (reduced.Kind == TypeKind.List)
            {
                result += method ?
                    GenerateListMethod(field, reduced) :
                    GenerateListField(field, reduced);
            }
            else if (reduced.Kind == TypeKind.Union)
            {
                // HACK: Returning IQueryable<object> for unions for now until we decide how to handle them.
                reduced = TypeModel.List(reduced);
                result += method ?
                    GenerateListMethod(field, reduced) :
                    GenerateListField(field, reduced);
            }
            else
            {
                result += method ?
                    GenerateObjectMethod(field, reduced, rootNamespace) :
                    GenerateObjectField(field, reduced, rootNamespace);
            }

            return result;
        }

        private static string GenerateDocComments(TypeModel type, bool generate)
        {
            if (generate && !string.IsNullOrWhiteSpace(type.Description))
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

        private static string GenerateDocComments(FieldModel field, bool generate)
        {
            if (generate && !string.IsNullOrWhiteSpace(field.Description))
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
            var typeName = TypeUtilities.GetCSharpType(type);
            return $"        public {typeName} {name} {{ get; }}";
        }

        private static string GenerateObjectField(FieldModel field, TypeModel type, string rootNamespace)
        {
            var name = TypeUtilities.PascalCase(field.Name);
            var typeName = TypeUtilities.GetCSharpType(type);
            var implName = GetEntityImplementationName(type, rootNamespace);
            return $"        public {typeName} {name} => this.CreateProperty(x => x.{name}, {implName}.Create);";
        }

        private static string GenerateScalarMethod(FieldModel field, TypeModel type)
        {
            var name = TypeUtilities.PascalCase(field.Name);
            var csharpType = TypeUtilities.GetCSharpType(type);

            GenerateArguments(field, out string arguments, out string parameters);

            return $"        public {csharpType} {name}({arguments}) => null;";
        }

        private static string GenerateObjectMethod(FieldModel field, TypeModel type, string rootNamespace)
        {
            var name = TypeUtilities.PascalCase(field.Name);
            var typeName = TypeUtilities.GetCSharpType(type);
            var implName = GetEntityImplementationName(type, rootNamespace);

            GenerateArguments(field, out string arguments, out string parameters);

            return $"        public {typeName} {name}({arguments}) => this.CreateMethodCall(x => x.{name}({parameters}), {implName}.Create);";
        }

        private static string GenerateListField(FieldModel field, TypeModel type)
        {
            var name = TypeUtilities.PascalCase(field.Name);
            var typeName = TypeUtilities.GetCSharpType(type);
            return $"        public {typeName} {name} => this.CreateProperty(x => x.{name});";
        }

        private static string GenerateListMethod(FieldModel field, TypeModel type)
        {
            var name = TypeUtilities.PascalCase(field.Name);
            var typeName = TypeUtilities.GetCSharpType(type);

            GenerateArguments(field, out string arguments, out string parameters);

            return $"        public {typeName} {name}({arguments}) => this.CreateMethodCall(x => x.{name}({parameters}));";
        }

        private static void GenerateArguments(FieldModel field, out string arguments, out string parameters)
        {
            var argBuilder = new StringBuilder();
            var paramBuilder = new StringBuilder();
            var first = true;

            foreach (var arg in BuildUtilities.SortArgs(field.Args))
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
                else if (arg.Type.Kind != TypeKind.NonNull)
                {
                    argBuilder.Append(" = null");
                }

                first = false;
            }

            arguments = argBuilder.ToString();
            parameters = paramBuilder.ToString();
        }

        private static string GenerateImplementedInterfaces(TypeModel type)
        {
            var builder = new StringBuilder();

            if (type.Interfaces != null)
            {
                foreach (var iface in type.Interfaces)
                {
                    builder.Append(", ");
                    builder.Append(TypeUtilities.GetInterfaceName(iface));
                }
            }

            return builder.ToString();
        }

        private static object GetEntityImplementationName(TypeModel type, string rootNamespace)
        {
            switch (type.Kind)
            {
                case TypeKind.Interface:
                    return rootNamespace + ".Internal.Stub" + TypeUtilities.GetInterfaceName(type);
                default:
                    return rootNamespace + "." + TypeUtilities.GetClassName(type);
            }
        }
    }
}
