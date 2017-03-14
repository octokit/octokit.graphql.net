namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class ReleaseAssetEdge : QueryEntity
    {
        public ReleaseAssetEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public ReleaseAsset Node => this.CreateProperty(x => x.Node, Octoqit.ReleaseAsset.Create);

        internal static ReleaseAssetEdge Create(IQueryProvider provider, Expression expression)
        {
            return new ReleaseAssetEdge(provider, expression);
        }
    }
}