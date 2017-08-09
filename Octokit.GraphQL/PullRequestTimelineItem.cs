namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An item in an pull request timeline
    /// </summary>
    public class PullRequestTimelineItem : QueryEntity, IUnion
    {
        public PullRequestTimelineItem(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Represents a Git commit.
        /// </summary>
        public Commit Commit => this.CreateProperty(x => x.Commit, Octokit.GraphQL.Commit.Create);

        /// <summary>
        /// A thread of comments on a commit.
        /// </summary>
        public CommitCommentThread CommitCommentThread => this.CreateProperty(x => x.CommitCommentThread, Octokit.GraphQL.CommitCommentThread.Create);

        /// <summary>
        /// A review object for a given pull request.
        /// </summary>
        public PullRequestReview PullRequestReview => this.CreateProperty(x => x.PullRequestReview, Octokit.GraphQL.PullRequestReview.Create);

        /// <summary>
        /// A threaded list of comments for a given pull request.
        /// </summary>
        public PullRequestReviewThread PullRequestReviewThread => this.CreateProperty(x => x.PullRequestReviewThread, Octokit.GraphQL.PullRequestReviewThread.Create);

        /// <summary>
        /// A review comment associated with a given repository pull request.
        /// </summary>
        public PullRequestReviewComment PullRequestReviewComment => this.CreateProperty(x => x.PullRequestReviewComment, Octokit.GraphQL.PullRequestReviewComment.Create);

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
        /// Represents a 'merged' event on a given pull request.
        /// </summary>
        public MergedEvent MergedEvent => this.CreateProperty(x => x.MergedEvent, Octokit.GraphQL.MergedEvent.Create);

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

        /// <summary>
        /// Represents a 'deployed' event on a given pull request.
        /// </summary>
        public DeployedEvent DeployedEvent => this.CreateProperty(x => x.DeployedEvent, Octokit.GraphQL.DeployedEvent.Create);

        /// <summary>
        /// Represents a 'head_ref_deleted' event on a given pull request.
        /// </summary>
        public HeadRefDeletedEvent HeadRefDeletedEvent => this.CreateProperty(x => x.HeadRefDeletedEvent, Octokit.GraphQL.HeadRefDeletedEvent.Create);

        /// <summary>
        /// Represents a 'head_ref_restored' event on a given pull request.
        /// </summary>
        public HeadRefRestoredEvent HeadRefRestoredEvent => this.CreateProperty(x => x.HeadRefRestoredEvent, Octokit.GraphQL.HeadRefRestoredEvent.Create);

        /// <summary>
        /// Represents a 'head_ref_force_pushed' event on a given pull request.
        /// </summary>
        public HeadRefForcePushedEvent HeadRefForcePushedEvent => this.CreateProperty(x => x.HeadRefForcePushedEvent, Octokit.GraphQL.HeadRefForcePushedEvent.Create);

        /// <summary>
        /// Represents a 'base_ref_force_pushed' event on a given pull request.
        /// </summary>
        public BaseRefForcePushedEvent BaseRefForcePushedEvent => this.CreateProperty(x => x.BaseRefForcePushedEvent, Octokit.GraphQL.BaseRefForcePushedEvent.Create);

        /// <summary>
        /// Represents an 'review_requested' event on a given pull request.
        /// </summary>
        public ReviewRequestedEvent ReviewRequestedEvent => this.CreateProperty(x => x.ReviewRequestedEvent, Octokit.GraphQL.ReviewRequestedEvent.Create);

        /// <summary>
        /// Represents an 'review_request_removed' event on a given pull request.
        /// </summary>
        public ReviewRequestRemovedEvent ReviewRequestRemovedEvent => this.CreateProperty(x => x.ReviewRequestRemovedEvent, Octokit.GraphQL.ReviewRequestRemovedEvent.Create);

        /// <summary>
        /// Represents a 'review_dismissed' event on a given issue or pull request.
        /// </summary>
        public ReviewDismissedEvent ReviewDismissedEvent => this.CreateProperty(x => x.ReviewDismissedEvent, Octokit.GraphQL.ReviewDismissedEvent.Create);

        internal static PullRequestTimelineItem Create(IQueryProvider provider, Expression expression)
        {
            return new PullRequestTimelineItem(provider, expression);
        }
    }
}