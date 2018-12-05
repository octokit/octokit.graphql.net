using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class User : QueryableValue<User>
    {
        public User(Expression expression) : base(expression)
        {
        }

        public string AvatarUrl(Arg<int>? size = null) => null;

        public string Bio { get; }

        public string BioHTML { get; }

        public string Company { get; }

        public string CompanyHTML { get; }

        public DateTimeOffset CreatedAt { get; }

        public int? DatabaseId { get; }

        public string Email { get; }

        public ID Id { get; }

        public bool IsBountyHunter { get; }

        public bool IsCampusExpert { get; }

        public bool IsDeveloperProgramMember { get; }

        public bool IsEmployee { get; }

        public bool IsHireable { get; }

        public bool IsSiteAdmin { get; }

        public bool IsViewer { get; }

        public IssueCommentConnection IssueComments(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.IssueComments(first, after, last, before), IssueCommentConnection.Create);

        public string Location { get; }

        public string Login { get; }

        public string Name { get; }

        public Organization Organization(Arg<string> login) => this.CreateMethodCall(x => x.Organization(login), Models.Organization.Create);

        public OrganizationConnection Organizations(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Organizations(first, after, last, before), OrganizationConnection.Create);

        public RepositoryConnection Repositories(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<RepositoryAffiliation?>>? affiliations = null, Arg<bool>? isFork = null, Arg<bool>? isLocked = null, Arg<RepositoryOrder>? orderBy = null, Arg<IEnumerable<RepositoryAffiliation?>>? ownerAffiliations = null, Arg<RepositoryPrivacy>? privacy = null) => this.CreateMethodCall(x => x.Repositories(first, after, last, before, affiliations, isFork, isLocked, orderBy, ownerAffiliations, privacy), RepositoryConnection.Create);

        public Repository Repository(Arg<string> name) => this.CreateMethodCall(x => x.Repository(name), Models.Repository.Create);

        public string ResourcePath { get; }

        public DateTimeOffset UpdatedAt { get; }

        public string Url { get; }

        public bool ViewerCanFollow { get; }

        public bool ViewerIsFollowing { get; }

        public RepositoryConnection Watching(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<RepositoryAffiliation?>>? affiliations = null, Arg<bool>? isLocked = null, Arg<RepositoryOrder>? orderBy = null, Arg<IEnumerable<RepositoryAffiliation?>>? ownerAffiliations = null, Arg<RepositoryPrivacy>? privacy = null) => this.CreateMethodCall(x => x.Watching(first, after, last, before, affiliations, isLocked, orderBy, ownerAffiliations, privacy), RepositoryConnection.Create);

        public string WebsiteUrl { get; }

        internal static User Create(Expression expression)
        {
            return new User(expression);
        }
    }
}