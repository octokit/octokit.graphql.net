using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Octokit.GraphQL.IntegrationTests.Utilities
{
    public class IntegrationTestDiscoverer : IXunitTestCaseDiscoverer
    {
        readonly IMessageSink _diagnosticMessageSink;

        public IntegrationTestDiscoverer(IMessageSink diagnosticMessageSink)
        {
            this._diagnosticMessageSink = diagnosticMessageSink;
        }

        public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            var requiredEnvironmentVariable = factAttribute.GetNamedArgument<string>(nameof(ManualIntegrationTestAttribute.RequiredEnvironmentVariable));
            var isEnabled = string.IsNullOrWhiteSpace(requiredEnvironmentVariable) ||
                !string.IsNullOrWhiteSpace(System.Environment.GetEnvironmentVariable(requiredEnvironmentVariable));

            return Helper.HasCredentials
                && isEnabled
                ? new[] { new XunitTestCase(_diagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), TestMethodDisplayOptions.None, testMethod) }
                : Enumerable.Empty<IXunitTestCase>();
        }
    }

    [XunitTestCaseDiscoverer("Octokit.GraphQL.IntegrationTests.Utilities.IntegrationTestDiscoverer", "Octokit.GraphQL.IntegrationTests")]
    public class IntegrationTestAttribute : FactAttribute
    {
        public string RequiredEnvironmentVariable { get; set; }
    }

    public class ManualIntegrationTestAttribute : IntegrationTestAttribute
    {
    }
}
