namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Merges must be performed via a merge queue.
    /// </summary>
    public class MergeQueueParametersInput
    {
        /// <summary>
        /// Maximum time for a required status check to report a conclusion. After this much time has elapsed, checks that have not reported a conclusion will be assumed to have failed
        /// </summary>
        public int CheckResponseTimeoutMinutes { get; set; }

        /// <summary>
        /// When set to ALLGREEN, the merge commit created by merge queue for each PR in the group must pass all required checks to merge. When set to HEADGREEN, only the commit at the head of the merge group, i.e. the commit containing changes from all of the PRs in the group, must pass its required checks to merge.
        /// </summary>
        public MergeQueueGroupingStrategy GroupingStrategy { get; set; }

        /// <summary>
        /// Limit the number of queued pull requests requesting checks and workflow runs at the same time.
        /// </summary>
        public int MaxEntriesToBuild { get; set; }

        /// <summary>
        /// The maximum number of PRs that will be merged together in a group.
        /// </summary>
        public int MaxEntriesToMerge { get; set; }

        /// <summary>
        /// Method to use when merging changes from queued pull requests.
        /// </summary>
        public MergeQueueMergeMethod MergeMethod { get; set; }

        /// <summary>
        /// The minimum number of PRs that will be merged together in a group.
        /// </summary>
        public int MinEntriesToMerge { get; set; }

        /// <summary>
        /// The time merge queue should wait after the first PR is added to the queue for the minimum group size to be met. After this time has elapsed, the minimum group size will be ignored and a smaller group will be merged.
        /// </summary>
        public int MinEntriesToMergeWaitMinutes { get; set; }
    }
}