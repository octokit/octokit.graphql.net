namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A repository issue template.
    /// </summary>
    public class IssueTemplate : QueryableValue<IssueTemplate>
    {
        internal IssueTemplate(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The template purpose.
        /// </summary>
        public string About { get; }

        /// <summary>
        /// The suggested issue body.
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The template name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The suggested issue title.
        /// </summary>
        public string Title { get; }

        internal static IssueTemplate Create(Expression expression)
        {
            return new IssueTemplate(expression);
        }
    }
}