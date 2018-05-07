using System;

namespace Octokit.GraphQL.Core
{
    public interface IPagingConnection : IQueryableValue
    {
        IPageInfo PageInfo { get; }
    }

    public interface IPagingConnection<TNode> : IPagingConnection
    {
        IQueryableList<TNode> Nodes { get; }
    }
}