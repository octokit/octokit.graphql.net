namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a commit status.
    /// </summary>
    public class Status : QueryEntity
    {
        public Status(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// The commit this status is attached to.
        /// </summary>
        public Commit Commit => this.CreateProperty(x => x.Commit, Octokit.GraphQL.Model.Commit.Create);

        /// <summary>
        /// Looks up an individual status context by context name.
        /// </summary>
        /// <param name="name">The context name.</param>
        public StatusContext Context(string name) => this.CreateMethodCall(x => x.Context(name), Octokit.GraphQL.Model.StatusContext.Create);

        /// <summary>
        /// The individual status contexts for this commit.
        /// </summary>
        public IQueryable<StatusContext> Contexts => this.CreateProperty(x => x.Contexts);

        public string Id { get; }

        /// <summary>
        /// The combined commit status.
        /// </summary>
        public StatusState State { get; }

        internal static Status Create(IQueryProvider provider, Expression expression)
        {
            return new Status(provider, expression);
        }
    }
}