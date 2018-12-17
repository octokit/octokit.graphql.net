namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a single day of contributions on GitHub by a user.
    /// </summary>
    public class ContributionCalendarDay : QueryableValue<ContributionCalendarDay>
    {
        internal ContributionCalendarDay(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The hex color code that represents how many contributions were made on this day compared to others in the calendar.
        /// </summary>
        public string Color { get; }

        /// <summary>
        /// How many contributions were made by the user on this day.
        /// </summary>
        public int ContributionCount { get; }

        /// <summary>
        /// The day this square represents.
        /// </summary>
        public string Date { get; }

        /// <summary>
        /// A number representing which day of the week this square represents, e.g., 1 is Monday.
        /// </summary>
        public int Weekday { get; }

        internal static ContributionCalendarDay Create(Expression expression)
        {
            return new ContributionCalendarDay(expression);
        }
    }
}