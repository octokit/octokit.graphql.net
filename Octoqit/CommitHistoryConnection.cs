namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// The connection type for Commit.
    /// </summary>
    public class CommitHistoryConnection : QueryEntity
    {
        public CommitHistoryConnection(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public IQueryable<CommitEdge> Edges => this.CreateProperty(x => x.Edges);

        /// <summary>
        /// A list of nodes.
        /// </summary>
        public IQueryable<Commit> Nodes => this.CreateProperty(x => x.Nodes);

        /// <summary>
        /// Information to aid in pagination.
        /// </summary>
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Octoqit.PageInfo.Create);

        internal static CommitHistoryConnection Create(IQueryProvider provider, Expression expression)
        {
            return new CommitHistoryConnection(provider, expression);
        }
    }
}