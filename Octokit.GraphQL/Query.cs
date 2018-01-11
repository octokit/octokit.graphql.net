namespace Octokit.GraphQL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The query root of GitHub's GraphQL interface.
    /// </summary>
    public class Query : QueryEntity, IQuery
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
        public CodeOfConduct CodeOfConduct(string key) => this.CreateMethodCall(x => x.CodeOfConduct(key), Octokit.GraphQL.Model.CodeOfConduct.Create);

        /// <summary>
        /// Look up a code of conduct by its key
        /// </summary>
        public IQueryable<CodeOfConduct> CodesOfConduct => this.CreateProperty(x => x.CodesOfConduct);

        /// <summary>
        /// Look up an open source license by its key
        /// </summary>
        /// <param name="key">The license's downcased SPDX ID</param>
        public License License(string key) => this.CreateMethodCall(x => x.License(key), Octokit.GraphQL.Model.License.Create);

        /// <summary>
        /// Return a list of known open source licenses
        /// </summary>
        public IQueryable<License> Licenses => this.CreateProperty(x => x.Licenses);

        /// <summary>
        /// Get alphabetically sorted list of Marketplace categories
        /// </summary>
        /// <param name="excludeEmpty">Exclude categories with no listings.</param>
        public IQueryable<MarketplaceCategory> MarketplaceCategories(bool? excludeEmpty = null) => this.CreateMethodCall(x => x.MarketplaceCategories(excludeEmpty));

        /// <summary>
        /// Look up a Marketplace category by its slug.
        /// </summary>
        /// <param name="slug">The URL slug of the category.</param>
        public MarketplaceCategory MarketplaceCategory(string slug) => this.CreateMethodCall(x => x.MarketplaceCategory(slug), Octokit.GraphQL.Model.MarketplaceCategory.Create);

        /// <summary>
        /// Look up a single Marketplace listing
        /// </summary>
        /// <param name="slug">Select the listing that matches this slug. It's the short name of the listing used in its URL.</param>
        public MarketplaceListing MarketplaceListing(string slug) => this.CreateMethodCall(x => x.MarketplaceListing(slug), Octokit.GraphQL.Model.MarketplaceListing.Create);

        /// <summary>
        /// Look up Marketplace listings
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="categorySlug">Select only listings with the given category.</param>
        /// <param name="viewerCanAdmin">Select listings to which user has admin access. If omitted, listings visible to the viewer are returned.</param>
        /// <param name="adminId">Select listings that can be administered by the specified user.</param>
        /// <param name="organizationId">Select listings for products owned by the specified organization.</param>
        /// <param name="allStates">Select listings visible to the viewer even if they are not approved. If omitted or false, only approved listings will be returned.</param>
        /// <param name="slugs">Select the listings with these slugs, if they are visible to the viewer.</param>
        /// <param name="primaryCategoryOnly">Select only listings where the primary category matches the given category slug.</param>
        /// <param name="withFreeTrialsOnly">Select only listings that offer a free trial.</param>
        public MarketplaceListingConnection MarketplaceListings(int? first = null, string after = null, int? last = null, string before = null, string categorySlug = null, bool? viewerCanAdmin = null, string adminId = null, string organizationId = null, bool? allStates = null, IEnumerable<string> slugs = null, bool? primaryCategoryOnly = false, bool? withFreeTrialsOnly = false) => this.CreateMethodCall(x => x.MarketplaceListings(first, after, last, before, categorySlug, viewerCanAdmin, adminId, organizationId, allStates, slugs, primaryCategoryOnly, withFreeTrialsOnly), Octokit.GraphQL.Model.MarketplaceListingConnection.Create);

        /// <summary>
        /// Return information about the GitHub instance
        /// </summary>
        public GitHubMetadata Meta => this.CreateProperty(x => x.Meta, Octokit.GraphQL.Model.GitHubMetadata.Create);

        /// <summary>
        /// Fetches an object given its ID.
        /// </summary>
        /// <param name="id">ID of the object.</param>
        public INode Node(string id) => this.CreateMethodCall(x => x.Node(id), Octokit.GraphQL.Model.Internal.StubINode.Create);

        /// <summary>
        /// Lookup nodes by a list of IDs.
        /// </summary>
        /// <param name="ids">The list of node IDs.</param>
        public IQueryable<INode> Nodes(IEnumerable<string> ids) => this.CreateMethodCall(x => x.Nodes(ids));

        /// <summary>
        /// Lookup a organization by login.
        /// </summary>
        /// <param name="login">The organization's login.</param>
        public Organization Organization(string login) => this.CreateMethodCall(x => x.Organization(login), Octokit.GraphQL.Model.Organization.Create);

        /// <summary>
        /// The client's rate limit information.
        /// </summary>
        /// <param name="dryRun">If true, calculate the cost for the query without evaluating it</param>
        public RateLimit RateLimit(bool? dryRun = false) => this.CreateMethodCall(x => x.RateLimit(dryRun), Octokit.GraphQL.Model.RateLimit.Create);

        /// <summary>
        /// Hack to workaround https://github.com/facebook/relay/issues/112 re-exposing the root query object
        /// </summary>
        public Query Relay => this.CreateProperty(x => x.Relay, Octokit.GraphQL.Query.Create);

        /// <summary>
        /// Lookup a given repository by the owner and repository name.
        /// </summary>
        /// <param name="owner">The login field of a user or organization</param>
        /// <param name="name">The name of the repository</param>
        public Repository Repository(string owner, string name) => this.CreateMethodCall(x => x.Repository(owner, name), Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// Lookup a repository owner (ie. either a User or an Organization) by login.
        /// </summary>
        /// <param name="login">The username to lookup the owner by.</param>
        public IRepositoryOwner RepositoryOwner(string login) => this.CreateMethodCall(x => x.RepositoryOwner(login), Octokit.GraphQL.Model.Internal.StubIRepositoryOwner.Create);

        /// <summary>
        /// Lookup resource by a URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        public IUniformResourceLocatable Resource(string url) => this.CreateMethodCall(x => x.Resource(url), Octokit.GraphQL.Model.Internal.StubIUniformResourceLocatable.Create);

        /// <summary>
        /// Perform a search across resources.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="query">The search string to look for.</param>
        /// <param name="type">The types of search items to search within.</param>
        public SearchResultItemConnection Search(string query, SearchType type, int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Search(query, type, first, after, last, before), Octokit.GraphQL.Model.SearchResultItemConnection.Create);

        /// <summary>
        /// Look up a topic by name.
        /// </summary>
        /// <param name="name">The topic's name.</param>
        public Topic Topic(string name) => this.CreateMethodCall(x => x.Topic(name), Octokit.GraphQL.Model.Topic.Create);

        /// <summary>
        /// Lookup a user by login.
        /// </summary>
        /// <param name="login">The user's login.</param>
        public User User(string login) => this.CreateMethodCall(x => x.User(login), Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The currently authenticated user.
        /// </summary>
        public User Viewer => this.CreateProperty(x => x.Viewer, Octokit.GraphQL.Model.User.Create);

        internal static Query Create(IQueryProvider provider, Expression expression)
        {
            return new Query(provider, expression);
        }
    }
}