using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LinqToGraphQL.Builders;
using LinqToGraphQL.Deserializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LinqToGraphQL
{
    public class Connection
    {
        private string uri;
        private string token;

        public Connection(string uri, string token)
        {
            this.uri = uri;
            this.token = token;
        }

        public async Task<T> Run<T>(IQueryable<T> query)
        {
            var builder = new QueryBuilder();
            var serializer = new QuerySerializer(2);
            var deserializer = new ResponseDeserializer();
            var operation = builder.Build(query.Expression);
            var payload = new Payload(serializer.Serialize(operation));
            var httpClient = CreateHttpClient();
            var payloadString = payload.ToString();
            var content = new StringContent(payloadString);
            var response = await httpClient.PostAsync(uri, content);
            var data = await response.Content.ReadAsStringAsync();
            var result = deserializer.Deserialize(operation, data);
            return (T)result;
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

        private class Payload
        {
            public Payload(string query)
            {
                Query = query;
            }

            public string Query { get; }

            public override string ToString()
            {
                return JsonConvert.SerializeObject(
                    this,
                    Formatting.Indented,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            }
        }
    }
}
