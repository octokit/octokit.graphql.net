using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.GraphQL.Core.UnitTests
{
    public static class ConnectionTests
    {
        private static readonly ICredentialStore CredentialStore = new MockCredentialStore();
        private static readonly ProductHeaderValue ProductInformation = new ProductHeaderValue("Octokit.GraphQL.Core.Tests", "1.0.0");

        [Fact]
        public static void Default_GraphQL_Uri_Is_Correct()
        {
            Assert.Equal("https://api.github.com/graphql", Connection.GithubApiUri?.ToString());
        }

        [Fact]
        public static void Connection_Constructor_Validates_Arguments_For_Null()
        {
            var productInformation = ProductInformation;
            var uri = new Uri("https://api.github.enterprise.local/graphql");
            var credentialStore = CredentialStore;
            var httpClient = new HttpClient();

            Assert.Throws<ArgumentNullException>("productInformation", () => new Connection(null, uri, credentialStore, httpClient));
            Assert.Throws<ArgumentNullException>("uri", () => new Connection(productInformation, null, credentialStore, httpClient));
            Assert.Throws<ArgumentNullException>("credentialStore", () => new Connection(productInformation, uri, null, httpClient));
            Assert.Throws<ArgumentNullException>("httpClient", () => new Connection(productInformation, uri, credentialStore, null));
        }

        [Fact]
        public static void Connection_Constructor_Throws_If_Uri_Is_Relative()
        {
            var productInformation = ProductInformation;
            var uri = new Uri("/graphql", UriKind.Relative);
            var credentialStore = CredentialStore;

            var exception = Assert.Throws<ArgumentException>("uri", () => new Connection(productInformation, uri, credentialStore));
            Assert.StartsWith("The base address for the connection must be an absolute URI.", exception.Message);
        }

        [Fact]
        public static void Connection_Constructors_With_No_Uri_Parameter_Use_GitHub_GraphQL_Uri()
        {
            var connection = new Connection(ProductInformation, "token");
            Assert.Equal(Connection.GithubApiUri, connection.Uri);

            connection = new Connection(ProductInformation, CredentialStore);
            Assert.Equal(Connection.GithubApiUri, connection.Uri);

            connection = new Connection(ProductInformation, CredentialStore, new HttpClient());
            Assert.Equal(Connection.GithubApiUri, connection.Uri);
        }

        [Fact]
        public static async Task Run_Specifies_Cancellation_Token()
        {
            var query = "{}";
            var cancellationToken = new CancellationToken(true);

            var httpClient = CreateFakeHttpClient(
                (request, token) =>
                {
                    Assert.True(token.IsCancellationRequested);
                });

            var connection = new Connection(ProductInformation, CredentialStore, httpClient);

            await connection.Run(query, cancellationToken);
        }

        [Theory]
        [InlineData("Accept", "application/vnd.github.antiope-preview+json")]
        [InlineData("Authorization", "bearer my-token")]
        [InlineData("User-Agent", "Octokit.GraphQL.Core.Tests/1.0.0")]
        public static async Task Run_Specifies_Http_Headers(string name, string expected)
        {
            var query = "{}";

            var httpClient = CreateFakeHttpClient(
                (request, token) =>
                {
                    Assert.Equal(expected, string.Concat(request.Headers.GetValues(name)));
                });

            var connection = new Connection(ProductInformation, CredentialStore, httpClient);

            await connection.Run(query);
        }

        [Fact]
        public static void Run_Throws_If_Query_Is_Null()
        {
            var connection = new Connection(ProductInformation, CredentialStore);
            Assert.ThrowsAsync<ArgumentNullException>("query", () => connection.Run(null));
        }

        [Fact]
        public static async Task Run_Throws_If_Http_Request_Does_Not_Return_An_Http_2xx_Response_Code()
        {
            var httpClient = CreateFakeHttpClient(statusCode: HttpStatusCode.BadRequest);
            var connection = new Connection(ProductInformation, CredentialStore);
            var query = "{}";

            await Assert.ThrowsAsync<HttpRequestException>(() => connection.Run(query));
        }

        [Fact]
        public static async Task Run_Uses_The_Specified_Uri()
        {
            var productInformation = ProductInformation;
            var uri = new Uri("https://api.github.enterprise.local/graphql");
            var credentialStore = CredentialStore;
            var query = "{}";

            var httpClient = CreateFakeHttpClient(
                (request, token) =>
                {
                    Assert.Equal(uri, request.RequestUri);
                });

            var connection = new Connection(ProductInformation, uri, CredentialStore, httpClient);

            await connection.Run(query);
        }

        private static HttpClient CreateFakeHttpClient(Action<HttpRequestMessage, CancellationToken> inspector = null, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var handler = new MockHttpMessageHandler(inspector, statusCode);
            return new HttpClient(handler);
        }

        private sealed class MockHttpMessageHandler : HttpMessageHandler
        {
            private readonly Action<HttpRequestMessage, CancellationToken> _inspector;
            private readonly HttpStatusCode _statusCode;

            public MockHttpMessageHandler(Action<HttpRequestMessage, CancellationToken> inspector, HttpStatusCode statusCode = HttpStatusCode.OK)
            {
                _inspector = inspector;
                _statusCode = statusCode;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                _inspector?.Invoke(request, cancellationToken);

                var response = new HttpResponseMessage(_statusCode)
                {
                    Content = new StringContent(string.Empty)
                };

                return Task.FromResult(response);
            }
        }

        private sealed class MockCredentialStore : ICredentialStore
        {
            public Task<string> GetCredentials(CancellationToken cancellationToken) => Task.FromResult("my-token");
        }
    }
}
