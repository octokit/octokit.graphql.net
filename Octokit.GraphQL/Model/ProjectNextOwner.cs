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
    /// Represents an owner of a project (beta).
    /// </summary>
    public interface IProjectNextOwner : IQueryableValue<IProjectNextOwner>, IQueryableInterface
    {
        ID Id { get; }

        /// <summary>
        /// Find a project by project (beta) number.
        /// </summary>
        /// <param name="number">The project (beta) number.</param>
        ProjectNext ProjectNext(Arg<int> number);

        /// <summary>
        /// A list of projects (beta) under the owner.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="query">A project (beta) to search for under the the owner.</param>
        /// <param name="sortBy">How to order the returned projects (beta).</param>
        ProjectNextConnection ProjectsNext(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<string>? query = null, Arg<ProjectNextOrderField>? sortBy = null);
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIProjectNextOwner : QueryableValue<StubIProjectNextOwner>, IProjectNextOwner
    {
        internal StubIProjectNextOwner(Expression expression) : base(expression)
        {
        }

        public ID Id { get; }

        public ProjectNext ProjectNext(Arg<int> number) => this.CreateMethodCall(x => x.ProjectNext(number), Octokit.GraphQL.Model.ProjectNext.Create);

        public ProjectNextConnection ProjectsNext(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<string>? query = null, Arg<ProjectNextOrderField>? sortBy = null) => this.CreateMethodCall(x => x.ProjectsNext(first, after, last, before, query, sortBy), Octokit.GraphQL.Model.ProjectNextConnection.Create);

        internal static StubIProjectNextOwner Create(Expression expression)
        {
            return new StubIProjectNextOwner(expression);
        }
    }
}