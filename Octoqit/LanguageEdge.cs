namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Represents the language of a repository.
    /// </summary>
    public class LanguageEdge : QueryEntity
    {
        public LanguageEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public string Cursor { get; }

        public Language Node => this.CreateProperty(x => x.Node, Octoqit.Language.Create);

        /// <summary>
        /// The number of bytes of code written in the language.
        /// </summary>
        public int Size { get; }

        internal static LanguageEdge Create(IQueryProvider provider, Expression expression)
        {
            return new LanguageEdge(provider, expression);
        }
    }
}