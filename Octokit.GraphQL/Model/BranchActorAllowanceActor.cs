namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Types which can be actors for `BranchActorAllowance` objects.
    /// </summary>
    public class BranchActorAllowanceActor : QueryableValue<BranchActorAllowanceActor>, IUnion
    {
        internal BranchActorAllowanceActor(Expression expression) : base(expression)
        {
        }

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {
            /// <summary>
            /// A GitHub App.
            /// </summary>
            public Selector<T> App(Func<App, T> selector) => default;

            /// <summary>
            /// A team of users in an organization.
            /// </summary>
            public Selector<T> Team(Func<Team, T> selector) => default;

            /// <summary>
            /// A user is an individual's account on GitHub that owns repositories and can make new content.
            /// </summary>
            public Selector<T> User(Func<User, T> selector) => default;
        }

        internal static BranchActorAllowanceActor Create(Expression expression)
        {
            return new BranchActorAllowanceActor(expression);
        }
    }
}