using System;
using System.Threading.Tasks;

namespace Octokit.GraphQL
{
    public interface IConnection
    {
        Uri Uri { get; }

        Task<string> Run(string query);
    }
}