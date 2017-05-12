namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'milestoned' event on a given issue or pull request.
    /// </summary>
    public class MilestonedEvent : QueryEntity
    {
        public MilestonedEvent(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the actor (user) associated with the event.
        /// </summary>
        public User Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.User.Create);

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
        /// Identifies the milestone title associated with the 'milestoned' event.
        /// </summary>
        public string MilestoneTitle { get; }

        /// <summary>
        /// Identifies the repository associated with the event.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Repository.Create);

        /// <summary>
        /// Identifies the event type associated with the event.
        /// </summary>
        public IssueEventType Type { get; }

        internal static MilestonedEvent Create(IQueryProvider provider, Expression expression)
        {
            return new MilestonedEvent(provider, expression);
        }
    }
}