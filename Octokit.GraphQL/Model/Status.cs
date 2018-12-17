namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a commit status.
    /// </summary>
    public class Status : QueryableValue<Status>
    {
        internal Status(Expression expression) : base(expression)
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
        public StatusContext Context(Arg<string> name) => this.CreateMethodCall(x => x.Context(name), Octokit.GraphQL.Model.StatusContext.Create);

        /// <summary>
        /// The individual status contexts for this commit.
        /// </summary>
        public IQueryableList<StatusContext> Contexts => this.CreateProperty(x => x.Contexts);

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public ID Id { get; }

        /// <summary>
        /// The combined commit status.
        /// </summary>
        public StatusState State { get; }

        internal static Status Create(Expression expression)
        {
            return new Status(expression);
        }
    }
}