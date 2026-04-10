using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Octokit.GraphQL.Internal;

namespace Octokit.GraphQL
{
    /// <summary>
    /// A connection for making HTTP requests against the GitHub GraphQL API endpoint.
    /// </summary>
    public class Connection : IConnection
    {
        private const string DefaultMediaType = "application/vnd.github.antiope-preview+json";

        /// <summary>
        /// Gets the address of the GitHub GraphQL API.
        /// </summary>
        public static Uri GithubApiUri { get; } = new Uri("https://api.github.com/graphql");

        /// <summary>
        /// Creates a new connection instance used to make requests of the GitHub GraphQL API.
        /// </summary>
        /// <remarks>
        /// See more information regarding User-Agent requirements here: https://developer.github.com/v3/#user-agent-required.
        /// </remarks>
        /// <param name="productInformation">
        /// The name (and optionally version) of the product using this library, the name of your GitHub organization, or your GitHub username (in that order of preference). This is sent to the server as part of
        /// the user agent for analytics purposes, and used by GitHub to contact you if there are problems.
        /// </param>
        /// <param name="token">The token to use to authenticate with the GitHub GraphQL API.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="productInformation"/> is <see langword="null"/>.
        /// </exception>
        public Connection(ProductHeaderValue productInformation, string token)
            : this(productInformation, GithubApiUri, token)
        {
        }

        /// <summary>
        /// Creates a new connection instance used to make requests of the GitHub GraphQL API.
        /// </summary>
        /// <remarks>
        /// See more information regarding User-Agent requirements here: https://developer.github.com/v3/#user-agent-required.
        /// </remarks>
        /// <param name="productInformation">
        /// The name (and optionally version) of the product using this library, the name of your GitHub organization, or your GitHub username (in that order of preference). This is sent to the server as part of
        /// the user agent for analytics purposes, and used by GitHub to contact you if there are problems.
        /// </param>
        /// <param name="uri">
        /// The address to point this client to such as https://api.github.com or the URL to a GitHub Enterprise instance.
        /// </param>
        /// <param name="token">The token to use to authenticate with the GitHub GraphQL API.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="uri"/> is not an absolute URI.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="productInformation"/> or <paramref name="uri"/> are <see langword="null"/>.
        /// </exception>
        public Connection(ProductHeaderValue productInformation, Uri uri, string token)
            : this(productInformation, uri, new InMemoryCredentialStore(token))
        {
        }

        /// <summary>
        /// Creates a new connection instance used to make requests of the GitHub GraphQL API.
        /// </summary>
        /// <remarks>
        /// See more information regarding User-Agent requirements here: https://developer.github.com/v3/#user-agent-required.
        /// </remarks>
        /// <param name="productInformation">
        /// The name (and optionally version) of the product using this library, the name of your GitHub organization, or your GitHub username (in that order of preference). This is sent to the server as part of
        /// the user agent for analytics purposes, and used by GitHub to contact you if there are problems.
        /// </param>
        /// <param name="credentialStore">Provides credentials to the client when making requests.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="productInformation"/> or <paramref name="credentialStore"/> are <see langword="null"/>.
        /// </exception>
        public Connection(ProductHeaderValue productInformation, ICredentialStore credentialStore)
            : this(productInformation, GithubApiUri, credentialStore)
        {
        }

        /// <summary>
        /// Creates a new connection instance used to make requests of the GitHub GraphQL API.
        /// </summary>
        /// <remarks>
        /// See more information regarding User-Agent requirements here: https://developer.github.com/v3/#user-agent-required.
        /// </remarks>
        /// <param name="productInformation">
        /// The name (and optionally version) of the product using this library, the name of your GitHub organization, or your GitHub username (in that order of preference). This is sent to the server as part of
        /// the user agent for analytics purposes, and used by GitHub to contact you if there are problems.
        /// </param>
        /// <param name="uri">
        /// The address to point this client to such as https://api.github.com or the URL to a GitHub Enterprise instance.
        /// </param>
        /// <param name="credentialStore">Provides credentials to the client when making requests.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="uri"/> is not an absolute URI.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="productInformation"/>, <paramref name="uri"/> or <paramref name="credentialStore"/> are <see langword="null"/>.
        /// </exception>
        public Connection(ProductHeaderValue productInformation, Uri uri, ICredentialStore credentialStore)
            : this(productInformation, uri, credentialStore, new HttpClient())
        {
        }

        /// <summary>
        /// Creates a new connection instance used to make requests of the GitHub GraphQL API.
        /// </summary>
        /// <remarks>
        /// See more information regarding User-Agent requirements here: https://developer.github.com/v3/#user-agent-required.
        /// </remarks>
        /// <param name="productInformation">
        /// The name (and optionally version) of the product using this library, the name of your GitHub organization, or your GitHub username (in that order of preference). This is sent to the server as part of
        /// the user agent for analytics purposes, and used by GitHub to contact you if there are problems.
        /// </param>
        /// <param name="credentialStore">Provides credentials to the client when making requests.</param>
        /// <param name="httpClient">An <see cref="HttpClient"/> used to make requests.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="productInformation"/>, <paramref name="credentialStore"/> or <paramref name="httpClient"/> are <see langword="null"/>.
        /// </exception>
        public Connection(ProductHeaderValue productInformation, ICredentialStore credentialStore, HttpClient httpClient)
            : this(productInformation, GithubApiUri, credentialStore, httpClient)
        {
        }

