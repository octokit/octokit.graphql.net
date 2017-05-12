namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The root query for implementing GraphQL mutations.
    /// </summary>
    public class Mutation : QueryEntity
    {
        public Mutation(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Applies a suggested topic to the repository.
        /// </summary>
        public AcceptTopicSuggestionPayload AcceptTopicSuggestion(AcceptTopicSuggestionInput input) => this.CreateMethodCall(x => x.AcceptTopicSuggestion(input), Octokit.GraphQL.AcceptTopicSuggestionPayload.Create);

        /// <summary>
        /// Adds a comment to an Issue or Pull Request.
        /// </summary>
        public AddCommentPayload AddComment(AddCommentInput input) => this.CreateMethodCall(x => x.AddComment(input), Octokit.GraphQL.AddCommentPayload.Create);

        /// <summary>
        /// Adds a card to a ProjectColumn. Either `contentId` or `note` must be provided but **not** both.
        /// </summary>
        public AddProjectCardPayload AddProjectCard(AddProjectCardInput input) => this.CreateMethodCall(x => x.AddProjectCard(input), Octokit.GraphQL.AddProjectCardPayload.Create);

        /// <summary>
        /// Adds a column to a Project.
        /// </summary>
        public AddProjectColumnPayload AddProjectColumn(AddProjectColumnInput input) => this.CreateMethodCall(x => x.AddProjectColumn(input), Octokit.GraphQL.AddProjectColumnPayload.Create);

        /// <summary>
        /// Adds a review to a Pull Request.
        /// </summary>
        public AddPullRequestReviewPayload AddPullRequestReview(AddPullRequestReviewInput input) => this.CreateMethodCall(x => x.AddPullRequestReview(input), Octokit.GraphQL.AddPullRequestReviewPayload.Create);

        /// <summary>
        /// Adds a comment to a review.
        /// </summary>
        public AddPullRequestReviewCommentPayload AddPullRequestReviewComment(AddPullRequestReviewCommentInput input) => this.CreateMethodCall(x => x.AddPullRequestReviewComment(input), Octokit.GraphQL.AddPullRequestReviewCommentPayload.Create);

        /// <summary>
        /// Adds a reaction to a subject.
        /// </summary>
        public AddReactionPayload AddReaction(AddReactionInput input) => this.CreateMethodCall(x => x.AddReaction(input), Octokit.GraphQL.AddReactionPayload.Create);

        /// <summary>
        /// Creates a new project.
        /// </summary>
        public CreateProjectPayload CreateProject(CreateProjectInput input) => this.CreateMethodCall(x => x.CreateProject(input), Octokit.GraphQL.CreateProjectPayload.Create);

        /// <summary>
        /// Rejects a suggested topic for the repository.
        /// </summary>
        public DeclineTopicSuggestionPayload DeclineTopicSuggestion(DeclineTopicSuggestionInput input) => this.CreateMethodCall(x => x.DeclineTopicSuggestion(input), Octokit.GraphQL.DeclineTopicSuggestionPayload.Create);

        /// <summary>
        /// Deletes a project.
        /// </summary>
        public DeleteProjectPayload DeleteProject(DeleteProjectInput input) => this.CreateMethodCall(x => x.DeleteProject(input), Octokit.GraphQL.DeleteProjectPayload.Create);

        /// <summary>
        /// Deletes a project card.
        /// </summary>
        public DeleteProjectCardPayload DeleteProjectCard(DeleteProjectCardInput input) => this.CreateMethodCall(x => x.DeleteProjectCard(input), Octokit.GraphQL.DeleteProjectCardPayload.Create);

        /// <summary>
        /// Deletes a project column.
        /// </summary>
        public DeleteProjectColumnPayload DeleteProjectColumn(DeleteProjectColumnInput input) => this.CreateMethodCall(x => x.DeleteProjectColumn(input), Octokit.GraphQL.DeleteProjectColumnPayload.Create);

        /// <summary>
        /// Deletes a pull request review.
        /// </summary>
        public DeletePullRequestReviewPayload DeletePullRequestReview(DeletePullRequestReviewInput input) => this.CreateMethodCall(x => x.DeletePullRequestReview(input), Octokit.GraphQL.DeletePullRequestReviewPayload.Create);

        /// <summary>
        /// Dismisses an approved or rejected pull request review.
        /// </summary>
        public DismissPullRequestReviewPayload DismissPullRequestReview(DismissPullRequestReviewInput input) => this.CreateMethodCall(x => x.DismissPullRequestReview(input), Octokit.GraphQL.DismissPullRequestReviewPayload.Create);

        /// <summary>
        /// Moves a project card to another place.
        /// </summary>
        public MoveProjectCardPayload MoveProjectCard(MoveProjectCardInput input) => this.CreateMethodCall(x => x.MoveProjectCard(input), Octokit.GraphQL.MoveProjectCardPayload.Create);

        /// <summary>
        /// Moves a project column to another place.
        /// </summary>
        public MoveProjectColumnPayload MoveProjectColumn(MoveProjectColumnInput input) => this.CreateMethodCall(x => x.MoveProjectColumn(input), Octokit.GraphQL.MoveProjectColumnPayload.Create);

        /// <summary>
        /// Removes outside collaborator from all repositories in an organization.
        /// </summary>
        public RemoveOutsideCollaboratorPayload RemoveOutsideCollaborator(RemoveOutsideCollaboratorInput input) => this.CreateMethodCall(x => x.RemoveOutsideCollaborator(input), Octokit.GraphQL.RemoveOutsideCollaboratorPayload.Create);

        /// <summary>
        /// Removes a reaction from a subject.
        /// </summary>
        public RemoveReactionPayload RemoveReaction(RemoveReactionInput input) => this.CreateMethodCall(x => x.RemoveReaction(input), Octokit.GraphQL.RemoveReactionPayload.Create);

        /// <summary>
        /// Set review requests on a pull request.
        /// </summary>
        public RequestReviewsPayload RequestReviews(RequestReviewsInput input) => this.CreateMethodCall(x => x.RequestReviews(input), Octokit.GraphQL.RequestReviewsPayload.Create);

        /// <summary>
        /// Submits a pending pull request review.
        /// </summary>
        public SubmitPullRequestReviewPayload SubmitPullRequestReview(SubmitPullRequestReviewInput input) => this.CreateMethodCall(x => x.SubmitPullRequestReview(input), Octokit.GraphQL.SubmitPullRequestReviewPayload.Create);

        /// <summary>
        /// Updates an existing project.
        /// </summary>
        public UpdateProjectPayload UpdateProject(UpdateProjectInput input) => this.CreateMethodCall(x => x.UpdateProject(input), Octokit.GraphQL.UpdateProjectPayload.Create);

        /// <summary>
        /// Updates an existing project card.
        /// </summary>
        public UpdateProjectCardPayload UpdateProjectCard(UpdateProjectCardInput input) => this.CreateMethodCall(x => x.UpdateProjectCard(input), Octokit.GraphQL.UpdateProjectCardPayload.Create);

        /// <summary>
        /// Updates an existing project column.
        /// </summary>
        public UpdateProjectColumnPayload UpdateProjectColumn(UpdateProjectColumnInput input) => this.CreateMethodCall(x => x.UpdateProjectColumn(input), Octokit.GraphQL.UpdateProjectColumnPayload.Create);

        /// <summary>
        /// Updates the body of a pull request review.
        /// </summary>
        public UpdatePullRequestReviewPayload UpdatePullRequestReview(UpdatePullRequestReviewInput input) => this.CreateMethodCall(x => x.UpdatePullRequestReview(input), Octokit.GraphQL.UpdatePullRequestReviewPayload.Create);

        /// <summary>
        /// Updates a pull request review comment.
        /// </summary>
        public UpdatePullRequestReviewCommentPayload UpdatePullRequestReviewComment(UpdatePullRequestReviewCommentInput input) => this.CreateMethodCall(x => x.UpdatePullRequestReviewComment(input), Octokit.GraphQL.UpdatePullRequestReviewCommentPayload.Create);

        /// <summary>
        /// Updates viewers repository subscription state.
        /// </summary>
        public UpdateSubscriptionPayload UpdateSubscription(UpdateSubscriptionInput input) => this.CreateMethodCall(x => x.UpdateSubscription(input), Octokit.GraphQL.UpdateSubscriptionPayload.Create);

        /// <summary>
        /// Replaces the repository's topics with the given topics.
        /// </summary>
        public UpdateTopicsPayload UpdateTopics(UpdateTopicsInput input) => this.CreateMethodCall(x => x.UpdateTopics(input), Octokit.GraphQL.UpdateTopicsPayload.Create);

        internal static Mutation Create(IQueryProvider provider, Expression expression)
        {
            return new Mutation(provider, expression);
        }
    }
}