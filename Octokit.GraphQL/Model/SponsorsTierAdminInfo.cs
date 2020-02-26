namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// SponsorsTier information only visible to users that can administer the associated Sponsors listing.
    /// </summary>
    public class SponsorsTierAdminInfo : QueryableValue<SponsorsTierAdminInfo>
    {
        internal SponsorsTierAdminInfo(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The sponsorships associated with this tier.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="includePrivate">Whether or not to include private sponsorships in the result set</param>
        /// <param name="orderBy">Ordering options for sponsorships returned from this connection. If left blank, the sponsorships will be ordered based on relevancy to the viewer.</param>
        public SponsorshipConnection Sponsorships(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? includePrivate = null, Arg<SponsorshipOrder>? orderBy = null) => this.CreateMethodCall(x => x.Sponsorships(first, after, last, before, includePrivate, orderBy), Octokit.GraphQL.Model.SponsorshipConnection.Create);

        internal static SponsorsTierAdminInfo Create(Expression expression)
        {
            return new SponsorsTierAdminInfo(expression);
        }
    }
}