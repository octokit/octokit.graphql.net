namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A tool that must provide code scanning results for this rule to pass.
    /// </summary>
    public class CodeScanningTool : QueryableValue<CodeScanningTool>
    {
        internal CodeScanningTool(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The severity level at which code scanning results that raise alerts block a reference update. For more information on alert severity levels, see "[About code scanning alerts](${externalDocsUrl}/code-security/code-scanning/managing-code-scanning-alerts/about-code-scanning-alerts#about-alert-severity-and-security-severity-levels)."
        /// </summary>
        public string AlertsThreshold { get; }

        /// <summary>
        /// The severity level at which code scanning results that raise security alerts block a reference update. For more information on security severity levels, see "[About code scanning alerts](${externalDocsUrl}/code-security/code-scanning/managing-code-scanning-alerts/about-code-scanning-alerts#about-alert-severity-and-security-severity-levels)."
        /// </summary>
        public string SecurityAlertsThreshold { get; }

        /// <summary>
        /// The name of a code scanning tool
        /// </summary>
        public string Tool { get; }

        internal static CodeScanningTool Create(Expression expression)
        {
            return new CodeScanningTool(expression);
        }
    }
}