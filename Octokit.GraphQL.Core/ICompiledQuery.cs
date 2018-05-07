using System;
using System.Collections.Generic;
using Octokit.GraphQL.Core;

namespace Octokit.GraphQL
{
    public interface ICompiledQuery
    {
        IQueryRunner Start(IConnection connection, Dictionary<string, object> variables);
        string ToString(int indentation);
    }

    public interface ICompiledQuery<TResult> : ICompiledQuery
    {
        new IQueryRunner<TResult> Start(IConnection connection, Dictionary<string, object> variables);
    }
}
