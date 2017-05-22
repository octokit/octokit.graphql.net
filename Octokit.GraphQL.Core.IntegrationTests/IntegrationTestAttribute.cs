using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Octokit.GraphQL.Core.IntegrationTests
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
            return !Helper.HasCredentials
                ? Enumerable.Empty<IXunitTestCase>()
                : new[] { new XunitTestCase(_diagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(), testMethod) };
        }
    }

    [XunitTestCaseDiscoverer("Octokit.Tests.Integration.IntegrationTestDiscoverer", "Octokit.Tests.Integration")]
    public class IntegrationTestAttribute : FactAttribute
    {
    }

    public class SampleIntegrationTest
    {
        [IntegrationTest]
        public void Blah()
        {
            
        }
    }
}
