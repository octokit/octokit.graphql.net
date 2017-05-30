namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The query root of GitHub's GraphQL interface.
    /// </summary>
    public class Query : QueryEntity, IRootQuery
    {
        public Query() : base(new QueryProvider())
        {
        }

        internal Query(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Look up a code of conduct by its key
        /// </summary>
        /// <param name="key">The code of conduct's key</param>
        public CodeOfConduct CodeOfConduct(string key) => this.CreateMethodCall(x => x.CodeOfConduct(key), Octokit.GraphQL.CodeOfConduct.Create);

        /// <summary>
        /// Look up a code of conduct by its key
        /// </summary>
        public IQueryable<CodeOfConduct> CodesOfConduct => this.CreateProperty(x => x.CodesOfConduct);

        /// <summary>
        /// Fetches an object given its ID.
        /// </summary>
        /// <param name="id">ID of the object.</param>
        public INode Node(string id) => this.CreateMethodCall(x => x.Node(id), Octokit.GraphQL.Internal.StubINode.Create);

        /// <summary>
        /// Lookup nodes by a list of IDs.
        /// </summary>
        /// <param name="ids">The list of node IDs.</param>
        public IQueryable<INode> Nodes(IQueryable<string> ids) => this.CreateMethodCall(x => x.Nodes(ids));

        /// <summary>
        /// Lookup a organization by login.
        /// </summary>
        /// <param name="login">The organization's login.</param>
        public Organization Organization(string login) => this.CreateMethodCall(x => x.Organization(login), Octokit.GraphQL.Organization.Create);

        /// <summary>
        /// The client's rate limit information.
        /// </summary>
        public RateLimit RateLimit => this.CreateProperty(x => x.RateLimit, Octokit.GraphQL.RateLimit.Create);

        /// <summary>
        /// Hack to workaround https://github.com/facebook/relay/issues/112 re-exposing the root query object
        /// </summary>
        public Query Relay => this.CreateProperty(x => x.Relay, Octokit.GraphQL.Query.Create);

        /// <summary>
        /// Lookup a given repository by the owner and repository name.
        /// </summary>
        /// <param name="owner">The login field of a user or organizationn</param>
        /// <param name="name">The name of the repository</param>
        public Repository Repository(string owner, string name) => this.CreateMethodCall(x => x.Repository(owner, name), Octokit.GraphQL.Repository.Create);

        /// <summary>
        /// Lookup a repository owner (ie. either a User or an Organization) by login.
        /// </summary>
        /// <param name="login">The username to lookup the owner by.</param>
        public IRepositoryOwner RepositoryOwner(string login) => this.CreateMethodCall(x => x.RepositoryOwner(login), Octokit.GraphQL.Internal.StubIRepositoryOwner.Create);

        /// <summary>
        /// Lookup resource by a URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        public IUniformResourceLocatable Resource(string url) => this.CreateMethodCall(x => x.Resource(url), Octokit.GraphQL.Internal.StubIUniformResourceLocatable.Create);

        /// <summary>
        /// Perform a search across resources.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="query">The search string to look for.</param>
        /// <param name="type">The types of search items to search within.</param>
        public SearchResultItemConnection Search(string query, SearchType type, int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Search(query, type, first, after, last, before), Octokit.GraphQL.SearchResultItemConnection.Create);

        /// <summary>
        /// Look up a topic by name.
        /// </summary>
        /// <param name="name">The topic's name.</param>
        public Topic Topic(string name) => this.CreateMethodCall(x => x.Topic(name), Octokit.GraphQL.Topic.Create);

        /// <summary>
        /// Lookup a user by login.
        /// </summary>
        /// <param name="login">The user's login.</param>
        public User User(string login) => this.CreateMethodCall(x => x.User(login), Octokit.GraphQL.User.Create);

        /// <summary>
        /// The currently authenticated user.
        /// </summary>
        public User Viewer => this.CreateProperty(x => x.Viewer, Octokit.GraphQL.User.Create);

        internal static Query Create(IQueryProvider provider, Expression expression)
        {
            return new Query(provider, expression);
        }
    }
}