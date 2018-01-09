namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a single highlight in a search result match.
    /// </summary>
    public class TextMatchHighlight : QueryEntity
    {
        public TextMatchHighlight(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// The indice in the fragment where the matched text begins.
        /// </summary>
        public int BeginIndice { get; }

        /// <summary>
        /// The indice in the fragment where the matched text ends.
        /// </summary>
        public int EndIndice { get; }

        /// <summary>
        /// The text matched.
        /// </summary>
        public string Text { get; }

        internal static TextMatchHighlight Create(IQueryProvider provider, Expression expression)
        {
            return new TextMatchHighlight(provider, expression);
        }
    }
}