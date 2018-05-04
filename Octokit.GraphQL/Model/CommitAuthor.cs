namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Specifies an author for filtering Git commits.
    /// </summary>
    public class CommitAuthor
    {
        public ID? Id { get; set; }

        public IEnumerable<string> Emails { get; set; }
    }
}