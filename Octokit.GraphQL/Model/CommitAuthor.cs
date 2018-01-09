namespace Octokit.GraphQL.Model
{
    using System.Linq;

    /// <summary>
    /// Specifies an author for filtering Git commits.
    /// </summary>
    public class CommitAuthor
    {
        public string Id { get; set; }

        public IQueryable<string> Emails { get; set; }
    }
}