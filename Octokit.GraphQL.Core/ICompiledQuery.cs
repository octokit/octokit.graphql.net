using System;
using System.Collections.Generic;
using Octokit.GraphQL.Core;

namespace Octokit.GraphQL
{
    public interface ICompiledQuery<TResult>
    {
        IQueryRunner<TResult> Start(IConnection connection, Dictionary<string, object> variables);
    }
}
