namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Represents a Git blame.
    /// </summary>
    public class Blame : QueryEntity
    {
        public Blame(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// The list of ranges from a Git blame.
        /// </summary>
        public IQueryable<BlameRange> Ranges => this.CreateProperty(x => x.Ranges);

        internal static Blame Create(IQueryProvider provider, Expression expression)
        {
            return new Blame(provider, expression);
        }
    }
}