namespace Octoqit
{
    using System.Linq;

    /// <summary>
    /// Specifies a review comment to be left with a Pull Request Review.
    /// </summary>
    public class DraftPullRequestReviewComment
    {
        public string Path { get; set; }

        public int Position { get; set; }

        public string Body { get; set; }
    }
}