using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using PagingDemo.Models;
using static Octokit.GraphQL.Variable;

namespace PagingDemo
{
    static class ManualPaging
    {
        public static async Task Run()
        {
            var master = new Query()
                .Repository("Microsoft", "MixedRealityToolkit-Unity")
                .PullRequest(1884)
                .Select(pr => new PullRequestModel
                {
                    NodeId = pr.Id.ToString(),
                    Author = new AuthorModel(pr.Author.Login, pr.Author.AvatarUrl(null)),
                    Base = pr.BaseRef != null ?
                        new GitReferenceModel(
                            pr.BaseRef.Name,
                            pr.BaseRef.Repository.Owner.Login + ':' + pr.BaseRef.Name,
                            pr.BaseRef.Target.Oid,
                            pr.BaseRef.Repository.Url) : null,
                    Body = pr.Body,
                    CreatedAt = pr.CreatedAt,
                    Head = pr.HeadRef != null ?
                        new GitReferenceModel(
                            pr.HeadRef.Name,
                            pr.HeadRef.Repository.Owner.Login + ':' + pr.HeadRef.Name,
                            pr.HeadRef.Target.Oid,
                            pr.HeadRef.Repository.Url) : null,
                    Number = pr.Number,
                    Title = pr.Title,
                    State = pr.State,
                    UpdatedAt = pr.UpdatedAt,
                    Reviews = pr.Reviews(100, null, null, null, null, null).Select(reviewConnection => new Page<PullRequestReviewModel>
                    {
                        HasNextPage = reviewConnection.PageInfo.HasNextPage,
                        EndCursor = reviewConnection.PageInfo.EndCursor,
                        Items = reviewConnection.Nodes.Select(review => new PullRequestReviewModel
                        {
                            Id = review.DatabaseId.Value,
                            NodeId = review.Id.Value,
                            Body = review.Body,
                            CommitId = review.Commit.Oid,
                            State = review.State,
                            SubmittedAt = review.SubmittedAt,
                            User = new AuthorModel(review.Author.Login, review.Author.AvatarUrl(null)),
                            Comments = review.Comments(100, null, null, null).Select(commentConnection => new Page<PullRequestReviewCommentModel>
                            {
                                HasNextPage = commentConnection.PageInfo.HasNextPage,
                                EndCursor = commentConnection.PageInfo.EndCursor,
                                Items = commentConnection.Nodes.Select(comment => new PullRequestReviewCommentModel
                                {
                                    Id = comment.DatabaseId.Value,
                                    NodeId = comment.Id.Value,
                                    Body = comment.Body,
                                    CommitId = comment.Commit.Oid,
                                    CreatedAt = comment.CreatedAt,
                                    DiffHunk = comment.DiffHunk,
                                    OriginalCommitId = comment.OriginalCommit.Oid,
                                    OriginalPosition = comment.OriginalPosition,
                                    Path = comment.Path,
                                    Position = comment.Position,
                                    User = new AuthorModel(comment.Author.Login, comment.Author.AvatarUrl(null)),
                                }).ToList(),
                            }).Single(),
                        }).ToList(),
                    }).Single(),
                }).Compile();

            var reviews = new Query()
                .Node(Var("id"))
                .Cast<PullRequest>()
                .Reviews(100, Var("after"))
                .Select(reviewConnection => new Page<PullRequestReviewModel>
                {
                    HasNextPage = reviewConnection.PageInfo.HasNextPage,
                    EndCursor = reviewConnection.PageInfo.EndCursor,
                    Items = reviewConnection.Nodes.Select(review => new PullRequestReviewModel
                    {
                        Id = review.DatabaseId.Value,
                        NodeId = review.Id.Value,
                        Body = review.Body,
                        CommitId = review.Commit.Oid,
                        State = review.State,
                        SubmittedAt = review.SubmittedAt,
                        User = new AuthorModel(review.Author.Login, review.Author.AvatarUrl(null)),
                        Comments = review.Comments(100, null, null, null).Select(commentConnection => new Page<PullRequestReviewCommentModel>
                        {
                            HasNextPage = commentConnection.PageInfo.HasNextPage,
                            EndCursor = commentConnection.PageInfo.EndCursor,
                            Items = commentConnection.Nodes.Select(comment => new PullRequestReviewCommentModel
                            {
                                Id = comment.DatabaseId.Value,
                                NodeId = comment.Id.Value,
                                Body = comment.Body,
                                CommitId = comment.Commit.Oid,
                                CreatedAt = comment.CreatedAt,
                                DiffHunk = comment.DiffHunk,
                                OriginalCommitId = comment.OriginalCommit.Oid,
                                OriginalPosition = comment.OriginalPosition,
                                Path = comment.Path,
                                Position = comment.Position,
                                User = new AuthorModel(comment.Author.Login, comment.Author.AvatarUrl(null)),
                            }).ToList(),
                        }).Single(),
                    }).ToList(),
                }).Compile();

            var comments = new Query()
                .Node(Var("id"))
                .Cast<PullRequestReview>()
                .Comments(100, Var("after"))
                .Select(commentConnection => new Page<PullRequestReviewCommentModel>
                {
                    HasNextPage = commentConnection.PageInfo.HasNextPage,
                    EndCursor = commentConnection.PageInfo.EndCursor,
                    Items = commentConnection.Nodes.Select(comment => new PullRequestReviewCommentModel
                    {
                        Id = comment.DatabaseId.Value,
                        NodeId = comment.Id.Value,
                        Body = comment.Body,
                        CommitId = comment.Commit.Oid,
                        CreatedAt = comment.CreatedAt,
                        DiffHunk = comment.DiffHunk,
                        OriginalCommitId = comment.OriginalCommit.Oid,
                        OriginalPosition = comment.OriginalPosition,
                        Path = comment.Path,
                        Position = comment.Position,
                        User = new AuthorModel(comment.Author.Login, comment.Author.AvatarUrl(null)),
                    }).ToList(),
                }).Compile();

            var connection = new DemoConnection();

            // Fetch the master page.
            var result = await connection.Run(master);
            var reviewsPageInfo = (Page<PullRequestReviewModel>)result.Reviews;

            // Get the page info from the master page.
            var vars = new Dictionary<string, object>
            {
                { "id", result.NodeId },
                { "after", reviewsPageInfo.EndCursor },
            };

            // While there are more reviews to read, page them in.
            var more = reviewsPageInfo.HasNextPage;

            while (more)
            {
                var reviewsPage = await connection.Run(reviews, vars);

                result.Reviews.AddRange(reviewsPage);

                more = reviewsPage.HasNextPage;
                vars["after"] = reviewsPage.EndCursor;
            }

            // Loop through each review.
            foreach (var review in result.Reviews)
            {
                var commentsPageInfo = (Page<PullRequestReviewCommentModel>)review.Comments;

                vars["id"] = review.NodeId;
                vars["after"] = commentsPageInfo.EndCursor;
                more = commentsPageInfo.HasNextPage;

                while (more)
                {
                    var commentsPage = await connection.Run(comments, vars);

                    review.Comments.AddRange(commentsPage);

                    more = commentsPage.HasNextPage;
                    vars["after"] = commentsPage.EndCursor;
                }
            }

            Console.WriteLine();
            Console.WriteLine($"Read {result.Reviews.Count} reviews.");
            Console.WriteLine($"Most comments per review was {result.Reviews.Max(x => x.Comments.Count)}.");
        }
    }
}
