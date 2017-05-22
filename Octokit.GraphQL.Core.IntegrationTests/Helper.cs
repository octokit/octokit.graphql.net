using System;
using System.Diagnostics;

namespace Octokit.GraphQL.Core.IntegrationTests
{
    public static class Helper
    {
        private static string Username => Environment.GetEnvironmentVariable("OCTOKIT_GQL_GITHUBUSERNAME");

        private static string Password => Environment.GetEnvironmentVariable("OCTOKIT_GQL_GITHUBPASSWORD");

        private static string OAuthToken => Environment.GetEnvironmentVariable("OCTOKIT_GQL_OAUTHTOKEN");

        public static bool HasCredentials => !string.IsNullOrWhiteSpace(Username) && (!string.IsNullOrWhiteSpace(Password) || !string.IsNullOrWhiteSpace(OAuthToken));
    }
}
