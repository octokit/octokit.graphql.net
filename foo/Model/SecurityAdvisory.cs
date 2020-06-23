namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A GitHub Security Advisory
    /// </summary>
    public class SecurityAdvisory : QueryableValue<SecurityAdvisory>
    {
        internal SecurityAdvisory(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; }

        /// <summary>
        /// This is a long plaintext description of the advisory
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The GitHub Security Advisory ID
        /// </summary>
        public string GhsaId { get; }

        public ID Id { get; }

        /// <summary>
        /// A list of identifiers for this advisory
        /// </summary>
        public IQueryableList<SecurityAdvisoryIdentifier> Identifiers => this.CreateProperty(x => x.Identifiers);

        /// <summary>
        /// The organization that originated the advisory
        /// </summary>
        public string Origin { get; }

        /// <summary>
        /// The permalink for the advisory
        /// </summary>
        public string Permalink { get; }

        /// <summary>
        /// When the advisory was published
        /// </summary>
        public DateTimeOffset PublishedAt { get; }

        /// <summary>
        /// A list of references for this advisory
        /// </summary>
        public IQueryableList<SecurityAdvisoryReference> References => this.CreateProperty(x => x.References);

        /// <summary>
        /// The severity of the advisory
        /// </summary>
        public SecurityAdvisorySeverity Severity { get; }

        /// <summary>
        /// A short plaintext summary of the advisory
        /// </summary>
        public string Summary { get; }

        /// <summary>
        /// When the advisory was last updated
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// Vulnerabilities associated with this Advisory
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="ecosystem">An ecosystem to filter vulnerabilities by.</param>
        /// <param name="orderBy">Ordering options for the returned topics.</param>
        /// <param name="package">A package name to filter vulnerabilities by.</param>
        /// <param name="severities">A list of severities to filter vulnerabilities by.</param>
        public SecurityVulnerabilityConnection Vulnerabilities(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SecurityAdvisoryEcosystem>? ecosystem = null, Arg<SecurityVulnerabilityOrder>? orderBy = null, Arg<string>? package = null, Arg<IEnumerable<SecurityAdvisorySeverity>>? severities = null) => this.CreateMethodCall(x => x.Vulnerabilities(first, after, last, before, ecosystem, orderBy, package, severities), Octokit.GraphQL.Model.SecurityVulnerabilityConnection.Create);

        /// <summary>
        /// When the advisory was withdrawn, if it has been withdrawn
        /// </summary>
        public DateTimeOffset? WithdrawnAt { get; }

        internal static SecurityAdvisory Create(Expression expression)
        {
            return new SecurityAdvisory(expression);
        }
    }
}