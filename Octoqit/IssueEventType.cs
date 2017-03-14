using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octoqit
{
    /// <summary>
    /// The possible issue event types.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum IssueEventType
    {
        /// <summary>
        /// The issue was assigned to the actor.
        /// </summary>
        [EnumMember(Value = "ASSIGNED")]
        Assigned,

        /// <summary>
        /// The base branch was force pushed by the actor.
        /// </summary>
        [EnumMember(Value = "BASE_REF_FORCE_PUSHED")]
        BaseRefForcePushed,

        /// <summary>
        /// The issue was closed by the actor.
        /// </summary>
        [EnumMember(Value = "CLOSED")]
        Closed,

        /// <summary>
        /// The issue had a milestone removed from it.
        /// </summary>
        [EnumMember(Value = "DEMILESTONED")]
        Demilestoned,

        /// <summary>
        /// The branch was deployed by the actor.
        /// </summary>
        [EnumMember(Value = "DEPLOYED")]
        Deployed,

        /// <summary>
        /// The head branch was deleted by the actor.
        /// </summary>
        [EnumMember(Value = "HEAD_REF_DELETED")]
        HeadRefDeleted,

        /// <summary>
        /// The head branch was force pushed by the actor.
        /// </summary>
        [EnumMember(Value = "HEAD_REF_FORCE_PUSHED")]
        HeadRefForcePushed,

        /// <summary>
        /// The head branch was restored by the actor.
        /// </summary>
        [EnumMember(Value = "HEAD_REF_RESTORED")]
        HeadRefRestored,

        /// <summary>
        /// A label was added to the issue.
        /// </summary>
        [EnumMember(Value = "LABELED")]
        Labeled,

        /// <summary>
        /// The issue was locked by the actor.
        /// </summary>
        [EnumMember(Value = "LOCKED")]
        Locked,

        /// <summary>
        /// The pull request or issue was mentioned by the actor.
        /// </summary>
        [EnumMember(Value = "MENTIONED")]
        Mentioned,

        /// <summary>
        /// The issue was merged by the actor.
        /// </summary>
        [EnumMember(Value = "MERGED")]
        Merged,

        /// <summary>
        /// The issue had a milestone added to it.
        /// </summary>
        [EnumMember(Value = "MILESTONED")]
        Milestoned,

        /// <summary>
        /// The issue was referenced from a commit message.
        /// </summary>
        [EnumMember(Value = "REFERENCED")]
        Referenced,

        /// <summary>
        /// The issue's title was changed.
        /// </summary>
        [EnumMember(Value = "RENAMED")]
        Renamed,

        /// <summary>
        /// The issue was reopened by the actor.
        /// </summary>
        [EnumMember(Value = "REOPENED")]
        Reopened,

        /// <summary>
        /// The actor requested review from the subject.
        /// </summary>
        [EnumMember(Value = "REVIEW_REQUESTED")]
        ReviewRequested,

        /// <summary>
        /// The actor removed the review request for the subject.
        /// </summary>
        [EnumMember(Value = "REVIEW_REQUEST_REMOVED")]
        ReviewRequestRemoved,

        /// <summary>
        /// The review was dismissed by the actor.
        /// </summary>
        [EnumMember(Value = "REVIEW_DISMISSED")]
        ReviewDismissed,

        /// <summary>
        /// The pull request or issue was subscribed to by the actor.
        /// </summary>
        [EnumMember(Value = "SUBSCRIBED")]
        Subscribed,

        /// <summary>
        /// The issue was unassigned to the actor.
        /// </summary>
        [EnumMember(Value = "UNASSIGNED")]
        Unassigned,

        /// <summary>
        /// A label was removed from the issue.
        /// </summary>
        [EnumMember(Value = "UNLABELED")]
        Unlabeled,

        /// <summary>
        /// The issue was unlocked by the actor.
        /// </summary>
        [EnumMember(Value = "UNLOCKED")]
        Unlocked,

        /// <summary>
        /// The pull request or issue was unsubscribed from by the actor.
        /// </summary>
        [EnumMember(Value = "UNSUBSCRIBED")]
        Unsubscribed,
    }
}