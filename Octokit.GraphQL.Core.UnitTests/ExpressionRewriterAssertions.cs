using System.Linq.Expressions;
using AgileObjects.ReadableExpressions;
using Xunit;

namespace Octokit.GraphQL.Core.UnitTests
{
    public static class ExpressionRewriterAssertions
    {
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

        private static void AssertCompiledQueryExpressionEqual<T>(Expression expected, ICompiledQuery<T> actualCompiledQuery)
        {
            var expectedString = expected.ToReadableString();
            AssertCompiledQueryExpressionEqual(expectedString, actualCompiledQuery);
        }

        private static void AssertCompiledQueryExpressionEqual<T>(string expectedString, ICompiledQuery<T> actualCompiledQuery)
        {
            var actualResultExpression = actualCompiledQuery.GetResultBuilderExpression();
            var actualString = actualResultExpression.ToReadableString(settings => settings.SerializeAnonymousTypesAsObject);

            Assert.Equal(StripWhitespace(expectedString), StripWhitespace(actualString));
        }

        private static string StripWhitespace(string x)
        {
            return x.Replace(" ", string.Empty).Replace("\r", string.Empty).Replace("\n", string.Empty);
        }
    }
}