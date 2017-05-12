namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An object that can have labels assigned to it.
    /// </summary>
    public interface ILabelable : IQueryEntity
    {
        /// <summary>
        /// A list of labels associated with the object.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        LabelConnection Labels(int? first = null, string after = null, int? last = null, string before = null);
    }
}

namespace Octokit.GraphQL.Internal
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubILabelable : QueryEntity, ILabelable
    {
        public StubILabelable(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public LabelConnection Labels(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Labels(first, after, last, before), Octokit.GraphQL.LabelConnection.Create);

        internal static StubILabelable Create(IQueryProvider provider, Expression expression)
        {
            return new StubILabelable(provider, expression);
        }
    }
}