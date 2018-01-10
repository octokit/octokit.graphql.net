namespace Octokit.GraphQL.Model
{
    using System;
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
        /// `true` if the object is closed (definition of closed may depend on type)
        /// </summary>
        public bool Closed { get; }

        /// <summary>
        /// Identifies the date and time when the object was closed.
        /// </summary>
        public string ClosedAt { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        /// <summary>
        /// Identifies the actor who created the milestone.
        /// </summary>
        public IActor Creator => this.CreateProperty(x => x.Creator, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the description of the milestone.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Identifies the due date of the milestone.
        /// </summary>
        public DateTimeOffset? DueOn { get; }

        public string Id { get; }

        /// <summary>
        /// A list of issues associated with the milestone.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="labels">A list of label names to filter the pull requests by.</param>
        /// <param name="orderBy">Ordering options for issues returned from the connection.</param>
        /// <param name="states">A list of states to filter the issues by.</param>
        public IssueConnection Issues(int? first = null, string after = null, int? last = null, string before = null, IEnumerable<string> labels = null, IssueOrder orderBy = null, IEnumerable<IssueState> states = null) => this.CreateMethodCall(x => x.Issues(first, after, last, before, labels, orderBy, states), Octokit.GraphQL.Model.IssueConnection.Create);

        /// <summary>
        /// Identifies the number of the milestone.
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// The repository associated with this milestone.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

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
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public string UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this milestone
        /// </summary>
        public string Url { get; }

        internal static Milestone Create(IQueryProvider provider, Expression expression)
        {
            return new Milestone(provider, expression);
        }
    }
}