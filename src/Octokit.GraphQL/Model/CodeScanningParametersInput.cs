namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Choose which tools must provide code scanning results before the reference is updated. When configured, code scanning must be enabled and have results for both the commit and the reference being updated.
    /// </summary>
    public class CodeScanningParametersInput
    {
        /// <summary>
        /// Tools that must provide code scanning results for this rule to pass.
        /// </summary>
        public IEnumerable<CodeScanningToolInput> CodeScanningTools { get; set; }
    }
}