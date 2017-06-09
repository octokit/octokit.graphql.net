namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a Milestone object on a given repository.
    /// </summary>
    public class Milestone : QueryEntity
    {
        public Milestone(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the actor who created the milestone.
        /// </summary>
        public IActor Creator => this.CreateProperty(x => x.Creator, Octokit.GraphQL.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the description of the milestone.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Identifies the due date of the milestone.
        /// </summary>
        public string DueOn { get; }

        public string Id { get; }

        /// <summary>
        /// Identifies the number of the milestone.
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// The repository associated with this milestone.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Repository.Create);

        /// <summary>
        /// The HTTP path for this milestone
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// Identifies the state of the milestone.
        /// </summary>
        public MilestoneState State { get; }

        /// <summary>
        /// Identifies the title of the milestone.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// The HTTP url for this milestone
        /// </summary>
        public string Url { get; }

        internal static Milestone Create(IQueryProvider provider, Expression expression)
        {
            return new Milestone(provider, expression);
        }
    }
}