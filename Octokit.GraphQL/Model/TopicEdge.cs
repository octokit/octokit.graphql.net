namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class TopicEdge : QueryableValue<TopicEdge>
    {
        internal TopicEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public Topic Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.Topic.Create);

        internal static TopicEdge Create(Expression expression)
        {
            return new TopicEdge(expression);
        }
    }
}