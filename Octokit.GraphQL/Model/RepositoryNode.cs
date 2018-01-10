namespace Octokit.GraphQL.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

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

namespace Octokit.GraphQL.Model.Internal
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIRepositoryNode : QueryEntity, IRepositoryNode
    {
        public StubIRepositoryNode(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        internal static StubIRepositoryNode Create(IQueryProvider provider, Expression expression)
        {
            return new StubIRepositoryNode(provider, expression);
        }
    }
}