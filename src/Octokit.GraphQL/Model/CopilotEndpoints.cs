namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Copilot endpoint information
    /// </summary>
    public class CopilotEndpoints : QueryableValue<CopilotEndpoints>
    {
        internal CopilotEndpoints(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Copilot API endpoint
        /// </summary>
        public string Api { get; }

        /// <summary>
        /// Copilot origin tracker endpoint
        /// </summary>
        public string OriginTracker { get; }

        /// <summary>
        /// Copilot proxy endpoint
        /// </summary>
        public string Proxy { get; }

        /// <summary>
        /// Copilot telemetry endpoint
        /// </summary>
        public string Telemetry { get; }

        internal static CopilotEndpoints Create(Expression expression)
        {
            return new CopilotEndpoints(expression);
        }
    }
}