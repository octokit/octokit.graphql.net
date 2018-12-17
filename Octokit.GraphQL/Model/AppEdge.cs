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
    public class AppEdge : QueryableValue<AppEdge>
    {
        internal AppEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public App Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.App.Create);

        internal static AppEdge Create(Expression expression)
        {
            return new AppEdge(expression);
        }
    }
}