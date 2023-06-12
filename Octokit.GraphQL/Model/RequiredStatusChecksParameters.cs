namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Choose which status checks must pass before branches can be merged into a branch that matches this rule. When enabled, commits must first be pushed to another branch, then merged or pushed directly to a branch that matches this rule after status checks have passed.
    /// </summary>
    public class RequiredStatusChecksParameters : QueryableValue<RequiredStatusChecksParameters>
    {
        internal RequiredStatusChecksParameters(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Status checks that are required.
        /// </summary>
        public IQueryableList<StatusCheckConfiguration> RequiredStatusChecks => this.CreateProperty(x => x.RequiredStatusChecks);

        /// <summary>
        /// Whether pull requests targeting a matching branch must be tested with the latest code. This setting will not take effect unless at least one status check is enabled.
        /// </summary>
        public bool StrictRequiredStatusChecksPolicy { get; }

        internal static RequiredStatusChecksParameters Create(Expression expression)
        {
            return new RequiredStatusChecksParameters(expression);
        }
    }
}