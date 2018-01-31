namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A release contains the content for a release.
    /// </summary>
    public class Release : QueryableValue<Release>
    {
        public Release(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The author of the release
        /// </summary>
        public User Author => this.CreateProperty(x => x.Author, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset? CreatedAt { get; }

        /// <summary>
        /// Identifies the description of the release.
        /// </summary>
        public string Description { get; }

        public string Id { get; }

        /// <summary>
        /// Whether or not the release is a draft
        /// </summary>
        public bool IsDraft { get; }

        /// <summary>
        /// Whether or not the release is a prerelease
        /// </summary>
        public bool IsPrerelease { get; }

        /// <summary>
        /// Identifies the title of the release.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Identifies the date and time when the release was created.
        /// </summary>
        public DateTimeOffset? PublishedAt { get; }

        /// <summary>
        /// List of releases assets which are dependent on this release.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="name">A list of names to filter the assets by.</param>
        public ReleaseAssetConnection ReleaseAssets(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<string>? name = null) => this.CreateMethodCall(x => x.ReleaseAssets(first, after, last, before, name), Octokit.GraphQL.Model.ReleaseAssetConnection.Create);

        /// <summary>
        /// The HTTP path for this issue
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The Git tag the release points to
        /// </summary>
        public Ref Tag => this.CreateProperty(x => x.Tag, Octokit.GraphQL.Model.Ref.Create);

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        [Obsolete(@"General type updated timestamps will eventually be replaced by other field specific timestamps.")]
        public DateTimeOffset? UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this issue
        /// </summary>
        public string Url { get; }

        internal static Release Create(Expression expression)
        {
            return new Release(expression);
        }
    }
}