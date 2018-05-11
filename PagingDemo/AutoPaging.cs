using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit.GraphQL;
using PagingDemo.Models;

namespace PagingDemo
{
    static class AutoPaging
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
                    Reviews = pr.Reviews(100, null, null, null, null, null).AllPages().Select(review => new PullRequestReviewModel
                    {
                        Id = review.DatabaseId.Value,
                        NodeId = review.Id.Value,
                        Body = review.Body,
                        CommitId = review.Commit.Oid,
                        State = review.State,
                        SubmittedAt = review.SubmittedAt,
                        User = new AuthorModel(review.Author.Login, review.Author.AvatarUrl(null)),
                        Comments = review.Comments(100, null, null, null).AllPages().Select(comment => new PullRequestReviewCommentModel
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
                    }).ToList(),
                }).Compile();

            var connection = new DemoConnection();

            // Fetch all results.
            var result = await connection.Run(master);

            Console.WriteLine();
            Console.WriteLine($"Read {result.Reviews.Count} reviews.");
            Console.WriteLine($"Most comments per review was {result.Reviews.Max(x => x.Comments.Count)}.");
        }
    }
}
