using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit.GraphQL
{
    public interface IConnection
    {
        Uri Uri { get; }

        Task<T> Run<T>(CompiledQuery<T> query, IDictionary<string, object> variables = null);
    }
}