using System;

namespace Octokit.GraphQL.IntegrationTests.Utilities
{
    public static class Helper
    {
        public static string Username => GetSetting("OCTOKIT_GQL_GITHUBUSERNAME");

        public static string OAuthToken => GetSetting("OCTOKIT_GQL_OAUTHTOKEN");

        public static bool HasCredentials => !String.IsNullOrWhiteSpace(Username) && !String.IsNullOrWhiteSpace(OAuthToken);

        private static string GetSetting(string key)
        {
            return Environment.GetEnvironmentVariable(key);
        }
    }
}
