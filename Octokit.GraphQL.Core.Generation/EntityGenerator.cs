﻿using System;
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
            string queryType,
            string modifiers = "public ",
            bool generateDocComments = true,
            string entityNamespace = null,
            string graphQlModelType = null)
        {
            var className = TypeUtilities.GetClassName(type);
            var pagingConnectionNodeType = GetPagingConnectionNodeType(type);
            var annotations = TypeUtilities.GetGraphQlIdentifierAttribute(graphQlModelType);

            return $@"namespace {rootNamespace}
{{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    {GenerateDocComments(type, generateDocComments)}{annotations}{modifiers}class {className} : QueryableValue<{className}>{GenerateImplementedInterfaces(type, pagingConnectionNodeType)}
    {{
        internal {className}(Expression expression) : base(expression)
        {{
        }}{GenerateFields(type, generateDocComments, rootNamespace, entityNamespace, queryType, pagingConnectionNodeType != null)}

        internal static {className} Create(Expression expression)
        {{
            return new {className}(expression);
        }}
    }}
}}";
        }

        public static string GenerateRoot(TypeModel type, string rootNamespace, string entityNamespace, string interfaceName, string queryType)
        {
            var className = TypeUtilities.GetClassName(type);

            var includeEntities = rootNamespace == entityNamespace ? string.Empty: $@"
    using {entityNamespace};";

            return $@"namespace {rootNamespace}
{{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;{includeEntities}
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    {GenerateDocComments(type, true)}public class {className} : QueryableValue<{className}>, {interfaceName}
    {{
        public {className}() : base(null)
        {{
        }}

        internal {className}(Expression expression) : base(expression)
        {{
        }}{GenerateFields(type, true, rootNamespace, entityNamespace, queryType, false)}

        internal static {className} Create(Expression expression)
        {{
            return new {className}(expression);
        }}
    }}
}}";
        }

        private static string GenerateFields(TypeModel type, bool generateDocComments, string rootNamespace, string entityNamespace, string queryType, bool isPagingConnection)
        {
            var builder = new StringBuilder();
            var first = true;

            if (type.Fields?.Count > 0)
            {
                builder.AppendLine();

                foreach (var field in type.Fields)
                {
                    if (!first)
                    {
                        builder.AppendLine();
                    }

                    builder.AppendLine();
                    builder.Append(GenerateField(field, generateDocComments, rootNamespace, entityNamespace, queryType));

                    first = false;
                }
            }

            if (isPagingConnection)
            {
                if (!first)
                {
                    builder.AppendLine();
                }

                builder.AppendLine();
                builder.Append($"        IPageInfo IPagingConnection.PageInfo => PageInfo;");
            }

            return builder.ToString();
        }

        private static string GenerateField(FieldModel field, bool generateDocComments, string rootNamespace, string entityNamespace, string queryType)
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
            else
            {
                result += method ?
                    GenerateObjectMethod(field, reduced, entityNamespace) :
                    GenerateObjectField(field, reduced, rootNamespace, entityNamespace, queryType);
            }

            return result;
        }

        private static string GenerateDocComments(TypeModel type, bool generate)
        {
            if (generate && !string.IsNullOrWhiteSpace(type.Description))
            {
                var builder = new StringBuilder();
                DocCommentGenerator.GenerateSummary(type.Description, 4, builder);
                builder.Append("    ");
                return builder.ToString().TrimStart();
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
                var builder = new StringBuilder();
                DocCommentGenerator.GenerateSummary(field.Description, 8, builder);

                if (field.Args != null)
                {
                    foreach (var arg in BuildUtilities.SortArgs(field.Args))
                    {
                        if (!string.IsNullOrWhiteSpace(arg.Description))
                        {
                            var description = string.Join(" ", arg.Description.Split(new[]{ '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)).Trim();
                            builder.AppendLine($"        /// <param name=\"{arg.Name}\">{description}</param>");
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

        private static void GenerateDocComments(string text, int indentation, StringBuilder builder)
        {
            var indent = new string(' ', indentation);

            foreach (var line in text.Split('\r', '\n').Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                builder.Append(indent);
                builder.Append("/// ");
                builder.AppendLine(line);
            }
        }

        private static string GenerateScalarField(FieldModel field, TypeModel type)
        {
            var obsoleteAttribute = field.IsDeprecated
                    ? $@"        {AttributeGenerator.GenerateObsoleteAttribute(field.DeprecationReason)}{Environment.NewLine}"
                    : string.Empty;

            var name = TypeUtilities.PascalCase(field.Name);
            var typeName = TypeUtilities.GetCSharpReturnType(type);
            return $"{obsoleteAttribute}        public {typeName} {name} {{ get; }}";
        }

        private static string GenerateObjectField(FieldModel field, TypeModel type, string rootNamespace, string entityNamespace, string queryType)
        {
            var obsoleteAttribute = field.IsDeprecated
                ? $@"        {AttributeGenerator.GenerateObsoleteAttribute(field.DeprecationReason)}{Environment.NewLine}"
                : string.Empty;

            var name = TypeUtilities.PascalCase(field.Name);
            var typeName = TypeUtilities.GetCSharpReturnType(type);
            var implName = GetEntityImplementationName(type,(typeName != queryType) ? entityNamespace : rootNamespace);
            return $"{obsoleteAttribute}        public {typeName} {name} => this.CreateProperty(x => x.{name}, {implName}.Create);";
        }

        private static string GenerateScalarMethod(FieldModel field, TypeModel type)
        {
            var obsoleteAttribute = field.IsDeprecated
                ? $@"        {AttributeGenerator.GenerateObsoleteAttribute(field.DeprecationReason)}{Environment.NewLine}"
                : string.Empty;

            var name = TypeUtilities.PascalCase(field.Name);
            var csharpType = TypeUtilities.GetCSharpReturnType(type);

            GenerateArguments(field, out string arguments, out string parameters);

            return $"{obsoleteAttribute}        public {csharpType} {name}({arguments}) => default;";
        }

        private static string GenerateObjectMethod(FieldModel field, TypeModel type, string entityNamespace)
        {
            var name = TypeUtilities.PascalCase(field.Name);
            var typeName = TypeUtilities.GetCSharpReturnType(type);
            var implName = GetEntityImplementationName(type, entityNamespace);

            GenerateArguments(field, out string arguments, out string parameters);

            return $"        public {typeName} {name}({arguments}) => this.CreateMethodCall(x => x.{name}({parameters}), {implName}.Create);";
        }

        private static string GenerateListField(FieldModel field, TypeModel type)
        {
            var obsoleteAttribute = field.IsDeprecated
                ? $@"        {AttributeGenerator.GenerateObsoleteAttribute(field.DeprecationReason)}{Environment.NewLine}"
                : string.Empty;

            var name = TypeUtilities.PascalCase(field.Name);
            var typeName = TypeUtilities.GetCSharpReturnType(type);
            var getter = TypeUtilities.IsCSharpPrimitive(type.OfType) ?
                "{ get; }" :
                $"=> this.CreateProperty(x => x.{name});";

            return $"{obsoleteAttribute}        public {typeName} {name} {getter}";
        }

        private static string GenerateListMethod(FieldModel field, TypeModel type)
        {
            var name = TypeUtilities.PascalCase(field.Name);
            var typeName = TypeUtilities.GetCSharpReturnType(type);

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
                argBuilder
                    .Append(TypeUtilities.GetWrappedArgType(arg.Type))
                    .Append(' ')
                    .Append(argName);
                paramBuilder.Append(argName);

                if (arg.Type.Kind != TypeKind.NonNull)
                {
                    argBuilder.Append(" = null");
                }
                else if (arg.DefaultValue != null)
                {
                    throw new Exception("Encountered default value for non-null type.");
                }

                first = false;
            }

            arguments = argBuilder.ToString();
            parameters = paramBuilder.ToString();
        }

        private static string GenerateImplementedInterfaces(TypeModel type, TypeModel pagingConnectionNodeType)
        {
            var builder = new StringBuilder();

            if (type.Name == "PageInfo")
            {
                builder.Append(", IPageInfo");
            }

            if (pagingConnectionNodeType != null)
            {
                builder.Append(", IPagingConnection<");
                builder.Append(pagingConnectionNodeType.Name);
                builder.Append('>');
            }

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

        private static TypeModel GetPagingConnectionNodeType(TypeModel type)
        {
            var nodes = type.Fields?.FirstOrDefault(x =>
                x.Name == "nodes" &&
                x.Type.Kind == TypeKind.List);
            var pageInfo = type.Fields?.FirstOrDefault(x => 
                x.Name == "pageInfo" &&
                x.Type.Kind == TypeKind.NonNull &&
                x.Type.OfType.Kind == TypeKind.Object &&
                x.Type.OfType.Name == "PageInfo");

            if (nodes != null && pageInfo != null)
            {
                return nodes.Type.OfType;
            }

            return null;
        }

        private static string GetEntityImplementationName(TypeModel type, string entityNamespace)
        {
            switch (type.Kind)
            {
                case TypeKind.Interface:
                    return entityNamespace + ".Internal.Stub" + TypeUtilities.GetInterfaceName(type);
                default:
                    return entityNamespace + "." + TypeUtilities.GetClassName(type);
            }
        }
    }
}
