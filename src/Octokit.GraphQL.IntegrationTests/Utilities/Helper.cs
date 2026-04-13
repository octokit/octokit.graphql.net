using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Octokit.GraphQL.IntegrationTests.Utilities
{
    public static class Helper
    {
        private static readonly Lazy<IReadOnlyDictionary<string, string>> DotEnv =
            new Lazy<IReadOnlyDictionary<string, string>>(LoadDotEnv);

        public static string Username => GetSetting("OCTOKIT_GQL_GITHUBUSERNAME");

        public static string OAuthToken => GetSetting("OCTOKIT_GQL_OAUTHTOKEN");

        public static bool HasCredentials => !String.IsNullOrWhiteSpace(Username) && !String.IsNullOrWhiteSpace(OAuthToken);

        private static string GetSetting(string key)
        {
            var value = Environment.GetEnvironmentVariable(key);

            if (!String.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            return DotEnv.Value.TryGetValue(key, out var dotenvValue) ? dotenvValue : null;
        }

        private static IReadOnlyDictionary<string, string> LoadDotEnv()
        {
            foreach (var root in GetCandidateRoots())
            {
                var path = FindDotEnv(root);

                if (path != null)
                {
                    return ParseDotEnv(path);
                }
            }

            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        private static IEnumerable<string> GetCandidateRoots()
        {
            yield return Directory.GetCurrentDirectory();
            yield return AppContext.BaseDirectory;
        }

        private static string FindDotEnv(string startDirectory)
        {
            if (String.IsNullOrWhiteSpace(startDirectory) || !Directory.Exists(startDirectory))
            {
                return null;
            }

            var directory = new DirectoryInfo(startDirectory);

            while (directory != null)
            {
                var candidate = Path.Combine(directory.FullName, ".env");

                if (File.Exists(candidate))
                {
                    return candidate;
                }

                directory = directory.Parent;
            }

            return null;
        }

        private static IReadOnlyDictionary<string, string> ParseDotEnv(string path)
        {
            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var line in File.ReadLines(path).Select(x => x.Trim()))
            {
                if (line.Length == 0 || line.StartsWith("#", StringComparison.Ordinal))
                {
                    continue;
                }

                var separator = line.IndexOf('=');

                if (separator <= 0)
                {
                    continue;
                }

                var key = line.Substring(0, separator).Trim();
                var value = line.Substring(separator + 1).Trim().Trim('"');
                result[key] = value;
            }

            return result;
        }
    }
}
