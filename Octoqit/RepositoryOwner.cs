namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Represents an owner of a Repository.
    /// </summary>
    public interface IRepositoryOwner : IQueryEntity
    {
        /// <summary>
        /// A URL pointing to the owner's public avatar.
        /// </summary>
        /// <param name="size">The size of the resulting square image.</param>
        IQueryable<string> AvatarURL(int? size);

        /// <summary>
        /// Find gist by repo name.
        /// </summary>
        /// <param name="name">The gist name to find.</param>
        Gist Gist(string name);

        /// <summary>
        /// A list of the Gists the user has created.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="visibility">Allows filtering by gist visibility.</param>
        GistConnection Gists(int? first, string after, int? last, string before, GistVisibility? visibility);

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
        /// <param name="isFork">If non-null, filters repositories according to whether they are forks of another repository</param>
        /// <param name="orderBy">Ordering options for repositories returned from the connection</param>
        /// <param name="affiliation">Affiliation options for repositories returned from the connection</param>
        RepositoryConnection Repositories(int? first, string after, int? last, string before, RepositoryPrivacy? privacy, bool? isFork, RepositoryOrder orderBy, IQueryable<RepositoryAffiliation?> affiliation);

        /// <summary>
        /// Find Repository.
        /// </summary>
        /// <param name="name">Name of Repository to find.</param>
        Repository Repository(string name);

        /// <summary>
        /// The HTTP url for the owner.
        /// </summary>
        string Url { get; }
    }
}

namespace Octoqit.Internal
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    internal class StubIRepositoryOwner : QueryEntity, IRepositoryOwner
    {
        public StubIRepositoryOwner(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public IQueryable<string> AvatarURL(int? size) => this.CreateMethodCall(x => x.AvatarURL(size));

        public Gist Gist(string name) => this.CreateMethodCall(x => x.Gist(name), Octoqit.Gist.Create);

        public GistConnection Gists(int? first, string after, int? last, string before, GistVisibility? visibility) => this.CreateMethodCall(x => x.Gists(first, after, last, before, visibility), Octoqit.GistConnection.Create);

        public string Id { get; }

        public string Login { get; }

        public string Path { get; }

        public RepositoryConnection Repositories(int? first, string after, int? last, string before, RepositoryPrivacy? privacy, bool? isFork, RepositoryOrder orderBy, IQueryable<RepositoryAffiliation?> affiliation) => this.CreateMethodCall(x => x.Repositories(first, after, last, before, privacy, isFork, orderBy, affiliation), Octoqit.RepositoryConnection.Create);

        public Repository Repository(string name) => this.CreateMethodCall(x => x.Repository(name), Octoqit.Repository.Create);

        public string Url { get; }

        internal static StubIRepositoryOwner Create(IQueryProvider provider, Expression expression)
        {
            return new StubIRepositoryOwner(provider, expression);
        }
    }
}