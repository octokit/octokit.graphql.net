namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents an owner of a Project.
    /// </summary>
    public interface IProjectOwner : IQueryEntity
    {
        string Id { get; }

        /// <summary>
        /// Find project by number.
        /// </summary>
        /// <param name="number">The project number to find.</param>
        Project Project(int number);

        /// <summary>
        /// A list of projects under the owner.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="orderBy">Ordering options for projects returned from the connection</param>
        /// <param name="search">Query to search projects by, currently only searching by name.</param>
        /// <param name="states">A list of states to filter the projects by.</param>
        ProjectConnection Projects(int? first = null, string after = null, int? last = null, string before = null, ProjectOrder orderBy = null, string search = null, IEnumerable<ProjectState> states = null);

        /// <summary>
        /// The HTTP path listing owners projects
        /// </summary>
        string ProjectsResourcePath { get; }

        /// <summary>
        /// The HTTP URL listing owners projects
        /// </summary>
        string ProjectsUrl { get; }

        /// <summary>
        /// Can the current viewer create new projects on this owner.
        /// </summary>
        bool ViewerCanCreateProjects { get; }
    }
}

namespace Octokit.GraphQL.Internal
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIProjectOwner : QueryEntity, IProjectOwner
    {
        public StubIProjectOwner(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public string Id { get; }

        public Project Project(int number) => this.CreateMethodCall(x => x.Project(number), Octokit.GraphQL.Project.Create);

        public ProjectConnection Projects(int? first = null, string after = null, int? last = null, string before = null, ProjectOrder orderBy = null, string search = null, IEnumerable<ProjectState> states = null) => this.CreateMethodCall(x => x.Projects(first, after, last, before, orderBy, search, states), Octokit.GraphQL.ProjectConnection.Create);

        public string ProjectsResourcePath { get; }

        public string ProjectsUrl { get; }

        public bool ViewerCanCreateProjects { get; }

        internal static StubIProjectOwner Create(IQueryProvider provider, Expression expression)
        {
            return new StubIProjectOwner(provider, expression);
        }
    }
}