namespace Octokit.GraphQL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An object that can have users assigned to it.
    /// </summary>
    public interface IAssignable : IQueryEntity
    {
        /// <summary>
        /// A list of Users assigned to this object.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        UserConnection Assignees(int? first = null, string after = null, int? last = null, string before = null);
    }
}

namespace Octokit.GraphQL.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIAssignable : QueryEntity, IAssignable
    {
        public StubIAssignable(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public UserConnection Assignees(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Assignees(first, after, last, before), Octokit.GraphQL.UserConnection.Create);

        internal static StubIAssignable Create(IQueryProvider provider, Expression expression)
        {
            return new StubIAssignable(provider, expression);
        }
    }
}