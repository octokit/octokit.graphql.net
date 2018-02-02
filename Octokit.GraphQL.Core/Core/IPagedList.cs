using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Octokit.GraphQL.Core
{
    public interface IPagedList<out T>
        where T : IPagingConnection
    {
        Expression Expression { get; }
        LambdaExpression Selector { get; }
    }
}
