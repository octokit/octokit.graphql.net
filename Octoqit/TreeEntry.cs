namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a Git tree entry.
    /// </summary>
    public class TreeEntry : QueryEntity
    {
        public TreeEntry(IQueryProvider provider, Expression expression) : base(provider, expression)
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
        public IGitObject Object => this.CreateProperty(x => x.Object, Octokit.GraphQL.Internal.StubIGitObject.Create);

        /// <summary>
        /// Entry file Git object ID.
        /// </summary>
        public string Oid { get; }

        /// <summary>
        /// The Repository the tree entry belongs to
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Repository.Create);

        /// <summary>
        /// Entry file type.
        /// </summary>
        public string Type { get; }

        internal static TreeEntry Create(IQueryProvider provider, Expression expression)
        {
            return new TreeEntry(provider, expression);
        }
    }
}