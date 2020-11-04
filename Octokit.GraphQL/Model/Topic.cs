namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A topic aggregates entities that are related to a subject.
    /// </summary>
    public class Topic : QueryableValue<Topic>
    {
        internal Topic(Expression expression) : base(expression)
        {
        }

        public ID Id { get; }

        /// <summary>
        /// The topic's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A list of related topics, including aliases of this topic, sorted with the most relevant
        /// first. Returns up to 10 Topics.
        /// </summary>
        /// <param name="first">How many topics to return.</param>
        public IQueryableList<Topic> RelatedTopics(Arg<int>? first = null) => this.CreateMethodCall(x => x.RelatedTopics(first));

        /// <summary>
        /// Returns a count of how many stargazers there are on this object
        /// </summary>
        public int StargazerCount { get; }

        /// <summary>
        /// A list of users who have starred this starrable.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Order for connection</param>
        public StargazerConnection Stargazers(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<StarOrder>? orderBy = null) => this.CreateMethodCall(x => x.Stargazers(first, after, last, before, orderBy), Octokit.GraphQL.Model.StargazerConnection.Create);

        /// <summary>
        /// Returns a boolean indicating whether the viewing user has starred this starrable.
        /// </summary>
        public bool ViewerHasStarred { get; }

        internal static Topic Create(Expression expression)
        {
            return new Topic(expression);
        }
    }
}