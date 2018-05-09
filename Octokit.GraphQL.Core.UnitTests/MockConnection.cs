using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Octokit.GraphQL.Core.UnitTests
{
    class MockConnection : IConnection
    {
        readonly Func<string, IDictionary<string, string>, string> execute;

        public MockConnection(Func<string, IDictionary<string, string>, string> execute)
        {
            this.execute = execute;
        }

        public Uri Uri => null;

        public async Task<T> Run<T>(string query, Func<JObject, T> deserialize)
        {
            var result = await Run(query);
            return deserialize(JObject.Parse(result));
        }

        public Task<string> Run(string query)
        {
            var payload = JsonConvert.DeserializeObject<Payload>(query);
            return Task.FromResult(execute(payload.Query, payload.Variables));
        }

        class Payload
        {
            public string Query { get; set; }
            public Dictionary<string, string> Variables { get; set; }
        }
    }
}
