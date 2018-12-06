namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Descriptive details about the check run.
    /// </summary>
    public class CheckRunOutput
    {
        public string Title { get; set; }

        public string Summary { get; set; }

        public string Text { get; set; }

        public IEnumerable<CheckAnnotationData> Annotations { get; set; }

        public IEnumerable<CheckRunOutputImage> Images { get; set; }
    }
}