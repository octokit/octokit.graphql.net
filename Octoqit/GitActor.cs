namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Represents an actor in a Git commit (ie. an author or committer).
    /// </summary>
    public class GitActor : QueryEntity
    {
        public GitActor(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A URL pointing to the author's public avatar.
        /// </summary>
        /// <param name="size">The size of the resulting square image.</param>
        public IQueryable<string> AvatarURL(int? size) => this.CreateMethodCall(x => x.AvatarURL(size));

        /// <summary>
        /// The timestamp of the Git action (authoring or committing).
        /// </summary>
        public string Date { get; }

        /// <summary>
        /// The email in the Git commit.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// The name in the Git commit.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The GitHub user corresponding to the email field. Null if no such user exists.
        /// </summary>
        public User User => this.CreateProperty(x => x.User, Octoqit.User.Create);

        internal static GitActor Create(IQueryProvider provider, Expression expression)
        {
            return new GitActor(provider, expression);
        }
    }
}