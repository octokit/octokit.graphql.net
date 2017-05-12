using System;

namespace Octokit.GraphQL.Core.Utilities
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string s)
        {
            return char.ToLowerInvariant(s[0]) + s.Substring(1);
        }

        public static string ToPascalCase(this string str)
        {
            var tokens = str.Split(new[] { "_" }, StringSplitOptions.RemoveEmptyEntries);

            for (var i = 0; i < tokens.Length; i++)
            {
                var token = tokens[i];
                tokens[i] = token.Substring(0, 1).ToUpper() + token.Substring(1).ToLower();
            }

            return string.Join("", tokens);
        }
    }
}
