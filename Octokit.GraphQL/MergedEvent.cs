namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'merged' event on a given pull request.
    /// </summary>
    public class MergedEvent : QueryEntity
    {
        public MergedEvent(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the 'merge' event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the commit associated with the `merge` event.
        /// </summary>
        public Commit Commit => this.CreateProperty(x => x.Commit, Octokit.GraphQL.Commit.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        public string Id { get; }

        /// <summary>
        /// Identifies the Ref associated with the `merge` event.
        /// </summary>
        public Ref MergeRef => this.CreateProperty(x => x.MergeRef, Octokit.GraphQL.Ref.Create);

        /// <summary>
        /// Identifies the name of the Ref associated with the `merge` event.
        /// </summary>
        public string MergeRefName { get; }

        /// <summary>
        /// PullRequest referenced by event.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.PullRequest.Create);

        internal static MergedEvent Create(IQueryProvider provider, Expression expression)
        {
            return new MergedEvent(provider, expression);
        }
    }
}