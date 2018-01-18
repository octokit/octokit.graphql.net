using System;
using System.Linq;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core
{
    public interface IQueryableValue<out T>
    {
        Expression Expression { get; }
        IQueryProvider Provider { get; }
    }
}
