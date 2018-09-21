namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Images attached to the check run output displayed in the GitHub pull request UI.
    /// </summary>
    public class CheckRunOutputImage
    {
        public string Alt { get; set; }

        public string ImageUrl { get; set; }

        public string Caption { get; set; }
    }
}