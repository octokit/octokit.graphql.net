namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents an owner of a Repository.
    /// </summary>
    public interface IRepositoryOwner : IQueryEntity
    {
        /// <summary>
        /// A URL pointing to the owner's public avatar.
        /// </summary>
        /// <param name="size">The size of the resulting square image.</param>
        string AvatarUrl(int? size = null);

        /// <summary>
        /// Find gist by repo name.
        /// </summary>
        /// <param name="name">The gist name to find.</param>
        Gist Gist(string name);

        string Id { get; }

        /// <summary>
        /// The username used to login.
        /// </summary>
        string Login { get; }

        /// <summary>
        /// The HTTP url for the owner.
        /// </summary>
        string Path { get; }

        /// <summary>
        /// A list of repositories that the user owns.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="privacy">If non-null, filters repositories according to privacy</param>
        /// <param name="orderBy">Ordering options for repositories returned from the connection</param>
        /// <param name="affiliations">Affiliation options for repositories returned from the connection</param>
        /// <param name="isLocked">If non-null, filters repositories according to whether they have been locked</param>
        /// <param name="isFork">If non-null, filters repositories according to whether they are forks of another repository</param>
        RepositoryConnection Repositories(int? first = null, string after = null, int? last = null, string before = null, RepositoryPrivacy? privacy = null, RepositoryOrder orderBy = null, IQueryable<RepositoryAffiliation?> affiliations = null, bool? isLocked = null, bool? isFork = null);

        /// <summary>
        /// Find Repository.
        /// </summary>
        /// <param name="name">Name of Repository to find.</param>
        Repository Repository(string name);

        /// <summary>
        /// The HTTP url for the owner.
        /// </summary>
        string ResourcePath { get; }

        /// <summary>
        /// The HTTP url for the owner.
        /// </summary>
        string Url { get; }
    }
}

namespace Octokit.GraphQL.Internal
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIRepositoryOwner : QueryEntity, IRepositoryOwner
    {
        public StubIRepositoryOwner(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public string AvatarUrl(int? size = null) => null;

        public Gist Gist(string name) => this.CreateMethodCall(x => x.Gist(name), Octokit.GraphQL.Gist.Create);

        public string Id { get; }

        public string Login { get; }

        public string Path { get; }

        public RepositoryConnection Repositories(int? first = null, string after = null, int? last = null, string before = null, RepositoryPrivacy? privacy = null, RepositoryOrder orderBy = null, IQueryable<RepositoryAffiliation?> affiliations = null, bool? isLocked = null, bool? isFork = null) => this.CreateMethodCall(x => x.Repositories(first, after, last, before, privacy, orderBy, affiliations, isLocked, isFork), Octokit.GraphQL.RepositoryConnection.Create);

        public Repository Repository(string name) => this.CreateMethodCall(x => x.Repository(name), Octokit.GraphQL.Repository.Create);

        public string ResourcePath { get; }

        public string Url { get; }

        internal static StubIRepositoryOwner Create(IQueryProvider provider, Expression expression)
        {
            return new StubIRepositoryOwner(provider, expression);
        }
    }
}