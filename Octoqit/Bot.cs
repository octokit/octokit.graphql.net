namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// A special type of user which takes actions on behalf of integrations.
    /// </summary>
    public class Bot : QueryEntity
    {
        public Bot(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A URL pointing to the owner's public avatar.
        /// </summary>
        /// <param name="size">The size of the resulting square image.</param>
        public IQueryable<string> AvatarURL(int? size) => this.CreateMethodCall(x => x.AvatarURL(size));

        public string Id { get; }

        /// <summary>
        /// The username of the author.
        /// </summary>
        public string Login { get; }

        /// <summary>
        /// The path to this resource.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// The URL to this resource.
        /// </summary>
        public string Url { get; }

        internal static Bot Create(IQueryProvider provider, Expression expression)
        {
            return new Bot(provider, expression);
        }
    }
}