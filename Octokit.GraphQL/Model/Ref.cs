namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a Git reference.
    /// </summary>
    public class Ref : QueryableValue<Ref>
    {
        public Ref(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A list of pull requests with this ref as the head ref.
        /// </summary>
        /// <param name="states">A list of states to filter the pull requests by.</param>
        /// <param name="labels">A list of label names to filter the pull requests by.</param>
        /// <param name="headRefName">The head ref name to filter the pull requests by.</param>
        /// <param name="baseRefName">The base ref name to filter the pull requests by.</param>
        /// <param name="orderBy">Ordering options for pull requests returned from the connection.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        public PullRequestConnection AssociatedPullRequests(Arg<IEnumerable<PullRequestState>>? states = null, Arg<IEnumerable<string>>? labels = null, Arg<string>? headRefName = null, Arg<string>? baseRefName = null, Arg<IssueOrder>? orderBy = null, Arg<string>? after = null, Arg<string>? before = null, Arg<int>? first = null, Arg<int>? last = null) => this.CreateMethodCall(x => x.AssociatedPullRequests(states, labels, headRefName, baseRefName, orderBy, after, before, first, last), Octokit.GraphQL.Model.PullRequestConnection.Create);

        public ID Id { get; }

        /// <summary>
        /// The ref name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The ref's prefix, such as `refs/heads/` or `refs/tags/`.
        /// </summary>
        public string Prefix { get; }

        /// <summary>
        /// The repository the ref belongs to.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The object the ref points to.
        /// </summary>
        public IGitObject Target => this.CreateProperty(x => x.Target, Octokit.GraphQL.Model.Internal.StubIGitObject.Create);

        internal static Ref Create(Expression expression)
        {
            return new Ref(expression);
        }
    }
}