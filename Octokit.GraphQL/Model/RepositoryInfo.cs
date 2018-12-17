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
    /// A subset of repository info.
    /// </summary>
    public interface IRepositoryInfo : IQueryableValue<IRepositoryInfo>, IQueryableInterface
    {
        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The description of the repository.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The description of the repository rendered to HTML.
        /// </summary>
        string DescriptionHTML { get; }

        /// <summary>
        /// Returns how many forks there are of this repository in the whole network.
        /// </summary>
        int ForkCount { get; }

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
        /// Indicates if the repository is unmaintained.
        /// </summary>
        bool IsArchived { get; }

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
        License LicenseInfo { get; }

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
        DateTimeOffset? PushedAt { get; }

        /// <summary>
        /// The HTTP path for this repository
        /// </summary>
        string ResourcePath { get; }

        /// <summary>
        /// A description of the repository, rendered to HTML without any links in it.
        /// </summary>
        /// <param name="limit">How many characters to return.</param>
        string ShortDescriptionHTML(Arg<int>? limit = null);

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this repository
        /// </summary>
        string Url { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIRepositoryInfo : QueryableValue<StubIRepositoryInfo>, IRepositoryInfo
    {
        internal StubIRepositoryInfo(Expression expression) : base(expression)
        {
        }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public DateTimeOffset CreatedAt { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public string Description { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public string DescriptionHTML { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public int ForkCount { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public bool HasIssuesEnabled { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public bool HasWikiEnabled { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public string HomepageUrl { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public bool IsArchived { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public bool IsFork { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public bool IsLocked { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public bool IsMirror { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public bool IsPrivate { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public License LicenseInfo => this.CreateProperty(x => x.LicenseInfo, Octokit.GraphQL.Model.License.Create);

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public RepositoryLockReason? LockReason { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public string MirrorUrl { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public string Name { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public string NameWithOwner { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public IRepositoryOwner Owner => this.CreateProperty(x => x.Owner, Octokit.GraphQL.Model.Internal.StubIRepositoryOwner.Create);

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public DateTimeOffset? PushedAt { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public string ResourcePath { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public string ShortDescriptionHTML(Arg<int>? limit = null) => default;

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public DateTimeOffset UpdatedAt { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public string Url { get; }

        internal static StubIRepositoryInfo Create(Expression expression)
        {
            return new StubIRepositoryInfo(expression);
        }
    }
}