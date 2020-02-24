namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An account to manage multiple organizations with consolidated policy and billing.
    /// </summary>
    public class Enterprise : QueryableValue<Enterprise>
    {
        internal Enterprise(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A URL pointing to the enterprise's public avatar.
        /// </summary>
        /// <param name="size">The size of the resulting square image.</param>
        public string AvatarUrl(Arg<int>? size = null) => default;

        /// <summary>
        /// Enterprise billing information visible to enterprise billing managers.
        /// </summary>
        public EnterpriseBillingInfo BillingInfo => this.CreateProperty(x => x.BillingInfo, Octokit.GraphQL.Model.EnterpriseBillingInfo.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; }

        /// <summary>
        /// The description of the enterprise.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The description of the enterprise as HTML.
        /// </summary>
        public string DescriptionHTML { get; }

        public ID Id { get; }

        /// <summary>
        /// The location of the enterprise.
        /// </summary>
        public string Location { get; }

        /// <summary>
        /// A list of users who are members of this enterprise.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="deployment">Only return members within the selected GitHub Enterprise deployment</param>
        /// <param name="orderBy">Ordering options for members returned from the connection.</param>
        /// <param name="organizationLogins">Only return members within the organizations with these logins</param>
        /// <param name="query">The search string to look for.</param>
        /// <param name="role">The role of the user in the enterprise organization or server.</param>
        public EnterpriseMemberConnection Members(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<EnterpriseUserDeployment>? deployment = null, Arg<EnterpriseMemberOrder>? orderBy = null, Arg<IEnumerable<string>>? organizationLogins = null, Arg<string>? query = null, Arg<EnterpriseUserAccountMembershipRole>? role = null) => this.CreateMethodCall(x => x.Members(first, after, last, before, deployment, orderBy, organizationLogins, query, role), Octokit.GraphQL.Model.EnterpriseMemberConnection.Create);

        /// <summary>
        /// The name of the enterprise.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A list of organizations that belong to this enterprise.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for organizations returned from the connection.</param>
        /// <param name="query">The search string to look for.</param>
        public OrganizationConnection Organizations(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<OrganizationOrder>? orderBy = null, Arg<string>? query = null) => this.CreateMethodCall(x => x.Organizations(first, after, last, before, orderBy, query), Octokit.GraphQL.Model.OrganizationConnection.Create);

        /// <summary>
        /// Enterprise information only visible to enterprise owners.
        /// </summary>
        public EnterpriseOwnerInfo OwnerInfo => this.CreateProperty(x => x.OwnerInfo, Octokit.GraphQL.Model.EnterpriseOwnerInfo.Create);

        /// <summary>
        /// The HTTP path for this enterprise.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The HTTP URL for this enterprise.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// A list of user accounts on this enterprise.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public EnterpriseUserAccountConnection UserAccounts(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.UserAccounts(first, after, last, before), Octokit.GraphQL.Model.EnterpriseUserAccountConnection.Create);

        /// <summary>
        /// Is the current viewer an admin of this enterprise?
        /// </summary>
        public bool ViewerIsAdmin { get; }

        /// <summary>
        /// The URL of the enterprise website.
        /// </summary>
        public string WebsiteUrl { get; }

        internal static Enterprise Create(Expression expression)
        {
            return new Enterprise(expression);
        }
    }
}