using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        [Fact]
        public static async Task Run_Retries_On_200_With_GraphQL_Rate_Limit_Error()
        {
            // GitHub GraphQL returns HTTP 200 with x-ratelimit-remaining: 0 and a RATE_LIMITED error
            // in the body when the primary rate-limit window is exhausted.  The status-code path alone
            // would miss this; the body-inspection path must catch it.
            const string rateLimitBody = "{\"errors\":[{\"message\":\"API rate limit exceeded\",\"type\":\"RATE_LIMITED\"}]}";
            var rateLimitResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(rateLimitBody),
            };
            rateLimitResponse.Headers.Add("X-RateLimit-Remaining", "0");

            var handler = new SequentialMockHttpMessageHandler(new[]
            {
                rateLimitResponse,
                new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(string.Empty) },
            });
            var httpClient = new HttpClient(handler);
            var connection = new ZeroDelayConnection(ProductInformation, CredentialStore, httpClient) { MaxRetryCount = 3 };
            var query = "{}";

            await connection.Run(query);

            Assert.Equal(2, handler.CallCount);
        }

        [Fact]
        public static async Task Run_Does_Not_Retry_200_Without_Rate_Limit_Header()
        {
            // A 200 body that mentions "RATE_LIMITED" but has no x-ratelimit-remaining: 0 header
            // should NOT be retried — the header is required to distinguish a genuine rate limit.
            const string body = "{\"errors\":[{\"message\":\"some error\",\"type\":\"RATE_LIMITED\"}]}";
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(body),
            };
            // No X-RateLimit-Remaining header added.

            var handler = new SequentialMockHttpMessageHandler(new[]
            {
                response,
                new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(string.Empty) },
            });
            var httpClient = new HttpClient(handler);
            var connection = new ZeroDelayConnection(ProductInformation, CredentialStore, httpClient) { MaxRetryCount = 3 };
            var query = "{}";

            // Should return the first response's body immediately without retrying.
            var result = await connection.Run(query);
            Assert.Equal(body, result);
            Assert.Equal(1, handler.CallCount);
        }

        [Fact]
        public static async Task Run_Does_Not_Retry_200_With_Rate_Limit_Header_But_No_Rate_Limit_Error()
        {
            // A 200 with x-ratelimit-remaining: 0 but no RATE_LIMITED error type in the body
            // (e.g. the last query succeeded but drained the quota) must NOT be retried.
            const string successBody = "{\"data\":{\"viewer\":{\"login\":\"monalisa\"}}}";
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(successBody),
            };
            response.Headers.Add("X-RateLimit-Remaining", "0");

            var handler = new SequentialMockHttpMessageHandler(new[]
            {
                response,
                new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(string.Empty) },
            });
            var httpClient = new HttpClient(handler);
            var connection = new ZeroDelayConnection(ProductInformation, CredentialStore, httpClient) { MaxRetryCount = 3 };
            var query = "{}";

            var result = await connection.Run(query);
            Assert.Equal(successBody, result);
            Assert.Equal(1, handler.CallCount);
        }

        [Fact]
        public static async Task Run_Respects_X_RateLimit_Reset_On_200_Rate_Limit_Body()
        {
            // When a 200 + RATE_LIMITED body also carries X-RateLimit-Reset, the delay should be
            // derived from that header (not fall back to short exponential backoff).
            var testStartTime = DateTimeOffset.UtcNow;
            var resetAt = testStartTime.AddSeconds(30);
            var resetUnix = resetAt.ToUnixTimeSeconds().ToString();

            const string rateLimitBody = "{\"errors\":[{\"message\":\"API rate limit exceeded\",\"type\":\"RATE_LIMITED\"}]}";
            var rateLimitResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(rateLimitBody),
            };
            rateLimitResponse.Headers.Add("X-RateLimit-Remaining", "0");
            rateLimitResponse.Headers.Add("X-RateLimit-Reset", resetUnix);

            var handler = new SequentialMockHttpMessageHandler(new[]
            {
                rateLimitResponse,
                new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(string.Empty) },
            });
            var httpClient = new HttpClient(handler);
            var observedDelays = new List<TimeSpan>();
            var connection = new ObservableDelayConnection(ProductInformation, CredentialStore, httpClient, observedDelays) { MaxRetryCount = 3 };
            var query = "{}";

            await connection.Run(query);
            var testEndTime = DateTimeOffset.UtcNow;

            Assert.Single(observedDelays);
            var minExpected = resetAt - testEndTime;
            var maxExpected = resetAt - testStartTime;
            Assert.True(observedDelays[0] >= (minExpected > TimeSpan.Zero ? minExpected : TimeSpan.Zero),
                $"Expected delay ≥ {minExpected}, got {observedDelays[0]}");
            Assert.True(observedDelays[0] <= maxExpected,
                $"Expected delay ≤ {maxExpected}, got {observedDelays[0]}");
        }

        [Fact]
        public static async Task Run_Retries_On_429()
        {
            var handler = new SequentialMockHttpMessageHandler(new[]
            {
                new HttpResponseMessage((HttpStatusCode)429) { Content = new StringContent(string.Empty) },
                new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(string.Empty) },
            });
            var httpClient = new HttpClient(handler);
            var connection = new ZeroDelayConnection(ProductInformation, CredentialStore, httpClient) { MaxRetryCount = 3 };
            var query = "{}";

            await connection.Run(query);

            Assert.Equal(2, handler.CallCount);
        }

        [Fact]
        public static async Task Run_Retries_On_403_With_Retry_After_Header()
        {
            var rateLimitResponse = new HttpResponseMessage(HttpStatusCode.Forbidden)
            {
                Content = new StringContent(string.Empty),
            };
            rateLimitResponse.Headers.RetryAfter = new RetryConditionHeaderValue(TimeSpan.FromSeconds(1));

            var handler = new SequentialMockHttpMessageHandler(new[]
            {
                rateLimitResponse,
                new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(string.Empty) },
            });
            var httpClient = new HttpClient(handler);
            var connection = new ZeroDelayConnection(ProductInformation, CredentialStore, httpClient) { MaxRetryCount = 3 };
            var query = "{}";

            await connection.Run(query);

            Assert.Equal(2, handler.CallCount);
        }

        [Fact]
        public static async Task Run_Retries_On_403_With_X_RateLimit_Remaining_Zero()
        {
            var rateLimitResponse = new HttpResponseMessage(HttpStatusCode.Forbidden)
            {
                Content = new StringContent(string.Empty),
            };
            rateLimitResponse.Headers.Add("X-RateLimit-Remaining", "0");

            var handler = new SequentialMockHttpMessageHandler(new[]
            {
                rateLimitResponse,
                new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(string.Empty) },
            });
            var httpClient = new HttpClient(handler);
            var connection = new ZeroDelayConnection(ProductInformation, CredentialStore, httpClient) { MaxRetryCount = 3 };
            var query = "{}";

            await connection.Run(query);

            Assert.Equal(2, handler.CallCount);
        }

        [Fact]
        public static async Task Run_Does_Not_Retry_403_Without_Rate_Limit_Headers()
        {
            // GitHub uses 403 for scope/SAML/permission errors in addition to rate limits.
            // A bare 403 with no rate-limit indicators must NOT be retried.
            var handler = new SequentialMockHttpMessageHandler(new[]
            {
                new HttpResponseMessage(HttpStatusCode.Forbidden) { Content = new StringContent(string.Empty) },
                new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(string.Empty) },
            });
            var httpClient = new HttpClient(handler);
            var connection = new ZeroDelayConnection(ProductInformation, CredentialStore, httpClient) { MaxRetryCount = 3 };
            var query = "{}";

            await Assert.ThrowsAsync<HttpRequestException>(() => connection.Run(query));
            Assert.Equal(1, handler.CallCount); // no retry for a plain 403
        }

        [Fact]
        public static async Task Run_Does_Not_Retry_By_Default()
        {
            // MaxRetryCount defaults to 0 because GraphQL POST requests may be mutations with side effects.
            var handler = new SequentialMockHttpMessageHandler(new[]
            {
                new HttpResponseMessage((HttpStatusCode)429) { Content = new StringContent(string.Empty) },
                new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(string.Empty) },
            });
            var httpClient = new HttpClient(handler);
            var connection = new ZeroDelayConnection(ProductInformation, CredentialStore, httpClient);
            var query = "{}";

            await Assert.ThrowsAsync<HttpRequestException>(() => connection.Run(query));
            Assert.Equal(1, handler.CallCount); // default MaxRetryCount=0, no retry
        }

        [Fact]
        public static async Task Run_Throws_After_Max_Retries_On_Rate_Limit_Status_Code()
        {
            var responses = new[]
            {
                new HttpResponseMessage((HttpStatusCode)429) { Content = new StringContent(string.Empty) },
                new HttpResponseMessage((HttpStatusCode)429) { Content = new StringContent(string.Empty) },
                new HttpResponseMessage((HttpStatusCode)429) { Content = new StringContent(string.Empty) },
                new HttpResponseMessage((HttpStatusCode)429) { Content = new StringContent(string.Empty) },
            };
            var handler = new SequentialMockHttpMessageHandler(responses);
            var httpClient = new HttpClient(handler);
            var connection = new ZeroDelayConnection(ProductInformation, CredentialStore, httpClient) { MaxRetryCount = 3 };
            var query = "{}";

            await Assert.ThrowsAsync<HttpRequestException>(() => connection.Run(query));
            Assert.Equal(4, handler.CallCount); // 1 initial + 3 retries
        }

        [Fact]
        public static async Task Run_Does_Not_Retry_Non_Rate_Limit_Errors_When_Retry_Is_Enabled()
        {
            var handler = new SequentialMockHttpMessageHandler(new[]
            {
                new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(string.Empty) },
                new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(string.Empty) },
            });
            var httpClient = new HttpClient(handler);
            var connection = new ZeroDelayConnection(ProductInformation, CredentialStore, httpClient) { MaxRetryCount = 3 };
            var query = "{}";

            await Assert.ThrowsAsync<HttpRequestException>(() => connection.Run(query));
            Assert.Equal(1, handler.CallCount); // no retry for 400
        }

        [Fact]
        public static async Task Run_Respects_Retry_After_Delta_Header()
        {
            var retryAfterDelay = TimeSpan.FromSeconds(5);
            var rateLimitResponse = new HttpResponseMessage(HttpStatusCode.Forbidden)
            {
                Content = new StringContent(string.Empty),
            };
            rateLimitResponse.Headers.RetryAfter = new RetryConditionHeaderValue(retryAfterDelay);

            var handler = new SequentialMockHttpMessageHandler(new[]
            {
                rateLimitResponse,
                new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(string.Empty) },
            });
            var httpClient = new HttpClient(handler);
            var observedDelays = new List<TimeSpan>();
            var connection = new ObservableDelayConnection(ProductInformation, CredentialStore, httpClient, observedDelays) { MaxRetryCount = 3 };
            var query = "{}";

            await connection.Run(query);

            Assert.Single(observedDelays);
            Assert.Equal(retryAfterDelay, observedDelays[0]);
        }

        [Fact]
        public static async Task Run_Respects_X_RateLimit_Reset_Header()
        {
            // X-RateLimit-Reset is a Unix timestamp; the client should wait until that time, not fall back
            // to short exponential backoff, to avoid exhausting retries before the window resets.
            var testStartTime = DateTimeOffset.UtcNow;
            var resetAt = testStartTime.AddSeconds(30);
            var resetUnix = resetAt.ToUnixTimeSeconds().ToString();

            var rateLimitResponse = new HttpResponseMessage(HttpStatusCode.Forbidden)
            {
                Content = new StringContent(string.Empty),
            };
            rateLimitResponse.Headers.Add("X-RateLimit-Remaining", "0");
            rateLimitResponse.Headers.Add("X-RateLimit-Reset", resetUnix);

            var handler = new SequentialMockHttpMessageHandler(new[]
            {
                rateLimitResponse,
                new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(string.Empty) },
            });
            var httpClient = new HttpClient(handler);
            var observedDelays = new List<TimeSpan>();
            var connection = new ObservableDelayConnection(ProductInformation, CredentialStore, httpClient, observedDelays) { MaxRetryCount = 3 };
            var query = "{}";

            await connection.Run(query);
            var testEndTime = DateTimeOffset.UtcNow;

            Assert.Single(observedDelays);
            // The delay should be the time remaining until the reset timestamp. Bracket with the
            // actual elapsed test time so the assertion is deterministic regardless of how fast or
            // slow the test runs.
            var minExpected = resetAt - testEndTime;
            var maxExpected = resetAt - testStartTime;
            Assert.True(observedDelays[0] >= (minExpected > TimeSpan.Zero ? minExpected : TimeSpan.Zero),
                $"Expected delay ≥ {minExpected}, got {observedDelays[0]}");
            Assert.True(observedDelays[0] <= maxExpected,
                $"Expected delay ≤ {maxExpected}, got {observedDelays[0]}");
        }

        [Fact]
        public static async Task Run_Uses_Exponential_Backoff_When_No_Retry_After_Header()
        {
            // Use 429 (unconditional rate limit) so that no Retry-After header is needed.
            var handler = new SequentialMockHttpMessageHandler(new[]
            {
                new HttpResponseMessage((HttpStatusCode)429) { Content = new StringContent(string.Empty) },
                new HttpResponseMessage((HttpStatusCode)429) { Content = new StringContent(string.Empty) },
                new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(string.Empty) },
            });
            var httpClient = new HttpClient(handler);
            var observedDelays = new List<TimeSpan>();
            var connection = new ObservableDelayConnection(ProductInformation, CredentialStore, httpClient, observedDelays) { MaxRetryCount = 3 };
            var query = "{}";

            await connection.Run(query);

            Assert.Equal(2, observedDelays.Count);
            Assert.Equal(TimeSpan.FromSeconds(1), observedDelays[0]); // attempt 0: 2^0 = 1s
            Assert.Equal(TimeSpan.FromSeconds(2), observedDelays[1]); // attempt 1: 2^1 = 2s
        }

        private static HttpClient CreateFakeHttpClient(Action<HttpRequestMessage, CancellationToken> inspector = null, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var handler = new MockHttpMessageHandler(inspector, statusCode);
            return new HttpClient(handler);
        }

        /// <summary>A <see cref="Connection"/> subclass that returns zero-duration delays to keep tests fast.</summary>
        private sealed class ZeroDelayConnection : Connection
        {
            public ZeroDelayConnection(ProductHeaderValue productInformation, ICredentialStore credentialStore, HttpClient httpClient)
                : base(productInformation, credentialStore, httpClient)
            {
            }

            protected override TimeSpan GetRetryDelay(HttpResponseMessage response, int retryAttempt) => TimeSpan.Zero;
        }

        /// <summary>A <see cref="Connection"/> subclass that records computed delays instead of actually sleeping.</summary>
        private sealed class ObservableDelayConnection : Connection
        {
            private readonly List<TimeSpan> _observedDelays;

            public ObservableDelayConnection(ProductHeaderValue productInformation, ICredentialStore credentialStore, HttpClient httpClient, List<TimeSpan> observedDelays)
                : base(productInformation, credentialStore, httpClient)
            {
                _observedDelays = observedDelays;
            }

            protected override TimeSpan GetRetryDelay(HttpResponseMessage response, int retryAttempt)
            {
                var delay = base.GetRetryDelay(response, retryAttempt);
                _observedDelays.Add(delay);
                return TimeSpan.Zero; // don't actually sleep
            }
        }

        /// <summary>Returns pre-configured HTTP responses in sequence, repeating the last response once the list is exhausted.</summary>
        private sealed class SequentialMockHttpMessageHandler : HttpMessageHandler
        {
            private readonly IList<HttpResponseMessage> _responses;
            private int _callCount;

            public SequentialMockHttpMessageHandler(IList<HttpResponseMessage> responses)
            {
                _responses = responses;
            }

            public int CallCount => _callCount;

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var index = _callCount++;
                // Once the pre-configured responses are exhausted, repeat the last one.
                var responseIndex = index < _responses.Count ? index : _responses.Count - 1;
                return Task.FromResult(_responses[responseIndex]);
            }
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
