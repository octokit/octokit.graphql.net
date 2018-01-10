namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A release asset contains the content for a release asset.
    /// </summary>
    public class ReleaseAsset : QueryEntity
    {
        public ReleaseAsset(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// The asset's content-type
        /// </summary>
        public string ContentType { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        /// <summary>
        /// The number of times this asset was downloaded
        /// </summary>
        public int DownloadCount { get; }

        /// <summary>
        /// Identifies the URL where you can download the release asset via the browser.
        /// </summary>
        public string DownloadUrl { get; }

        public string Id { get; }

        /// <summary>
        /// Identifies the title of the release asset.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Release that the asset is associated with
        /// </summary>
        public Release Release => this.CreateProperty(x => x.Release, Octokit.GraphQL.Release.Create);

        /// <summary>
        /// The size (in bytes) of the asset
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public string UpdatedAt { get; }

        /// <summary>
        /// The user that performed the upload
        /// </summary>
        public User UploadedBy => this.CreateProperty(x => x.UploadedBy, Octokit.GraphQL.User.Create);

        /// <summary>
        /// Identifies the URL of the release asset.
        /// </summary>
        public string Url { get; }

        internal static ReleaseAsset Create(IQueryProvider provider, Expression expression)
        {
            return new ReleaseAsset(provider, expression);
        }
    }
}