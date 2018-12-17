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
    /// Represents a Git object.
    /// </summary>
    public interface IGitObject : IQueryableValue<IGitObject>, IQueryableInterface
    {
        /// <summary>
        /// An abbreviated version of the Git object ID
        /// </summary>
        string AbbreviatedOid { get; }

        /// <summary>
        /// The HTTP path for this Git object
        /// </summary>
        string CommitResourcePath { get; }

        /// <summary>
        /// The HTTP URL for this Git object
        /// </summary>
        string CommitUrl { get; }

        ID Id { get; }

        /// <summary>
        /// The Git object ID
        /// </summary>
        string Oid { get; }

        /// <summary>
        /// The Repository the Git object belongs to
        /// </summary>
        Repository Repository { get; }
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

    internal class StubIGitObject : QueryableValue<StubIGitObject>, IGitObject
    {
        internal StubIGitObject(Expression expression) : base(expression)
        {
        }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public string AbbreviatedOid { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public string CommitResourcePath { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public string CommitUrl { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public ID Id { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public string Oid { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        internal static StubIGitObject Create(Expression expression)
        {
            return new StubIGitObject(expression);
        }
    }
}