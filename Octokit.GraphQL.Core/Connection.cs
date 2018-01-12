using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.Core.Deserializers;
using Octokit.GraphQL.Core.Serializers;

namespace Octokit.GraphQL
{
    public class Connection
    {
        private static readonly QueryBuilder builder = new QueryBuilder();
        private static readonly QuerySerializer serializer = new QuerySerializer();
        private static readonly ResponseDeserializer deserializer = new ResponseDeserializer();
        private string uri;
        private string token;

        public Connection(string uri, string token)
        {
            this.uri = uri;
            this.token = token;
        }

        public async Task<IEnumerable<T>> Run<T>(IQueryable<T> queryable)
        {
            var query = builder.Build(queryable);
            var httpClient = CreateHttpClient();
            var payload = query.GetPayload();
            var content = new StringContent(payload);
            var response = await httpClient.PostAsync(uri, content);
            var data = await response.Content.ReadAsStringAsync();
            return deserializer.Deserialize(query, data);
        }

        private HttpClient CreateHttpClient()
        {
            var result = new HttpClient();
            var auth = new AuthenticationHeaderValue("bearer", token);
            var userAgent = new ProductInfoHeaderValue("Octokit.GraphQL", "0.1");
            result.DefaultRequestHeaders.Authorization = auth;
            result.DefaultRequestHeaders.UserAgent.Add(userAgent);
            return result;
        }
    }
}
