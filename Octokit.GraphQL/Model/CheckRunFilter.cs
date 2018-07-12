namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The filters that are available when fetching check runs.
    /// </summary>
    public class CheckRunFilter
    {
        public CheckRunType? CheckType { get; set; }

        public int? AppId { get; set; }

        public string CheckName { get; set; }

        public CheckStatusState? Status { get; set; }
    }
}