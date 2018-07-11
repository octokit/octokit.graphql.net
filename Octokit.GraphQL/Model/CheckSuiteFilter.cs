namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The filters that are available when fetching check suites.
    /// </summary>
    public class CheckSuiteFilter
    {
        public int? AppId { get; set; }

        public string CheckName { get; set; }
    }
}