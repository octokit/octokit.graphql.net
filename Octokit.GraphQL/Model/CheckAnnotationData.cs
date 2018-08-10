namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information from a check run analysis to specific lines of code.
    /// </summary>
    public class CheckAnnotationData
    {
        public string Path { get; set; }

        public CheckAnnotationRange Location { get; set; }

        public CheckAnnotationLevel AnnotationLevel { get; set; }

        public string Message { get; set; }

        public string Title { get; set; }

        public string RawDetails { get; set; }
    }
}