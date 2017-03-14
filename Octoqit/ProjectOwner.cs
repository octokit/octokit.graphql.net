namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

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
        ProjectConnection Projects(int? first, string after, int? last, string before, ProjectOrder orderBy, string search, IQueryable<ProjectState> states);

        /// <summary>
        /// The HTTP path listing owners projects
        /// </summary>
        string ProjectsPath { get; }

        /// <summary>
        /// The HTTP url listing owners projects
        /// </summary>
        string ProjectsUrl { get; }

        /// <summary>
        /// Can the current viewer create new projects on this owner.
        /// </summary>
        bool ViewerCanCreateProjects { get; }
    }
}

namespace Octoqit.Internal
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    internal class StubIProjectOwner : QueryEntity, IProjectOwner
    {
        public StubIProjectOwner(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public string Id { get; }

        public Project Project(int number) => this.CreateMethodCall(x => x.Project(number), Octoqit.Project.Create);

        public ProjectConnection Projects(int? first, string after, int? last, string before, ProjectOrder orderBy, string search, IQueryable<ProjectState> states) => this.CreateMethodCall(x => x.Projects(first, after, last, before, orderBy, search, states), Octoqit.ProjectConnection.Create);

        public string ProjectsPath { get; }

        public string ProjectsUrl { get; }

        public bool ViewerCanCreateProjects { get; }

        internal static StubIProjectOwner Create(IQueryProvider provider, Expression expression)
        {
            return new StubIProjectOwner(provider, expression);
        }
    }
}