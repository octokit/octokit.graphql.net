namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An item in an issue timeline
    /// </summary>
    public class IssueTimelineItem : QueryEntity, IUnion
    {
        public IssueTimelineItem(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Represents a Git commit.
        /// </summary>
        public Commit Commit => this.CreateProperty(x => x.Commit, Octokit.GraphQL.Commit.Create);

        /// <summary>
        /// Represents a comment on an Issue.
        /// </summary>
        public IssueComment IssueComment => this.CreateProperty(x => x.IssueComment, Octokit.GraphQL.IssueComment.Create);

        /// <summary>
        /// Represents a 'closed' event on any `Closable`.
        /// </summary>
        public ClosedEvent ClosedEvent => this.CreateProperty(x => x.ClosedEvent, Octokit.GraphQL.ClosedEvent.Create);

        /// <summary>
        /// Represents a 'reopened' event on any `Closable`.
        /// </summary>
        public ReopenedEvent ReopenedEvent => this.CreateProperty(x => x.ReopenedEvent, Octokit.GraphQL.ReopenedEvent.Create);

        /// <summary>
        /// Represents a 'subscribed' event on a given `Subscribable`.
        /// </summary>
        public SubscribedEvent SubscribedEvent => this.CreateProperty(x => x.SubscribedEvent, Octokit.GraphQL.SubscribedEvent.Create);

        /// <summary>
        /// Represents an 'unsubscribed' event on a given `Subscribable`.
        /// </summary>
        public UnsubscribedEvent UnsubscribedEvent => this.CreateProperty(x => x.UnsubscribedEvent, Octokit.GraphQL.UnsubscribedEvent.Create);

        /// <summary>
        /// Represents a 'referenced' event on a given `ReferencedSubject`.
        /// </summary>
        public ReferencedEvent ReferencedEvent => this.CreateProperty(x => x.ReferencedEvent, Octokit.GraphQL.ReferencedEvent.Create);

        /// <summary>
        /// Represents an 'assigned' event on any assignable object.
        /// </summary>
        public AssignedEvent AssignedEvent => this.CreateProperty(x => x.AssignedEvent, Octokit.GraphQL.AssignedEvent.Create);

        /// <summary>
        /// Represents an 'unassigned' event on any assignable object.
        /// </summary>
        public UnassignedEvent UnassignedEvent => this.CreateProperty(x => x.UnassignedEvent, Octokit.GraphQL.UnassignedEvent.Create);

        /// <summary>
        /// Represents a 'labeled' event on a given issue or pull request.
        /// </summary>
        public LabeledEvent LabeledEvent => this.CreateProperty(x => x.LabeledEvent, Octokit.GraphQL.LabeledEvent.Create);

        /// <summary>
        /// Represents an 'unlabeled' event on a given issue or pull request.
        /// </summary>
        public UnlabeledEvent UnlabeledEvent => this.CreateProperty(x => x.UnlabeledEvent, Octokit.GraphQL.UnlabeledEvent.Create);

        /// <summary>
        /// Represents a 'milestoned' event on a given issue or pull request.
        /// </summary>
        public MilestonedEvent MilestonedEvent => this.CreateProperty(x => x.MilestonedEvent, Octokit.GraphQL.MilestonedEvent.Create);

        /// <summary>
        /// Represents a 'demilestoned' event on a given issue or pull request.
        /// </summary>
        public DemilestonedEvent DemilestonedEvent => this.CreateProperty(x => x.DemilestonedEvent, Octokit.GraphQL.DemilestonedEvent.Create);

        /// <summary>
        /// Represents a 'renamed' event on a given issue or pull request
        /// </summary>
        public RenamedTitleEvent RenamedTitleEvent => this.CreateProperty(x => x.RenamedTitleEvent, Octokit.GraphQL.RenamedTitleEvent.Create);

        /// <summary>
        /// Represents a 'locked' event on a given issue or pull request.
        /// </summary>
        public LockedEvent LockedEvent => this.CreateProperty(x => x.LockedEvent, Octokit.GraphQL.LockedEvent.Create);

        /// <summary>
        /// Represents an 'unlocked' event on a given issue or pull request.
        /// </summary>
        public UnlockedEvent UnlockedEvent => this.CreateProperty(x => x.UnlockedEvent, Octokit.GraphQL.UnlockedEvent.Create);

        internal static IssueTimelineItem Create(IQueryProvider provider, Expression expression)
        {
            return new IssueTimelineItem(provider, expression);
        }
    }
}