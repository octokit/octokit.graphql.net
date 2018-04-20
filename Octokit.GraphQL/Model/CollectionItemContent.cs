namespace Octokit.GraphQL.Model
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Types that can be inside Collection Items.
    /// </summary>
    public class CollectionItemContent : QueryableValue<CollectionItemContent>, IUnion
    {
        public CollectionItemContent(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A repository contains the content for a project.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// An account on GitHub, with one or more owners, that has repositories, members and teams.
        /// </summary>
        public Organization Organization => this.CreateProperty(x => x.Organization, Octokit.GraphQL.Model.Organization.Create);

        /// <summary>
        /// A user is an individual's account on GitHub that owns repositories and can make new content.
        /// </summary>
        public User User => this.CreateProperty(x => x.User, Octokit.GraphQL.Model.User.Create);

        internal static CollectionItemContent Create(Expression expression)
        {
            return new CollectionItemContent(expression);
        }
    }
}