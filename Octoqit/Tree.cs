namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a Git tree.
    /// </summary>
    public class Tree : QueryEntity
    {
        public Tree(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A list of tree entries.
        /// </summary>
        public IQueryable<TreeEntry> Entries => this.CreateProperty(x => x.Entries);

        public string Id { get; }

        /// <summary>
        /// The Git object ID
        /// </summary>
        public string Oid { get; }

        /// <summary>
        /// The Repository the Git object belongs to
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Repository.Create);

        internal static Tree Create(IQueryProvider provider, Expression expression)
        {
            return new Tree(provider, expression);
        }
    }
}