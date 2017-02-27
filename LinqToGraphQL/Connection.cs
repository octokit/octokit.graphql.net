using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LinqToGraphQL.Builders;
using LinqToGraphQL.Deserializers;
using LinqToGraphQL.Serializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LinqToGraphQL
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
            var content = new StringContent(query.GetPayload());
            var response = await httpClient.PostAsync(uri, content);
            var data = await response.Content.ReadAsStringAsync();
            return deserializer.Deserialize(query, data);
        }

        private HttpClient CreateHttpClient()
        {
            var result = new HttpClient();
            var auth = new AuthenticationHeaderValue("bearer", token);
            var userAgent = new ProductInfoHeaderValue("Octoqit", "0.1");
            result.DefaultRequestHeaders.Authorization = auth;
            result.DefaultRequestHeaders.UserAgent.Add(userAgent);
            return result;
        }
    }
}
