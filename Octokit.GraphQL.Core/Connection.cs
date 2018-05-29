using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Octokit.GraphQL.Internal;

namespace Octokit.GraphQL
{
    public class Connection : IConnection
    {
        /// <summary>
        /// The address of the GitHub GraphQL API.
        /// </summary>
        public static readonly Uri GithubApiUri = new Uri("https://api.github.com/graphql");

        public Connection(ProductHeaderValue productInformation, string token)
            : this(productInformation, GithubApiUri, token)
        {
        }

        public Connection(ProductHeaderValue productInformation, Uri uri, string token)
            : this(productInformation, uri, new InMemoryCredentialStore(token))
        {
        }

        public Connection(ProductHeaderValue productInformation, ICredentialStore credentialStore)
            : this(productInformation, GithubApiUri, credentialStore)
        {
        }

        public Connection(ProductHeaderValue productInformation, Uri uri, ICredentialStore credentialStore)
        {
            Uri = uri;
            CredentialStore = credentialStore;
            HttpClient = CreateHttpClient(productInformation);
        }

        public Uri Uri { get; }
        protected ICredentialStore CredentialStore { get; }
        protected HttpClient HttpClient { get; }

        public async Task<string> Run(string query)
        {
            var token = await CredentialStore.GetCredentials();

            using (var request = new HttpRequestMessage(HttpMethod.Post, Uri))
            {
                request.Content = new StringContent(query);
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);

                using (var response = await HttpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }

        private HttpClient CreateHttpClient(ProductHeaderValue header)
        {
            var result = new HttpClient();
            var userAgent = new ProductInfoHeaderValue(header.Name, header.Version);
            result.DefaultRequestHeaders.UserAgent.Add(userAgent);
            return result;
        }
    }
}
