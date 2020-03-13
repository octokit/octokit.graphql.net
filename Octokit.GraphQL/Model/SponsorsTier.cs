namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A GitHub Sponsors tier associated with a GitHub Sponsors listing.
    /// </summary>
    public class SponsorsTier : QueryableValue<SponsorsTier>
    {
        internal SponsorsTier(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// SponsorsTier information only visible to users that can administer the associated Sponsors listing.
        /// </summary>
        public SponsorsTierAdminInfo AdminInfo => this.CreateProperty(x => x.AdminInfo, Octokit.GraphQL.Model.SponsorsTierAdminInfo.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The description of the tier.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The tier description rendered to HTML
        /// </summary>
        public string DescriptionHTML { get; }

        public string Id { get; }

        /// <summary>
        /// How much this tier costs per month in cents.
        /// </summary>
        public int MonthlyPriceInCents { get; }

        /// <summary>
        /// How much this tier costs per month in dollars.
        /// </summary>
        public int MonthlyPriceInDollars { get; }

        /// <summary>
        /// The name of the tier.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The sponsors listing that this tier belongs to.
        /// </summary>
        public SponsorsListing SponsorsListing => this.CreateProperty(x => x.SponsorsListing, Octokit.GraphQL.Model.SponsorsListing.Create);

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static SponsorsTier Create(Expression expression)
        {
            return new SponsorsTier(expression);
        }
    }
}