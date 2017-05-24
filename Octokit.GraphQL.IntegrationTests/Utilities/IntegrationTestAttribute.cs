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
            return Helper.HasCredentials
                ? new[] { new XunitTestCase(_diagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), testMethod) }
                : Enumerable.Empty<IXunitTestCase>();
        }
    }

    [XunitTestCaseDiscoverer("Octokit.GraphQL.IntegrationTests.IntegrationTestDiscoverer", "Octokit.GraphQL.IntegrationTests")]
    public class IntegrationTestAttribute : FactAttribute
    {
    }
}
