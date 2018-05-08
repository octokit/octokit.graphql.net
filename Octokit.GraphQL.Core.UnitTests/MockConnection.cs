using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Octokit.GraphQL.Core.UnitTests
{
    class MockConnection : IConnection
    {
        readonly Func<string, string> execute;

        public MockConnection(Func<string, string> execute)
        {
            this.execute = execute;
        }

        public Uri Uri => null;

        public Task<T> Run<T>(string query, Func<JObject, T> deserialize)
        {
            return Task.FromResult(deserialize(JObject.Parse(execute(query))));
        }

        public Task<string> Run(string query)
        {
            return Task.FromResult(execute(query));
        }
    }
}
