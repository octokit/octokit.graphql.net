namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A special type of user which takes actions on behalf of integrations.
    /// </summary>
    public class Bot : QueryEntity
    {
        public Bot(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A URL pointing to the integration's public avatar.
        /// </summary>
        /// <param name="size">The size of the resulting square image.</param>
        public string AvatarUrl(int? size = null) => null;

        public string Id { get; }

        /// <summary>
        /// The username of the actor.
        /// </summary>
        public string Login { get; }

        /// <summary>
        /// The HTTP path for this bot
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// The HTTP path for this bot
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The HTTP url for this bot
        /// </summary>
        public string Url { get; }

        internal static Bot Create(IQueryProvider provider, Expression expression)
        {
            return new Bot(provider, expression);
        }
    }
}