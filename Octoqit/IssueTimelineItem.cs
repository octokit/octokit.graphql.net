namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// An item in an issue/pull request timeline
    /// </summary>
    public class IssueTimelineItem : QueryEntity, IUnion
    {
        public IssueTimelineItem(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Represents a Git commit.
        /// </summary>
        public Commit Commit => this.CreateProperty(x => x.Commit, Octoqit.Commit.Create);

        /// <summary>
        /// A review object for a given pull request.
        /// </summary>
        public PullRequestReview PullRequestReview => this.CreateProperty(x => x.PullRequestReview, Octoqit.PullRequestReview.Create);

        /// <summary>
        /// A threaded list of comments for a given pull request.
        /// </summary>
        public PullRequestReviewThread PullRequestReviewThread => this.CreateProperty(x => x.PullRequestReviewThread, Octoqit.PullRequestReviewThread.Create);

        /// <summary>
        /// A review comment associated with a given repository pull request.
        /// </summary>
        public PullRequestReviewComment PullRequestReviewComment => this.CreateProperty(x => x.PullRequestReviewComment, Octoqit.PullRequestReviewComment.Create);

        /// <summary>
        /// Represents a comment on an Issue.
        /// </summary>
        public IssueComment IssueComment => this.CreateProperty(x => x.IssueComment, Octoqit.IssueComment.Create);

        /// <summary>
        /// Represents a 'closed' event on a given issue or pull request.
        /// </summary>
        public ClosedEvent ClosedEvent => this.CreateProperty(x => x.ClosedEvent, Octoqit.ClosedEvent.Create);

        /// <summary>
        /// Represents a 'reopened' event on a given issue or pull request.
        /// </summary>
        public ReopenedEvent ReopenedEvent => this.CreateProperty(x => x.ReopenedEvent, Octoqit.ReopenedEvent.Create);

        /// <summary>
        /// Represents a 'subscribed' event on a given issue or pull request.
        /// </summary>
        public SubscribedEvent SubscribedEvent => this.CreateProperty(x => x.SubscribedEvent, Octoqit.SubscribedEvent.Create);

        /// <summary>
        /// Represents a 'unsubscribed' event on a given issue or pull request.
        /// </summary>
        public UnsubscribedEvent UnsubscribedEvent => this.CreateProperty(x => x.UnsubscribedEvent, Octoqit.UnsubscribedEvent.Create);

        /// <summary>
        /// Represents a 'merged' event on a given pull request.
        /// </summary>
        public MergedEvent MergedEvent => this.CreateProperty(x => x.MergedEvent, Octoqit.MergedEvent.Create);

        /// <summary>
        /// Represents a 'referenced' event on a given issue or pull request.
        /// </summary>
        public ReferencedEvent ReferencedEvent => this.CreateProperty(x => x.ReferencedEvent, Octoqit.ReferencedEvent.Create);

        /// <summary>
        /// Represents a 'mentioned' event on a given issue or pull request.
        /// </summary>
        public MentionedEvent MentionedEvent => this.CreateProperty(x => x.MentionedEvent, Octoqit.MentionedEvent.Create);

        /// <summary>
        /// Represents an 'assigned' event on a given issue or pull request.
        /// </summary>
        public AssignedEvent AssignedEvent => this.CreateProperty(x => x.AssignedEvent, Octoqit.AssignedEvent.Create);

        /// <summary>
        /// Represents a 'unassigned' event on a given issue or pull request.
        /// </summary>
        public UnassignedEvent UnassignedEvent => this.CreateProperty(x => x.UnassignedEvent, Octoqit.UnassignedEvent.Create);

        /// <summary>
        /// Represents a 'labeled' event on a given issue or pull request.
        /// </summary>
        public LabeledEvent LabeledEvent => this.CreateProperty(x => x.LabeledEvent, Octoqit.LabeledEvent.Create);

        /// <summary>
        /// Represents a 'unlabeled' event on a given issue or pull request.
        /// </summary>
        public UnlabeledEvent UnlabeledEvent => this.CreateProperty(x => x.UnlabeledEvent, Octoqit.UnlabeledEvent.Create);

        /// <summary>
        /// Represents a 'milestoned' event on a given issue or pull request.
        /// </summary>
        public MilestonedEvent MilestonedEvent => this.CreateProperty(x => x.MilestonedEvent, Octoqit.MilestonedEvent.Create);

        /// <summary>
        /// Represents a 'demilestoned' event on a given issue or pull request.
        /// </summary>
        public DemilestonedEvent DemilestonedEvent => this.CreateProperty(x => x.DemilestonedEvent, Octoqit.DemilestonedEvent.Create);

        /// <summary>
        /// Represents a 'renamed' event on a given issue or pull request or pull request.
        /// </summary>
        public RenamedEvent RenamedEvent => this.CreateProperty(x => x.RenamedEvent, Octoqit.RenamedEvent.Create);

        /// <summary>
        /// Represents a 'locked' event on a given issue or pull request.
        /// </summary>
        public LockedEvent LockedEvent => this.CreateProperty(x => x.LockedEvent, Octoqit.LockedEvent.Create);

        /// <summary>
        /// Represents a 'unlocked' event on a given issue or pull request.
        /// </summary>
        public UnlockedEvent UnlockedEvent => this.CreateProperty(x => x.UnlockedEvent, Octoqit.UnlockedEvent.Create);

        /// <summary>
        /// Represents a 'deployed' event on a given issue or pull request.
        /// </summary>
        public DeployedEvent DeployedEvent => this.CreateProperty(x => x.DeployedEvent, Octoqit.DeployedEvent.Create);

        /// <summary>
        /// Represents a 'head_ref_deleted' event on a given pull request.
        /// </summary>
        public HeadRefDeletedEvent HeadRefDeletedEvent => this.CreateProperty(x => x.HeadRefDeletedEvent, Octoqit.HeadRefDeletedEvent.Create);

        /// <summary>
        /// Represents a 'head_ref_restored' event on a given pull request.
        /// </summary>
        public HeadRefRestoredEvent HeadRefRestoredEvent => this.CreateProperty(x => x.HeadRefRestoredEvent, Octoqit.HeadRefRestoredEvent.Create);

        /// <summary>
        /// Represents a 'head_ref_force_pushed' event on a given pull request.
        /// </summary>
        public HeadRefForcePushedEvent HeadRefForcePushedEvent => this.CreateProperty(x => x.HeadRefForcePushedEvent, Octoqit.HeadRefForcePushedEvent.Create);

        /// <summary>
        /// Represents a 'base_ref_force_pushed' event on a given pull request.
        /// </summary>
        public BaseRefForcePushedEvent BaseRefForcePushedEvent => this.CreateProperty(x => x.BaseRefForcePushedEvent, Octoqit.BaseRefForcePushedEvent.Create);

        /// <summary>
        /// Represents an 'review_requested' event on a given pull request.
        /// </summary>
        public ReviewRequestedEvent ReviewRequestedEvent => this.CreateProperty(x => x.ReviewRequestedEvent, Octoqit.ReviewRequestedEvent.Create);

        /// <summary>
        /// Represents an 'review_request_removed' event on a given pull request.
        /// </summary>
        public ReviewRequestRemovedEvent ReviewRequestRemovedEvent => this.CreateProperty(x => x.ReviewRequestRemovedEvent, Octoqit.ReviewRequestRemovedEvent.Create);

        /// <summary>
        /// Represents a 'review_dismissed' event on a given issue or pull request.
        /// </summary>
        public ReviewDismissedEvent ReviewDismissedEvent => this.CreateProperty(x => x.ReviewDismissedEvent, Octoqit.ReviewDismissedEvent.Create);

        internal static IssueTimelineItem Create(IQueryProvider provider, Expression expression)
        {
            return new IssueTimelineItem(provider, expression);
        }
    }
}