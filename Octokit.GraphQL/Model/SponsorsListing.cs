namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A GitHub Sponsors listing.
    /// </summary>
    public class SponsorsListing : QueryableValue<SponsorsListing>
    {
        internal SponsorsListing(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The full description of the listing.
        /// </summary>
        public string FullDescription { get; }

        /// <summary>
        /// The full description of the listing rendered to HTML.
        /// </summary>
        public string FullDescriptionHTML { get; }

        public ID Id { get; }

        /// <summary>
        /// The listing's full name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The short description of the listing.
        /// </summary>
        public string ShortDescription { get; }

        /// <summary>
        /// The short name of the listing.
        /// </summary>
        public string Slug { get; }

        /// <summary>
        /// The published tiers for this GitHub Sponsors listing.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for Sponsors tiers returned from the connection.</param>
        public SponsorsTierConnection Tiers(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SponsorsTierOrder>? orderBy = null) => this.CreateMethodCall(x => x.Tiers(first, after, last, before, orderBy), Octokit.GraphQL.Model.SponsorsTierConnection.Create);

        internal static SponsorsListing Create(Expression expression)
        {
            return new SponsorsListing(expression);
        }
    }
}