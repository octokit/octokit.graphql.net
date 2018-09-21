using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Octokit.GraphQL.IntegrationTests.Utilities;
using Octokit.GraphQL.Internal;
using Polly;
using Polly.CircuitBreaker;
using Xunit;
using static Octokit.GraphQL.Variable;

namespace Octokit.GraphQL.IntegrationTests.Configuration
{
    public static class HttpClientFactoryTests
    {
        [Fact]
        public static async Task Can_Configure_With_HttpClient_Factory()
        {
            // Arrange
            var services = new ServiceCollection();

            // Create a Polly policy that is a pre-isolated circuit breaker that is guaranteed to fail
            var policy = Policy.Handle<Exception>().CircuitBreakerAsync(1, TimeSpan.MaxValue);
            policy.Isolate();

            // Register Polly with DI and our pre-isolated circuit breaker
            services
                .AddPolicyRegistry()
                .Add(policy.PolicyKey, policy.AsAsyncPolicy<HttpResponseMessage>());

            // Register a credential store so we can make a connection, as
            // well as a custom typed client that performs queries with Connections.
            services.AddSingleton<ICredentialStore, InMemoryCredentialStore>((_) => new InMemoryCredentialStore(Helper.OAuthToken ?? " "));
            services.AddSingleton<MyQueryService>();

            // Register a typed HTTP client for use with the GitHub GraphQL API
            // that also configures Polly usage when performing HTTP calls.
            services
                .AddHttpClient("Octokit.GraphQL")
                .AddTypedClient((httpClient, serviceProvider) =>
                {
                    // Configure a connection using the HttpClient
                    var productInformation = new ProductHeaderValue("OctokitTests", "1.2.3");
                    var credentialStore = serviceProvider.GetRequiredService<ICredentialStore>();

                    return new Connection(productInformation, credentialStore, httpClient);
                })
                .ConfigureHttpClient((httpClient) =>
                {
                    // Apply user-configuration to HttpClient
                    httpClient.Timeout = TimeSpan.FromSeconds(10);
                })
                .AddPolicyHandlerFromRegistry(policy.PolicyKey);

            // Create the service provider for the registered services
            using (var provider = services.BuildServiceProvider())
            {
                var service = provider.GetRequiredService<MyQueryService>();

                // Act and Assert
                await Assert.ThrowsAsync<IsolatedCircuitException>(
                    () => service.GetRepositoryAsync(
                        "octokit",
                        "octokit.graphql.net",
                        repo => new
                        {
                            repo.Id,
                            repo.Name,
                            repo.Owner.Login,
                            repo.IsFork,
                            repo.IsPrivate,
                        }));
            }
        }

        private sealed class MyQueryService
        {
            private readonly Connection _connection;

            public MyQueryService(Connection connection)
            {
                _connection = connection;
            }

            public Task<T> GetRepositoryAsync<T>(string owner, string name, Expression<Func<Model.Repository, T>> selector)
            {
                var query = new Query()
                    .RepositoryOwner(Var("owner"))
                    .Repository(Var("name"))
                    .Select(selector)
                    .Compile();

                var variables = new Dictionary<string, object>
                {
                    { "owner", owner },
                    { "name", name },
                };

                return _connection.Run(query, variables);
            }
        }
    }
}
