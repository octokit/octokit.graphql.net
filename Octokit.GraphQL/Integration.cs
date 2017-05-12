namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents an integration with GitHub.
    /// </summary>
    public class Integration : QueryEntity
    {
        public Integration(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; }

        public string Id { get; }

        /// <summary>
        /// A URL pointing to the integration's logo.
        /// </summary>
        /// <param name="size">The size of the resulting image.</param>
        public string LogoUrl(int? size = null) => null;

        /// <summary>
        /// The name of the integration.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The owner of this integration.
        /// </summary>
        public User Owner => this.CreateProperty(x => x.Owner, Octokit.GraphQL.User.Create);

        /// <summary>
        /// A slug based on the name of the integration for use in URLs.
        /// </summary>
        public string Slug { get; }

        /// <summary>
        /// The URL to the integration's homepage.
        /// </summary>
        public string Url { get; }

        internal static Integration Create(IQueryProvider provider, Expression expression)
        {
            return new Integration(provider, expression);
        }
    }
}