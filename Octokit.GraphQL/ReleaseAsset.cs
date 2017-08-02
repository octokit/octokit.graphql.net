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

        public string Id { get; }

        /// <summary>
        /// Identifies the title of the release asset.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// release that the asset is associated with
        /// </summary>
        public Release Release => this.CreateProperty(x => x.Release, Octokit.GraphQL.Release.Create);

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