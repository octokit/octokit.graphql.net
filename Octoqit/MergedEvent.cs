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
        /// Identifies the actor (user) associated with the event.
        /// </summary>
        public User Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.User.Create);

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
        /// Identifies the issue associated with the event.
        /// </summary>
        public Issue Issue => this.CreateProperty(x => x.Issue, Octokit.GraphQL.Issue.Create);

        /// <summary>
        /// Identifies the Ref associated with the `merge` event.
        /// </summary>
        public Ref MergeRef => this.CreateProperty(x => x.MergeRef, Octokit.GraphQL.Ref.Create);

        /// <summary>
        /// Identifies the name of the Ref associated with the `merge` event.
        /// </summary>
        public string MergeRefName { get; }

        /// <summary>
        /// Identifies the repository associated with the event.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Repository.Create);

        /// <summary>
        /// Identifies the event type associated with the event.
        /// </summary>
        public IssueEventType Type { get; }

        internal static MergedEvent Create(IQueryProvider provider, Expression expression)
        {
            return new MergedEvent(provider, expression);
        }
    }
}