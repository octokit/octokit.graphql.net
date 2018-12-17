namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A contributions collection aggregates contributions such as opened issues and commits created by a user.
    /// </summary>
    public class ContributionsCollection : QueryableValue<ContributionsCollection>
    {
        internal ContributionsCollection(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A calendar of this user's contributions on GitHub.
        /// </summary>
        public ContributionCalendar ContributionCalendar => this.CreateProperty(x => x.ContributionCalendar, Octokit.GraphQL.Model.ContributionCalendar.Create);

        /// <summary>
        /// Determine if this collection's time span ends in the current month.
        /// </summary>
        public bool DoesEndInCurrentMonth { get; }

        /// <summary>
        /// The date of the first restricted contribution the user made in this time period. Can only be non-null when the user has enabled private contribution counts.
        /// </summary>
        public string EarliestRestrictedContributionDate { get; }

        /// <summary>
        /// The ending date and time of this collection.
        /// </summary>
        public DateTimeOffset EndedAt { get; }

        /// <summary>
        /// Does the user have any more activity in the timeline that occurred prior to the collection's time range?
        /// </summary>
        public bool HasActivityInThePast { get; }

        /// <summary>
        /// Determine if there are any contributions in this collection.
        /// </summary>
        public bool HasAnyContributions { get; }

        /// <summary>
        /// Determine if the user made any contributions in this time frame whose details are not visible because they were made in a private repository. Can only be true if the user enabled private contribution counts.
        /// </summary>
        public bool HasAnyRestrictedContributions { get; }

        /// <summary>
        /// Whether or not the collector's time span is all within the same day.
        /// </summary>
        public bool IsSingleDay { get; }

        /// <summary>
        /// When the user signed up for GitHub. This will be null if that sign up date falls outside the collection's time range and ignoreTimeRange is false.
        /// </summary>
        /// <param name="ignoreTimeRange">If true, the contribution will be returned even if the user signed up outside of the collection's time range.</param>
        public JoinedGitHubContribution JoinedGitHubContribution(Arg<bool>? ignoreTimeRange = null) => this.CreateMethodCall(x => x.JoinedGitHubContribution(ignoreTimeRange), Octokit.GraphQL.Model.JoinedGitHubContribution.Create);

        /// <summary>
        /// The date of the most recent restricted contribution the user made in this time period. Can only be non-null when the user has enabled private contribution counts.
        /// </summary>
        public string LatestRestrictedContributionDate { get; }

        /// <summary>
        /// When this collection's time range does not include any activity from the user, use this
        /// to get a different collection from an earlier time range that does have activity.
        /// </summary>
        public ContributionsCollection MostRecentCollectionWithActivity => this.CreateProperty(x => x.MostRecentCollectionWithActivity, Octokit.GraphQL.Model.ContributionsCollection.Create);

        /// <summary>
        /// Returns a different contributions collection from an earlier time range than this one
        /// that does not have any contributions.
        /// </summary>
        public ContributionsCollection MostRecentCollectionWithoutActivity => this.CreateProperty(x => x.MostRecentCollectionWithoutActivity, Octokit.GraphQL.Model.ContributionsCollection.Create);

        /// <summary>
        /// The issue the user opened on GitHub that received the most comments in the specified
        /// time frame.
        /// </summary>
        public CreatedIssueContribution PopularIssueContribution => this.CreateProperty(x => x.PopularIssueContribution, Octokit.GraphQL.Model.CreatedIssueContribution.Create);

        /// <summary>
        /// The pull request the user opened on GitHub that received the most comments in the
        /// specified time frame.
        /// </summary>
        public CreatedPullRequestContribution PopularPullRequestContribution => this.CreateProperty(x => x.PopularPullRequestContribution, Octokit.GraphQL.Model.CreatedPullRequestContribution.Create);

        /// <summary>
        /// A count of contributions made by the user that the viewer cannot access. Only non-zero when the user has chosen to share their private contribution counts.
        /// </summary>
        public int RestrictedContributionsCount { get; }

        /// <summary>
        /// The beginning date and time of this collection.
        /// </summary>
        public DateTimeOffset StartedAt { get; }

        /// <summary>
        /// How many commits were made by the user in this time span.
        /// </summary>
        public int TotalCommitContributions { get; }

        /// <summary>
        /// How many issues the user opened.
        /// </summary>
        /// <param name="excludeFirst">Should the user's first issue ever be excluded from this count.</param>
        /// <param name="excludePopular">Should the user's most commented issue be excluded from this count.</param>
        public int TotalIssueContributions(Arg<bool>? excludeFirst = null, Arg<bool>? excludePopular = null) => default;

        /// <summary>
        /// How many pull requests the user opened.
        /// </summary>
        /// <param name="excludeFirst">Should the user's first pull request ever be excluded from this count.</param>
        /// <param name="excludePopular">Should the user's most commented pull request be excluded from this count.</param>
        public int TotalPullRequestContributions(Arg<bool>? excludeFirst = null, Arg<bool>? excludePopular = null) => default;

        /// <summary>
        /// How many pull request reviews the user left.
        /// </summary>
        public int TotalPullRequestReviewContributions { get; }

        /// <summary>
        /// How many different repositories the user committed to.
        /// </summary>
        public int TotalRepositoriesWithContributedCommits { get; }

        /// <summary>
        /// How many different repositories the user opened issues in.
        /// </summary>
        /// <param name="excludeFirst">Should the user's first issue ever be excluded from this count.</param>
        /// <param name="excludePopular">Should the user's most commented issue be excluded from this count.</param>
        public int TotalRepositoriesWithContributedIssues(Arg<bool>? excludeFirst = null, Arg<bool>? excludePopular = null) => default;

        /// <summary>
        /// How many different repositories the user left pull request reviews in.
        /// </summary>
        public int TotalRepositoriesWithContributedPullRequestReviews { get; }

        /// <summary>
        /// How many different repositories the user opened pull requests in.
        /// </summary>
        /// <param name="excludeFirst">Should the user's first pull request ever be excluded from this count.</param>
        /// <param name="excludePopular">Should the user's most commented pull request be excluded from this count.</param>
        public int TotalRepositoriesWithContributedPullRequests(Arg<bool>? excludeFirst = null, Arg<bool>? excludePopular = null) => default;

        /// <summary>
        /// How many repositories the user created.
        /// </summary>
        /// <param name="excludeFirst">Should the user's first repository ever be excluded from this count.</param>
        public int TotalRepositoryContributions(Arg<bool>? excludeFirst = null) => default;

        /// <summary>
        /// The user who made the contributions in this collection.
        /// </summary>
        public User User => this.CreateProperty(x => x.User, Octokit.GraphQL.Model.User.Create);

        internal static ContributionsCollection Create(Expression expression)
        {
            return new ContributionsCollection(expression);
        }
    }
}