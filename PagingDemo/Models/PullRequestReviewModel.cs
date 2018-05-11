using System;
using System.Collections.Generic;
using Octokit.GraphQL.Model;

namespace PagingDemo.Models
{
    class PullRequestReviewModel
    {
        public int Id { get; set; }
        public string NodeId { get; set; }
        public string Body { get; set; }
        public string CommitId { get; set; }
        public PullRequestReviewState State { get; set; }
        public DateTimeOffset? SubmittedAt { get; set; }
        public object User { get; set; }
        public List<PullRequestReviewCommentModel> Comments { get; set; }
    }
}