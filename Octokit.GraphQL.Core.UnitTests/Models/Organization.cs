using System.Collections.Generic;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class Organization : QueryableValue<Organization>
    {
        public Organization(Expression expression) : base(expression)
        {
        }

        public string AvatarUrl(Arg<int>? size = null) => null;

        public int? DatabaseId { get; }

        public string Description { get; }

        public string Email { get; }

        public ID Id { get; }

        public bool IsVerified { get; }

        public string Location { get; }

        public string Login { get; }

        public string Name { get; }

        public string NewTeamResourcePath { get; }

        public string NewTeamUrl { get; }

        public string OrganizationBillingEmail { get; }

        public RepositoryConnection PinnedRepositories(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<RepositoryAffiliation?>>? affiliations = null, Arg<bool>? isLocked = null, Arg<RepositoryOrder>? orderBy = null, Arg<IEnumerable<RepositoryAffiliation?>>? ownerAffiliations = null, Arg<RepositoryPrivacy>? privacy = null) => this.CreateMethodCall(x => x.PinnedRepositories(first, after, last, before, affiliations, isLocked, orderBy, ownerAffiliations, privacy), RepositoryConnection.Create);

        public string ProjectsResourcePath { get; }

        public string ProjectsUrl { get; }

        public RepositoryConnection Repositories(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<RepositoryAffiliation?>>? affiliations = null, Arg<bool>? isFork = null, Arg<bool>? isLocked = null, Arg<RepositoryOrder>? orderBy = null, Arg<IEnumerable<RepositoryAffiliation?>>? ownerAffiliations = null, Arg<RepositoryPrivacy>? privacy = null) => this.CreateMethodCall(x => x.Repositories(first, after, last, before, affiliations, isFork, isLocked, orderBy, ownerAffiliations, privacy), RepositoryConnection.Create);

        public Repository Repository(Arg<string> name) => this.CreateMethodCall(x => x.Repository(name), Models.Repository.Create);

        public bool? RequiresTwoFactorAuthentication { get; }

        public string ResourcePath { get; }

        public string TeamsResourcePath { get; }

        public string TeamsUrl { get; }

        public string Url { get; }

        public bool ViewerCanAdminister { get; }

        public bool ViewerCanCreateProjects { get; }

        public bool ViewerCanCreateRepositories { get; }

        public bool ViewerCanCreateTeams { get; }

        public bool ViewerIsAMember { get; }

        public string WebsiteUrl { get; }

        internal static Organization Create(Expression expression)
        {
            return new Organization(expression);
        }
    }
}