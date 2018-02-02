using System;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core
{
    public interface IPagedList<out T>
        where T : IPagingConnection
    {
        Expression Expression { get; }
    }
}
