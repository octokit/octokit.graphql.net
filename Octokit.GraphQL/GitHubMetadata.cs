namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents information about the GitHub instance.
    /// </summary>
    public class GitHubMetadata : QueryEntity
    {
        public GitHubMetadata(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Returns a String that's a SHA of `github-services`
        /// </summary>
        public string GitHubServicesSha { get; }

        /// <summary>
        /// IP addresses that users connect to for git operations
        /// </summary>
        public IQueryable<string> GitIpAddresses => this.CreateProperty(x => x.GitIpAddresses);

        /// <summary>
        /// IP addresses that service hooks are sent from
        /// </summary>
        public IQueryable<string> HookIpAddresses => this.CreateProperty(x => x.HookIpAddresses);

        /// <summary>
        /// IP addresses that the importer connects from
        /// </summary>
        public IQueryable<string> ImporterIpAddresses => this.CreateProperty(x => x.ImporterIpAddresses);

        /// <summary>
        /// Whether or not users are verified
        /// </summary>
        public bool IsPasswordAuthenticationVerifiable { get; }

        /// <summary>
        /// IP addresses for GitHub Pages' A records
        /// </summary>
        public IQueryable<string> PagesIpAddresses => this.CreateProperty(x => x.PagesIpAddresses);

        internal static GitHubMetadata Create(IQueryProvider provider, Expression expression)
        {
            return new GitHubMetadata(provider, expression);
        }
    }
}