namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Possible further actions the integrator can perform.
    /// </summary>
    public class CheckRunAction
    {
        public string Label { get; set; }

        public string Description { get; set; }

        public string Identifier { get; set; }
    }
}