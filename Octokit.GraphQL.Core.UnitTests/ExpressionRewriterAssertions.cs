using System.Linq.Expressions;
using System.Text.RegularExpressions;
using AgileObjects.ReadableExpressions;
using Xunit;

namespace Octokit.GraphQL.Core.UnitTests
{
    public static class ExpressionRewriterAssertions
    {
        private const string AnonymousType = "AnonymousType";
        private static readonly int AnonymousTypeLength = AnonymousType.Length;

        public static string ExtractAnonymousTypeDeclarations(string actualString)
        {
            var result = actualString;
            var nextIndex = result.IndexOf(AnonymousType);
            while (nextIndex != -1)
            {
                int stack = 0;

                int endSlice;
                for (endSlice = nextIndex + AnonymousTypeLength; endSlice < result.Length; endSlice++)
                {
                    var c = result[endSlice];
                    if (c == '<')
                    {
                        stack++;
                    }

                    if (c == '>')
                    {
                        stack--;
                    }

                    if (stack == 0)
                        break;
                }

                if (nextIndex == 0 && endSlice + 1 == result.Length)
                {
                    result = "object";
                }
                else if (nextIndex == 0)
                {
                    result = "object" + result.Substring(endSlice + 1);
                }
                else
                {
                    var left = result.Substring(0, nextIndex) + "object";

                    if (endSlice != result.Length)
                    {
                        result = left + result.Substring(endSlice + 1);
                    }
                }

                nextIndex = result.IndexOf(AnonymousType);
            }

            return result;
        }

        public static void AssertExpressionQueryEqual<T>(Expression expected, IQueryableValue<T> actual)
        {
            var actualCompiledQuery = actual.Compile();
            AssertCompiledQueryExpressionEqual(expected, actualCompiledQuery);
        }

        public static void AssertExpressionQueryEqual<T>(Expression expected, IQueryableList<T> actual)
        {
            var actualCompiledQuery = actual.Compile();
            AssertCompiledQueryExpressionEqual(expected, actualCompiledQuery);
        }

        public static void AssertExpressionQueryEqual<T>(string expectedString, IQueryableValue<T> actual)
        {
            var actualCompiledQuery = actual.Compile();
            AssertCompiledQueryExpressionEqual(expectedString, actualCompiledQuery);
        }

        public static void AssertExpressionQueryEqual<T>(string expectedString, IQueryableList<T> actual)
        {
            var actualCompiledQuery = actual.Compile();
            AssertCompiledQueryExpressionEqual(expectedString, actualCompiledQuery);
        }

        public static void AssertCompiledQueryExpressionEqual<T>(Expression expected, ICompiledQuery<T> actualCompiledQuery, params string[] subqueryPlaceholderReplacements)
        {
            var expectedString = expected.ToReadableString();
            AssertCompiledQueryExpressionEqual(expectedString, actualCompiledQuery, subqueryPlaceholderReplacements);
        }

        public static void AssertCompiledQueryExpressionEqual<T>(string expectedString, ICompiledQuery<T> actualCompiledQuery, params string[] subqueryPlaceholderReplacements)
        {
            var actualResultExpression = actualCompiledQuery.GetResultBuilderExpression();
            var actualString = actualResultExpression.ToReadableString();

            foreach (var subqueryPlaceholderReplacement in subqueryPlaceholderReplacements)
            {
                var regex = new Regex("PagingTests.subqueryPlaceholder");
                expectedString = regex.Replace(expectedString, subqueryPlaceholderReplacement, 1);
            }

            actualString = ExtractAnonymousTypeDeclarations(actualString);

            Assert.Equal(StripWhitespace(expectedString), StripWhitespace(actualString));
        }

        private static string StripWhitespace(string x)
        {
            return x.Replace(" ", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);
        }
    }

    public class ExpressionRewriterAssertionsTests
    {
        [Theory]
        [InlineData("string", "string")]
        [InlineData("AnonymousType<string>", "object")]
        [InlineData("AnonymousType<string, int>", "object")]
        [InlineData("AnonymousType<string, int, List<string>>", "object")]
        [InlineData("AnonymousType<string, int, AnonymousType<string, int>>", "object")]
        [InlineData("asdf AnonymousType<string, int, AnonymousType<string, int>>", "asdf object")]
        [InlineData("AnonymousType<string, int, AnonymousType<string, int>> asdf", "object asdf")]
        [InlineData("Tuple<int, AnonymousType<string, int, AnonymousType<string, int>>>", "Tuple<int, object>")]
        public void ShouldExtractAnonymousTypeDeclarations(string input, string output)
        {
            Assert.Equal(output, ExpressionRewriterAssertions.ExtractAnonymousTypeDeclarations(input));
        }
    }
}