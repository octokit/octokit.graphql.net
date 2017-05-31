namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A subset of repository info.
    /// </summary>
    public interface IRepositoryInfo : IQueryEntity
    {
        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        string CreatedAt { get; }

        /// <summary>
        /// The description of the repository.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The description of the repository rendered to HTML.
        /// </summary>
        string DescriptionHTML { get; }

        /// <summary>
        /// Indicates if the repository has issues feature enabled.
        /// </summary>
        bool HasIssuesEnabled { get; }

        /// <summary>
        /// Indicates if the repository has wiki feature enabled.
        /// </summary>
        bool HasWikiEnabled { get; }

        /// <summary>
        /// The repository's URL.
        /// </summary>
        string HomepageUrl { get; }

        /// <summary>
        /// Identifies if the repository is a fork.
        /// </summary>
        bool IsFork { get; }

        /// <summary>
        /// Indicates if the repository has been locked or not.
        /// </summary>
        bool IsLocked { get; }

        /// <summary>
        /// Identifies if the repository is a mirror.
        /// </summary>
        bool IsMirror { get; }

        /// <summary>
        /// Identifies if the repository is private.
        /// </summary>
        bool IsPrivate { get; }

        /// <summary>
        /// The license associated with the repository
        /// </summary>
        string License { get; }

        /// <summary>
        /// The reason the repository has been locked.
        /// </summary>
        RepositoryLockReason? LockReason { get; }

        /// <summary>
        /// The repository's original mirror URL.
        /// </summary>
        string MirrorUrl { get; }

        /// <summary>
        /// The name of the repository.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The repository's name with owner.
        /// </summary>
        string NameWithOwner { get; }

        /// <summary>
        /// The User owner of the repository.
        /// </summary>
        IRepositoryOwner Owner { get; }

        /// <summary>
        /// Identifies when the repository was last pushed to.
        /// </summary>
        string PushedAt { get; }

        /// <summary>
        /// The HTTP path for this repository
        /// </summary>
        string ResourcePath { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        string UpdatedAt { get; }

        /// <summary>
        /// The HTTP url for this repository
        /// </summary>
        string Url { get; }
    }
}

namespace Octokit.GraphQL.Internal
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIRepositoryInfo : QueryEntity, IRepositoryInfo
    {
        public StubIRepositoryInfo(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public string CreatedAt { get; }

        public string Description { get; }

        public string DescriptionHTML { get; }

        public bool HasIssuesEnabled { get; }

        public bool HasWikiEnabled { get; }

        public string HomepageUrl { get; }

        public bool IsFork { get; }

        public bool IsLocked { get; }

        public bool IsMirror { get; }

        public bool IsPrivate { get; }

        public string License { get; }

        public RepositoryLockReason? LockReason { get; }

        public string MirrorUrl { get; }

        public string Name { get; }

        public string NameWithOwner { get; }

        public IRepositoryOwner Owner => this.CreateProperty(x => x.Owner, Octokit.GraphQL.Internal.StubIRepositoryOwner.Create);

        public string PushedAt { get; }

        public string ResourcePath { get; }

        public string UpdatedAt { get; }

        public string Url { get; }

        internal static StubIRepositoryInfo Create(IQueryProvider provider, Expression expression)
        {
            return new StubIRepositoryInfo(provider, expression);
        }
    }
}