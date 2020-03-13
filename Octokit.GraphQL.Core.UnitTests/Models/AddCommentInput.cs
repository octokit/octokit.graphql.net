namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class AddCommentInput
    {
        public string SubjectId { get; set; }

        public string Body { get; set; }

        public string ClientMutationId { get; set; }
    }
}