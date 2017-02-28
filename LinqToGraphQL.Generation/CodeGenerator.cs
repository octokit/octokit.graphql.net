using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToGraphQL.Generation.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace LinqToGraphQL.Generation
{
    public static class CodeGenerator
    {
        private static readonly ParameterListSyntax ConstructorParameters =
            ParameterList(SeparatedList<ParameterSyntax>(
                new SyntaxNodeOrToken[]
                {
                    Parameter(Identifier("provider")).WithType(IdentifierName("IQueryProvider")),
                    Token(SyntaxKind.CommaToken),
                    Parameter(Identifier("expression")).WithType(IdentifierName("Expression"))
                }));

        private static readonly ConstructorInitializerSyntax ConstructorInitializer =
            ConstructorInitializer(
                SyntaxKind.BaseConstructorInitializer,
                    ArgumentList(
                        SeparatedList<ArgumentSyntax>(
                            new SyntaxNodeOrToken[]
                            {
                                Argument(IdentifierName("provider")),
                                Token(SyntaxKind.CommaToken),
                                Argument(IdentifierName("expression")),
                            })));

        private static readonly SyntaxTokenList Public =
            TokenList(Token(SyntaxKind.PublicKeyword));

        private static readonly SyntaxList<UsingDirectiveSyntax> Usings =
            List(
                new UsingDirectiveSyntax[]
                {
                    UsingDirective(QualifiedName(IdentifierName("System"), IdentifierName("Linq"))),
                    UsingDirective(QualifiedName(QualifiedName(IdentifierName("System"), IdentifierName("Linq")), IdentifierName("Expressions"))),
                    UsingDirective(IdentifierName("LinqToGraphQL")),
                    UsingDirective(QualifiedName(IdentifierName("LinqToGraphQL"), IdentifierName("Builders"))),
                });

        public static string Format(CompilationUnitSyntax compilationUnit)
        {
            var workspace = new AdhocWorkspace();
            return Formatter.Format(compilationUnit, workspace).ToFullString();
        }

        public static CompilationUnitSyntax GenerateType(TypeModel type, string rootNamespace)
        {
            return CompilationUnit()
                .WithUsings(Usings)
                .AddMembers(
                    NamespaceDeclaration(IdentifierName(rootNamespace)).AddMembers(
                        ClassDeclaration(type.Name)
                            .WithModifiers(Public)
                            .AddBaseListTypes(SimpleBaseType(IdentifierName("QueryEntity")))
                            .WithMembers(GenerateMembers(type))));
        }

        private static SyntaxList<MemberDeclarationSyntax> GenerateMembers(TypeModel type)
        {
            return new SyntaxList<MemberDeclarationSyntax>()
                .Add(GenerateConstructor(type))
                .AddRange(GenerateFieldMembers(type));
        }

        private static MemberDeclarationSyntax GenerateConstructor(TypeModel type)
        {
            return ConstructorDeclaration(type.Name)
                .WithModifiers(Public)
                .WithParameterList(ConstructorParameters)
                .WithInitializer(ConstructorInitializer)
                .WithBody(Block());
        }

        private static IEnumerable<MemberDeclarationSyntax> GenerateFieldMembers(TypeModel type)
        {
            foreach (var field in type.Fields)
            {
                yield return PropertyDeclaration(IdentifierName(field.Type.Name), field.Name)
                    .WithModifiers(Public)
                    .WithExpressionBody(
                        ArrowExpressionClause(
                            InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    ThisExpression(),
                                    IdentifierName("CreateProperty")))
                            .WithArgumentList(
                                ArgumentList(
                                    SeparatedList<ArgumentSyntax>(
                                        new SyntaxNodeOrToken[]
                                        {
                                            Argument(
                                                SimpleLambdaExpression(
                                                    Parameter(Identifier("x")),
                                                    MemberAccessExpression(
                                                        SyntaxKind.SimpleMemberAccessExpression,
                                                        IdentifierName("x"),
                                                        IdentifierName(field.Name)))),
                                            Token(SyntaxKind.CommaToken),
                                            Argument(
                                                MemberAccessExpression(
                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                    IdentifierName(field.Type.Name),
                                                    IdentifierName("Create")))
                                        })))))
                            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
            }
        }
    }
}
