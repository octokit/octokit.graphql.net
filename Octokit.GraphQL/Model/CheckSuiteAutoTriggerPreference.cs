namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The auto-trigger preferences that are available for check suites.
    /// </summary>
    public class CheckSuiteAutoTriggerPreference
    {
        public ID AppId { get; set; }

        public bool Setting { get; set; }
    }
}