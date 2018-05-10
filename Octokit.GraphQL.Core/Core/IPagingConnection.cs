using System;

namespace Octokit.GraphQL.Core
{
    /// <summary>
    /// Denotes a GraphQL connection entity.
    /// </summary>
    public interface IPagingConnection : IQueryableValue
    {
        /// <summary>
        /// Gets the paging information field for the connection.
        /// </summary>
        IPageInfo PageInfo { get; }
    }

    /// <summary>
    /// Denotes a GraphQL connection entity.
    /// </summary>
    /// <typeparam name="TNode">The node type.</typeparam>
    public interface IPagingConnection<TNode> : IPagingConnection
    {
        /// <summary>
        /// Gets the connection nodes.
        /// </summary>
        IQueryableList<TNode> Nodes { get; }
    }
}