namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Entities that can be sponsored through GitHub Sponsors
    /// </summary>
    public interface ISponsorable : IQueryableValue<ISponsorable>, IQueryableInterface
    {
        /// <summary>
        /// The estimated next GitHub Sponsors payout for this user/organization in cents (USD).
        /// </summary>
        int EstimatedNextSponsorsPayoutInCents { get; }

        /// <summary>
        /// True if this user/organization has a GitHub Sponsors listing.
        /// </summary>
        bool HasSponsorsListing { get; }

        /// <summary>
        /// Check if the given account is sponsoring this user/organization.
        /// </summary>
        /// <param name="accountLogin">The target account's login.</param>
        bool IsSponsoredBy(Arg<string> accountLogin);

        /// <summary>
        /// True if the viewer is sponsored by this user/organization.
        /// </summary>
        bool IsSponsoringViewer { get; }

        /// <summary>
        /// The estimated monthly GitHub Sponsors income for this user/organization in cents (USD).
        /// </summary>
        int MonthlyEstimatedSponsorsIncomeInCents { get; }

        /// <summary>
        /// List of users and organizations this entity is sponsoring.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for the users and organizations returned from the connection.</param>
        SponsorConnection Sponsoring(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SponsorOrder>? orderBy = null);

        /// <summary>
        /// List of sponsors for this user or organization.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for sponsors returned from the connection.</param>
        /// <param name="tierId">If given, will filter for sponsors at the given tier. Will only return sponsors whose tier the viewer is permitted to see.</param>
        SponsorConnection Sponsors(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SponsorOrder>? orderBy = null, Arg<ID>? tierId = null);

        /// <summary>
        /// Events involving this sponsorable, such as new sponsorships.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for activity returned from the connection.</param>
        /// <param name="period">Filter activities returned to only those that occurred in a given time range.</param>
        SponsorsActivityConnection SponsorsActivities(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SponsorsActivityOrder>? orderBy = null, Arg<SponsorsActivityPeriod>? period = null);

        /// <summary>
        /// The GitHub Sponsors listing for this user or organization.
        /// </summary>
        SponsorsListing SponsorsListing { get; }

        /// <summary>
        /// The sponsorship from the viewer to this user/organization; that is, the sponsorship where you're the sponsor. Only returns a sponsorship if it is active.
        /// </summary>
        Sponsorship SponsorshipForViewerAsSponsor { get; }

        /// <summary>
        /// The sponsorship from this user/organization to the viewer; that is, the sponsorship you're receiving. Only returns a sponsorship if it is active.
        /// </summary>
        Sponsorship SponsorshipForViewerAsSponsorable { get; }

        /// <summary>
        /// List of sponsorship updates sent from this sponsorable to sponsors.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for sponsorship updates returned from the connection.</param>
        SponsorshipNewsletterConnection SponsorshipNewsletters(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SponsorshipNewsletterOrder>? orderBy = null);

        /// <summary>
        /// This object's sponsorships as the maintainer.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="includePrivate">Whether or not to include private sponsorships in the result set</param>
        /// <param name="orderBy">Ordering options for sponsorships returned from this connection. If left blank, the sponsorships will be ordered based on relevancy to the viewer.</param>
        SponsorshipConnection SponsorshipsAsMaintainer(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? includePrivate = null, Arg<SponsorshipOrder>? orderBy = null);

        /// <summary>
        /// This object's sponsorships as the sponsor.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for sponsorships returned from this connection. If left blank, the sponsorships will be ordered based on relevancy to the viewer.</param>
        SponsorshipConnection SponsorshipsAsSponsor(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SponsorshipOrder>? orderBy = null);

        /// <summary>
        /// Whether or not the viewer is able to sponsor this user/organization.
        /// </summary>
        bool ViewerCanSponsor { get; }

        /// <summary>
        /// True if the viewer is sponsoring this user/organization.
        /// </summary>
        bool ViewerIsSponsoring { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubISponsorable : QueryableValue<StubISponsorable>, ISponsorable
    {
        internal StubISponsorable(Expression expression) : base(expression)
        {
        }

        public int EstimatedNextSponsorsPayoutInCents { get; }

        public bool HasSponsorsListing { get; }

        public bool IsSponsoredBy(Arg<string> accountLogin) => default;

        public bool IsSponsoringViewer { get; }

        public int MonthlyEstimatedSponsorsIncomeInCents { get; }

        public SponsorConnection Sponsoring(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SponsorOrder>? orderBy = null) => this.CreateMethodCall(x => x.Sponsoring(first, after, last, before, orderBy), Octokit.GraphQL.Model.SponsorConnection.Create);

        public SponsorConnection Sponsors(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SponsorOrder>? orderBy = null, Arg<ID>? tierId = null) => this.CreateMethodCall(x => x.Sponsors(first, after, last, before, orderBy, tierId), Octokit.GraphQL.Model.SponsorConnection.Create);

        public SponsorsActivityConnection SponsorsActivities(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SponsorsActivityOrder>? orderBy = null, Arg<SponsorsActivityPeriod>? period = null) => this.CreateMethodCall(x => x.SponsorsActivities(first, after, last, before, orderBy, period), Octokit.GraphQL.Model.SponsorsActivityConnection.Create);

        public SponsorsListing SponsorsListing => this.CreateProperty(x => x.SponsorsListing, Octokit.GraphQL.Model.SponsorsListing.Create);

        public Sponsorship SponsorshipForViewerAsSponsor => this.CreateProperty(x => x.SponsorshipForViewerAsSponsor, Octokit.GraphQL.Model.Sponsorship.Create);

        public Sponsorship SponsorshipForViewerAsSponsorable => this.CreateProperty(x => x.SponsorshipForViewerAsSponsorable, Octokit.GraphQL.Model.Sponsorship.Create);

        public SponsorshipNewsletterConnection SponsorshipNewsletters(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SponsorshipNewsletterOrder>? orderBy = null) => this.CreateMethodCall(x => x.SponsorshipNewsletters(first, after, last, before, orderBy), Octokit.GraphQL.Model.SponsorshipNewsletterConnection.Create);

        public SponsorshipConnection SponsorshipsAsMaintainer(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? includePrivate = null, Arg<SponsorshipOrder>? orderBy = null) => this.CreateMethodCall(x => x.SponsorshipsAsMaintainer(first, after, last, before, includePrivate, orderBy), Octokit.GraphQL.Model.SponsorshipConnection.Create);

        public SponsorshipConnection SponsorshipsAsSponsor(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<SponsorshipOrder>? orderBy = null) => this.CreateMethodCall(x => x.SponsorshipsAsSponsor(first, after, last, before, orderBy), Octokit.GraphQL.Model.SponsorshipConnection.Create);

        public bool ViewerCanSponsor { get; }

        public bool ViewerIsSponsoring { get; }

        internal static StubISponsorable Create(Expression expression)
        {
            return new StubISponsorable(expression);
        }
    }
}