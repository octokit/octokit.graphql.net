namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A release contains the content for a release.
    /// </summary>
    public class Release : QueryEntity
    {
        public Release(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the description of the release.
        /// </summary>
        public string Description { get; }

        public string Id { get; }

        /// <summary>
        /// Identifies the title of the release.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The HTTP path for this issue
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Identifies the date and time when the release was created.
        /// </summary>
        public string PublishedAt { get; }

        /// <summary>
        /// List of releases assets which are dependent on this release.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="name">A list of names to filter the assets by.</param>
        public ReleaseAssetConnection ReleaseAsset(int? first = null, string after = null, int? last = null, string before = null, string name = null) => this.CreateMethodCall(x => x.ReleaseAsset(first, after, last, before, name), Octokit.GraphQL.ReleaseAssetConnection.Create);

        /// <summary>
        /// List of releases assets which are dependent on this release.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public ReleaseAssetConnection ReleaseAssets(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.ReleaseAssets(first, after, last, before), Octokit.GraphQL.ReleaseAssetConnection.Create);

        /// <summary>
        /// The HTTP path for this issue
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The Git tag the release points to
        /// </summary>
        public Ref Tag => this.CreateProperty(x => x.Tag, Octokit.GraphQL.Ref.Create);

        /// <summary>
        /// The HTTP url for this issue
        /// </summary>
        public string Url { get; }

        internal static Release Create(IQueryProvider provider, Expression expression)
        {
            return new Release(provider, expression);
        }
    }
}