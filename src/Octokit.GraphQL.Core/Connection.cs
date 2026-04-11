using System;
using System.Linq;
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
        /// Gets or sets the maximum number of times to retry a request when rate-limited (HTTP 429, or HTTP 403 with rate-limit indicators).
        /// </summary>
        /// <remarks>
        /// <para>Defaults to <c>0</c> (retries disabled). Set to a positive value to enable automatic retries.</para>
        /// <para><strong>Warning:</strong> GraphQL uses HTTP POST for both queries and mutations. Enabling retries means
        /// rate-limited mutations may be replayed, which can cause duplicate side effects. Only enable retries when you
        /// are confident the requests being made are idempotent (e.g. read-only queries).</para>
        /// </remarks>
        public int MaxRetryCount { get; set; } = 0;

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
                    // Handle non-2xx rate-limit responses (HTTP 429, or HTTP 403 with rate-limit headers).
                    if (retryAttempt < MaxRetryCount && IsRateLimitResponse(response))
                    {
                        var delay = GetRetryDelay(response, retryAttempt);
                        retryAttempt++;
                        await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
                        continue;
                    }

                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    // GitHub's GraphQL API can return HTTP 200 with x-ratelimit-remaining: 0 and a
                    // RATE_LIMITED error in the response body when the primary rate-limit window is
                    // exhausted. The status-code-based check above cannot detect this case.
                    if (retryAttempt < MaxRetryCount && IsGraphQLRateLimitBody(response, body))
                    {
                        var delay = GetRetryDelay(response, retryAttempt);
                        retryAttempt++;
                        await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
                        continue;
                    }

                    return body;
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
            // Retry-After takes highest priority (used for secondary rate limits).
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
                    return remaining > TimeSpan.Zero ? remaining : TimeSpan.Zero;
                }
            }

            // X-RateLimit-Reset contains the Unix timestamp (seconds since epoch) when the primary
            // rate-limit window resets. Use it so retries wait until the window actually opens again.
            if (response.Headers.TryGetValues("X-RateLimit-Reset", out var resetValues))
            {
                foreach (var value in resetValues)
                {
                    if (long.TryParse(value, out var resetUnixSeconds))
                    {
                        var resetAt = DateTimeOffset.FromUnixTimeSeconds(resetUnixSeconds);
                        var remaining = resetAt - DateTimeOffset.UtcNow;
                        return remaining > TimeSpan.Zero ? remaining : TimeSpan.Zero;
                    }
                }
            }

            // Exponential backoff: 1s, 2s, 4s, …
            return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
        }

        /// <summary>
        /// Determines whether a successful (HTTP 200) response body contains a GraphQL rate-limit error.
        /// </summary>
        /// <remarks>
        /// GitHub's GraphQL API returns HTTP 200 with <c>x-ratelimit-remaining: 0</c> and a
        /// <c>RATE_LIMITED</c> error type in the body when the primary rate-limit window is exhausted.
        /// This is distinct from the non-2xx rate-limit responses detected by <see cref="IsRateLimitResponse"/>.
        /// </remarks>
        private static bool IsGraphQLRateLimitBody(HttpResponseMessage response, string body)
        {
            // x-ratelimit-remaining: 0 must be present; without it the 200 body is not a rate-limit response.
            if (!response.Headers.TryGetValues("X-RateLimit-Remaining", out var values) ||
                !values.Any(v => int.TryParse(v, out var remaining) && remaining == 0))
            {
                return false;
            }

            // The body must contain the RATE_LIMITED error type that GitHub sets for primary rate-limit
            // exhaustion. Match the JSON string value form to avoid false positives on body text.
            return body.IndexOf("\"RATE_LIMITED\"", StringComparison.Ordinal) >= 0;
        }

        /// <summary>
        /// Determines whether the response represents a rate-limit that should trigger a retry.
        /// </summary>
        /// <remarks>
        /// HTTP 429 (Too Many Requests) is always treated as a rate-limit response. HTTP 403 is
        /// used by GitHub for both rate-limiting and for other policy failures (missing scopes,
        /// SAML enforcement, repository permissions, etc.), so it is only treated as a retryable
        /// rate-limit when the response includes a clear rate-limit indicator: a <c>Retry-After</c>
        /// header (GitHub secondary rate limit) or an <c>X-RateLimit-Remaining: 0</c> header
        /// (GitHub primary rate limit).
        /// </remarks>
        private static bool IsRateLimitResponse(HttpResponseMessage response)
        {
            // 429 (TooManyRequests) is not defined in HttpStatusCode for netstandard2.0, so cast directly.
            if ((int)response.StatusCode == 429)
            {
                return true;
            }

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                // Retry-After header is set by GitHub for secondary rate limits.
                if (response.Headers.RetryAfter != null)
                {
                    return true;
                }

                // X-RateLimit-Remaining: 0 is set by GitHub for primary rate limits.
                if (response.Headers.TryGetValues("X-RateLimit-Remaining", out var values) &&
                    values.Any(v => int.TryParse(v, out var remaining) && remaining == 0))
                {
                    return true;
                }
            }

            return false;
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
