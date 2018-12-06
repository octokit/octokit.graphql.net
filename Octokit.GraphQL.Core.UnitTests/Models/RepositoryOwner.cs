using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public interface IRepositoryOwner : IQueryableValue<IRepositoryOwner>, IQueryableInterface
    {
        ID Id { get; }
        string Login { get; }

        RepositoryConnection Repositories(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null);

        /// <summary>
        /// Find Repository.
        /// </summary>
        /// <param name="name">Name of Repository to find.</param>
        Repository Repository(Arg<string> name);

        /// <summary>
        /// The HTTP URL for the owner.
        /// </summary>
        string ResourcePath { get; }

        /// <summary>
        /// The HTTP URL for the owner.
        /// </summary>
        string Url { get; }
    }
}

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    internal class StubIRepositoryOwner : QueryableValue<StubIRepositoryOwner>, IRepositoryOwner
    {
        public StubIRepositoryOwner(Expression expression) : base(expression)
        {
        }

        public ID Id { get; }

        public string Login { get; }

        public RepositoryConnection Repositories(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Repositories(first, after, last, before), Models.RepositoryConnection.Create);

        public Repository Repository(Arg<string> name) => this.CreateMethodCall(x => x.Repository(name), Models.Repository.Create);

        public string ResourcePath { get; }

        public string Url { get; }

        internal static StubIRepositoryOwner Create(Expression expression)
        {
            return new StubIRepositoryOwner(expression);
        }
    }
}