namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information from a check run analysis to specific lines of code.
    /// </summary>
    public class CheckAnnotationRange
    {
        public int StartLine { get; set; }

        public int? StartColumn { get; set; }

        public int EndLine { get; set; }

        public int? EndColumn { get; set; }
    }
}