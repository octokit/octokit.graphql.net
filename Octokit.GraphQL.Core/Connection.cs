using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Octokit.GraphQL.Internal;

namespace Octokit.GraphQL
{
    /// <inheritdoc />
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

        /// <inheritdoc />
        public Connection(HttpClient httpClient, ICredentialStore credentialStore)
        {
            HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            CredentialStore = credentialStore ?? throw new ArgumentNullException(nameof(credentialStore));

            if (httpClient.BaseAddress == null)
            {
                httpClient.BaseAddress = GithubApiUri;
            }

            Uri = httpClient.BaseAddress;
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

        private static void ConfigureHttpClient(HttpClient httpClient, ProductHeaderValue header)
        {
            if (httpClient.DefaultRequestHeaders.UserAgent.Count == 0)
            {
                var userAgent = new ProductInfoHeaderValue(header.Name, header.Version);
                httpClient.DefaultRequestHeaders.UserAgent.Add(userAgent);
            }

            if (httpClient.DefaultRequestHeaders.Accept.Count == 0)
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.antiope-preview+json"));
            }
        }

        private static HttpClient CreateHttpClient(ProductHeaderValue header)
        {
            var httpClient = new HttpClient();

            ConfigureHttpClient(httpClient, header);

            return httpClient;
        }
    }
}
