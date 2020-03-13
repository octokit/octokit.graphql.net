namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A sponsorship relationship between a sponsor and a maintainer
    /// </summary>
    public class Sponsorship : QueryableValue<Sponsorship>
    {
        internal Sponsorship(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        public string Id { get; }

        /// <summary>
        /// The entity that is being sponsored
        /// </summary>
        [Obsolete(@"`Sponsorship.maintainer` will be removed. Use `Sponsorship.sponsorable` instead. Removal on 2020-04-01 UTC.")]
        public User Maintainer => this.CreateProperty(x => x.Maintainer, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The privacy level for this sponsorship.
        /// </summary>
        public SponsorshipPrivacy PrivacyLevel { get; }

        /// <summary>
        /// The entity that is sponsoring. Returns null if the sponsorship is private
        /// </summary>
        public User Sponsor => this.CreateProperty(x => x.Sponsor, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The entity that is being sponsored
        /// </summary>
        public ISponsorable Sponsorable => this.CreateProperty(x => x.Sponsorable, Octokit.GraphQL.Model.Internal.StubISponsorable.Create);

        /// <summary>
        /// The associated sponsorship tier
        /// </summary>
        public SponsorsTier Tier => this.CreateProperty(x => x.Tier, Octokit.GraphQL.Model.SponsorsTier.Create);

        internal static Sponsorship Create(Expression expression)
        {
            return new Sponsorship(expression);
        }
    }
}