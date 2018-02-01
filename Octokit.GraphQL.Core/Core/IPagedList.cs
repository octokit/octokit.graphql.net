using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Octokit.GraphQL.Core
{
    public interface IPagedList<T>
        where T : IQueryableList
    {
        Expression Expression { get; }
        MethodInfo Method { get; }
        LambdaExpression Selector { get; }
    }
}
