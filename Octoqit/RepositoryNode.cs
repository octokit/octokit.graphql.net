namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Represents a object that belongs to a repository.
    /// </summary>
    public interface IRepositoryNode : IQueryEntity
    {
        /// <summary>
        /// The repository associated with this node.
        /// </summary>
        Repository Repository { get; }
    }
}

namespace Octoqit.Internal
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    internal class StubIRepositoryNode : QueryEntity, IRepositoryNode
    {
        public StubIRepositoryNode(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public Repository Repository => this.CreateProperty(x => x.Repository, Octoqit.Repository.Create);

        internal static StubIRepositoryNode Create(IQueryProvider provider, Expression expression)
        {
            return new StubIRepositoryNode(provider, expression);
        }
    }
}