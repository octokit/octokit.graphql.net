using System;

namespace PagingDemo.Models
{
    internal class PullRequestReviewCommentModel
    {
        public int Id { get; set; }
        public string NodeId { get; set; }
        public string Body { get; set; }
        public string CommitId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string DiffHunk { get; set; }
        public string OriginalCommitId { get; set; }
        public int OriginalPosition { get; set; }
        public string Path { get; set; }
        public int? Position { get; set; }
        public AuthorModel User { get; set; }
    }
}