        /// <summary>
        /// Creates a new connection instance used to make requests of the GitHub GraphQL API.
        /// </summary>
        /// <remarks>
        /// See more information regarding User-Agent requirements here: https://developer.github.com/v3/#user-agent-required.
        /// </remarks>
        /// <param name="productInformation">
        /// The name (and optionally version) of the product using this library, the name of your GitHub organization, or your GitHub username (in that order of preference). This is sent to the server as part of
        /// the user agent for analytics purposes, and used by GitHub to contact you if there are problems.
        /// </param>
        /// <param name="uri">
        /// The address to point this client to such as https://api.github.com or the URL to a GitHub Enterprise instance.
        /// </param>
        /// <param name="credentialStore">Provides credentials to the client when making requests.</param>
        /// <param name="httpClient">An <see cref="HttpClient"/> used to make requests.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="uri"/> is not an absolute URI.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="productInformation"/>, <paramref name="uri"/>, <paramref name="credentialStore"/> or <paramref name="httpClient"/> are <see langword="null"/>.
        /// </exception>
        public Connection(
            ProductHeaderValue productInformation,
            Uri uri,
            ICredentialStore credentialStore,
            HttpClient httpClient)
        {
            if (productInformation == null)
            {
                throw new ArgumentNullException(nameof(productInformation));
            }

            Uri = uri ?? throw new ArgumentNullException(nameof(uri));
            CredentialStore = credentialStore ?? throw new ArgumentNullException(nameof(credentialStore));
            HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            if (!Uri.IsAbsoluteUri)
            {
                throw new ArgumentException("The base address for the connection must be an absolute URI.", nameof(uri));
            }

            Accept = new MediaTypeWithQualityHeaderValue(DefaultMediaType);
            UserAgent = new ProductInfoHeaderValue(productInformation.Name, productInformation.Version);
        }

        /// <inheritdoc />
        public Uri Uri { get; }

        /// <summary>
        /// Gets the credential store for the connection.
        /// </summary>
        protected ICredentialStore CredentialStore { get; }

        /// <summary>
        /// Gets the HTTP client for the connection.
        /// </summary>
        protected HttpClient HttpClient { get; }

        /// <summary>
        /// Gets the Accept value for the connection.
        /// </summary>
        private MediaTypeWithQualityHeaderValue Accept { get; }

        /// <summary>
        /// Gets the User Agent value for the connection.
        /// </summary>
        private ProductInfoHeaderValue UserAgent { get; }

        /// <summary>
        /// Gets or sets the maximum number of times to retry a request when rate-limited (HTTP 403 or 429).
        /// </summary>
        /// <remarks>
        /// Set to 0 to disable retries. Defaults to 3.
        /// </remarks>
        public int MaxRetryCount { get; set; } = 3;

        /// <inheritdoc />
        public virtual async Task<string> Run(string query, CancellationToken cancellationToken = default)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var token = await CredentialStore.GetCredentials(cancellationToken).ConfigureAwait(false);

            var retryAttempt = 0;
            while (true)
            {
                using (var request = CreateRequest(token, query))
                using (var response = await HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false))
                {
                    if (retryAttempt < MaxRetryCount && IsRateLimitStatusCode(response.StatusCode))
                    {
                        var delay = GetRetryDelay(response, retryAttempt);
                        retryAttempt++;
                        await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
                        continue;
                    }

                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Returns the delay to wait before retrying a rate-limited request.
        /// </summary>
        /// <param name="response">The rate-limited HTTP response.</param>
        /// <param name="retryAttempt">The zero-based retry attempt index.</param>
        /// <returns>The <see cref="TimeSpan"/> to wait before the next retry.</returns>
        protected virtual TimeSpan GetRetryDelay(HttpResponseMessage response, int retryAttempt)
        {
            var retryAfter = response.Headers.RetryAfter;
            if (retryAfter != null)
            {
                if (retryAfter.Delta.HasValue)
                {
                    return retryAfter.Delta.Value;
                }

                if (retryAfter.Date.HasValue)
                {
                    var remaining = retryAfter.Date.Value - DateTimeOffset.UtcNow;
                    if (remaining > TimeSpan.Zero)
                    {
                        return remaining;
                    }

                    return TimeSpan.Zero;
                }
            }

            // Exponential backoff: 1s, 2s, 4s, …
            return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
        }

        private static bool IsRateLimitStatusCode(HttpStatusCode statusCode)
        {
            return statusCode == HttpStatusCode.Forbidden || (int)statusCode == 429;
        }

        private HttpRequestMessage CreateRequest(string token, string query)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Uri);

            try
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
                request.Headers.Accept.Add(Accept);
                request.Headers.UserAgent.Add(UserAgent);

                request.Content = new StringContent(query, Encoding.UTF8);

                return request;
            }
            catch (Exception)
            {
                request.Dispose();
                throw;
            }
        }
    }
}
