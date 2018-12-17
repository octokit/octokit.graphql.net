namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A check run.
    /// </summary>
    public class CheckRun : QueryableValue<CheckRun>
    {
        internal CheckRun(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The check run's annotations
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public CheckAnnotationConnection Annotations(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Annotations(first, after, last, before), Octokit.GraphQL.Model.CheckAnnotationConnection.Create);

        /// <summary>
        /// The check suite that this run is a part of.
        /// </summary>
        public CheckSuite CheckSuite => this.CreateProperty(x => x.CheckSuite, Octokit.GraphQL.Model.CheckSuite.Create);

        /// <summary>
        /// Identifies the date and time when the check run was completed.
        /// </summary>
        public DateTimeOffset? CompletedAt { get; }

        /// <summary>
        /// The conclusion of the check run.
        /// </summary>
        public CheckConclusionState? Conclusion { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; }

        /// <summary>
        /// The URL from which to find full details of the check run on the integrator's site.
        /// </summary>
        public string DetailsUrl { get; }

        /// <summary>
        /// A reference for the check run on the integrator's system.
        /// </summary>
        public string ExternalId { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public ID Id { get; }

        /// <summary>
        /// The name of the check for this check run.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The permalink to the check run summary.
        /// </summary>
        public string Permalink { get; }

        /// <summary>
        /// The repository associated with this check run.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The HTTP path for this check run.
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// Identifies the date and time when the check run was started.
        /// </summary>
        public DateTimeOffset? StartedAt { get; }

        /// <summary>
        /// The current status of the check run.
        /// </summary>
        public CheckStatusState Status { get; }

        /// <summary>
        /// A string representing the check run's summary
        /// </summary>
        public string Summary { get; }

        /// <summary>
        /// A string representing the check run's text
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// A string representing the check run
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// The HTTP URL for this check run.
        /// </summary>
        public string Url { get; }

        internal static CheckRun Create(Expression expression)
        {
            return new CheckRun(expression);
        }
    }
}