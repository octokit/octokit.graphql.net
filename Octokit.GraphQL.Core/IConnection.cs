using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Octokit.GraphQL
{
    public interface IConnection
    {
        Uri Uri { get; }

        Task<T> Run<T>(string query, Func<JObject, T> deserialize);
        Task<string> Run(string query);
    }
}