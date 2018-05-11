using System;
using System.Collections.Generic;

namespace PagingDemo.Models
{
    class PullRequestModel
    {
        public string NodeId { get; set; }
        public AuthorModel Author { get; set; }
        public GitReferenceModel Base { get; set; }
        public string Body { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public GitReferenceModel Head { get; set; }
        public int Number { get; set; }
        public List<PullRequestReviewModel> Reviews { get; set; }
        public string Title { get; set; }
        public object State { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}