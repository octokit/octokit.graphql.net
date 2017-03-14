namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Represents a Git tag.
    /// </summary>
    public class Tag : QueryEntity
    {
        public Tag(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public string Id { get; }

        /// <summary>
        /// The Git tag message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// The Git tag name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The Git object ID
        /// </summary>
        public string Oid { get; }

        /// <summary>
        /// The Repository the Git object belongs to
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octoqit.Repository.Create);

        /// <summary>
        /// Details about the tag author.
        /// </summary>
        public GitActor Tagger => this.CreateProperty(x => x.Tagger, Octoqit.GitActor.Create);

        /// <summary>
        /// The Git object the tag points to.
        /// </summary>
        public IGitObject Target => this.CreateProperty(x => x.Target, Octoqit.Internal.StubIGitObject.Create);

        internal static Tag Create(IQueryProvider provider, Expression expression)
        {
            return new Tag(provider, expression);
        }
    }
}