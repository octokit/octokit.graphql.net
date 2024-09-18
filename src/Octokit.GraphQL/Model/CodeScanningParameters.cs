namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Choose which tools must provide code scanning results before the reference is updated. When configured, code scanning must be enabled and have results for both the commit and the reference being updated.
    /// </summary>
    public class CodeScanningParameters : QueryableValue<CodeScanningParameters>
    {
        internal CodeScanningParameters(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Tools that must provide code scanning results for this rule to pass.
        /// </summary>
        public IQueryableList<CodeScanningTool> CodeScanningTools => this.CreateProperty(x => x.CodeScanningTools);

        internal static CodeScanningParameters Create(Expression expression)
        {
            return new CodeScanningParameters(expression);
        }
    }
}