namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An object that can have users assigned to it.
    /// </summary>
    public interface IAssignable : IQueryableValue<IAssignable>, IQueryableInterface
    {
        /// <summary>
        /// A list of Users assigned to this object.
        /// </summary>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        UserConnection Assignees(Arg<string>? after = null, Arg<string>? before = null, Arg<int>? first = null, Arg<int>? last = null);
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIAssignable : QueryableValue<StubIAssignable>, IAssignable
    {
        public StubIAssignable(Expression expression) : base(expression)
        {
        }

        public UserConnection Assignees(Arg<string>? after = null, Arg<string>? before = null, Arg<int>? first = null, Arg<int>? last = null) => this.CreateMethodCall(x => x.Assignees(after, before, first, last), Octokit.GraphQL.Model.UserConnection.Create);

        internal static StubIAssignable Create(Expression expression)
        {
            return new StubIAssignable(expression);
        }
    }
}