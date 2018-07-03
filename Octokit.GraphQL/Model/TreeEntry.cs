namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a Git tree entry.
    /// </summary>
    public class TreeEntry : QueryableValue<TreeEntry>
    {
        public TreeEntry(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Entry file mode.
        /// </summary>
        public int Mode { get; }

        /// <summary>
        /// Entry file name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Entry file object.
        /// </summary>
        public IGitObject Object => this.CreateProperty(x => x.Object, Octokit.GraphQL.Model.Internal.StubIGitObject.Create);

        /// <summary>
        /// Entry file Git object ID.
        /// </summary>
        public string Oid { get; }

        /// <summary>
        /// The Repository the tree entry belongs to
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// Entry file type.
        /// </summary>
        public string Type { get; }

        internal static TreeEntry Create(Expression expression)
        {
            return new TreeEntry(expression);
        }
    }
}