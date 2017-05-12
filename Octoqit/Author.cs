namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents an object which can create content. Typically a User or Bot.
    /// </summary>
    public interface IAuthor : IQueryEntity
    {
        /// <summary>
        /// A URL pointing to the author's public avatar.
        /// </summary>
        /// <param name="size">The size of the resulting square image.</param>
        string AvatarURL(int? size = null);

        /// <summary>
        /// The username of the author.
        /// </summary>
        string Login { get; }

        /// <summary>
        /// The path to this author.
        /// </summary>
        string Path { get; }
    }
}

namespace Octokit.GraphQL.Internal
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIAuthor : QueryEntity, IAuthor
    {
        public StubIAuthor(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public string AvatarURL(int? size = null) => null;

        public string Login { get; }

        public string Path { get; }

        internal static StubIAuthor Create(IQueryProvider provider, Expression expression)
        {
            return new StubIAuthor(provider, expression);
        }
    }
}