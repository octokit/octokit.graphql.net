namespace Octokit.GraphQL
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The root query for implementing GraphQL mutations.
    /// </summary>
    public class Mutation : QueryableValue<Mutation>, IMutation
    {
        public Mutation() : base(null)
        {
        }

        public Mutation(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Applies a suggested topic to the repository.
        /// </summary>
        public AcceptTopicSuggestionPayload AcceptTopicSuggestion(Arg<AcceptTopicSuggestionInput> input) => this.CreateMethodCall(x => x.AcceptTopicSuggestion(input), Octokit.GraphQL.Model.AcceptTopicSuggestionPayload.Create);

        /// <summary>
        /// Adds a comment to an Issue or Pull Request.
        /// </summary>
        public AddCommentPayload AddComment(Arg<AddCommentInput> input) => this.CreateMethodCall(x => x.AddComment(input), Octokit.GraphQL.Model.AddCommentPayload.Create);

        /// <summary>
        /// Adds a card to a ProjectColumn. Either `contentId` or `note` must be provided but **not** both.
        /// </summary>
        public AddProjectCardPayload AddProjectCard(Arg<AddProjectCardInput> input) => this.CreateMethodCall(x => x.AddProjectCard(input), Octokit.GraphQL.Model.AddProjectCardPayload.Create);

        /// <summary>
        /// Adds a column to a Project.
        /// </summary>
        public AddProjectColumnPayload AddProjectColumn(Arg<AddProjectColumnInput> input) => this.CreateMethodCall(x => x.AddProjectColumn(input), Octokit.GraphQL.Model.AddProjectColumnPayload.Create);

        /// <summary>
        /// Adds a review to a Pull Request.
        /// </summary>
        public AddPullRequestReviewPayload AddPullRequestReview(Arg<AddPullRequestReviewInput> input) => this.CreateMethodCall(x => x.AddPullRequestReview(input), Octokit.GraphQL.Model.AddPullRequestReviewPayload.Create);

        /// <summary>
        /// Adds a comment to a review.
        /// </summary>
        public AddPullRequestReviewCommentPayload AddPullRequestReviewComment(Arg<AddPullRequestReviewCommentInput> input) => this.CreateMethodCall(x => x.AddPullRequestReviewComment(input), Octokit.GraphQL.Model.AddPullRequestReviewCommentPayload.Create);

        /// <summary>
        /// Adds a reaction to a subject.
        /// </summary>
        public AddReactionPayload AddReaction(Arg<AddReactionInput> input) => this.CreateMethodCall(x => x.AddReaction(input), Octokit.GraphQL.Model.AddReactionPayload.Create);

        /// <summary>
        /// Adds a star to a Starrable.
        /// </summary>
        public AddStarPayload AddStar(Arg<AddStarInput> input) => this.CreateMethodCall(x => x.AddStar(input), Octokit.GraphQL.Model.AddStarPayload.Create);

        /// <summary>
        /// Creates a new project.
        /// </summary>
        public CreateProjectPayload CreateProject(Arg<CreateProjectInput> input) => this.CreateMethodCall(x => x.CreateProject(input), Octokit.GraphQL.Model.CreateProjectPayload.Create);

        /// <summary>
        /// Rejects a suggested topic for the repository.
        /// </summary>
        public DeclineTopicSuggestionPayload DeclineTopicSuggestion(Arg<DeclineTopicSuggestionInput> input) => this.CreateMethodCall(x => x.DeclineTopicSuggestion(input), Octokit.GraphQL.Model.DeclineTopicSuggestionPayload.Create);

        /// <summary>
        /// Deletes a project.
        /// </summary>
        public DeleteProjectPayload DeleteProject(Arg<DeleteProjectInput> input) => this.CreateMethodCall(x => x.DeleteProject(input), Octokit.GraphQL.Model.DeleteProjectPayload.Create);

        /// <summary>
        /// Deletes a project card.
        /// </summary>
        public DeleteProjectCardPayload DeleteProjectCard(Arg<DeleteProjectCardInput> input) => this.CreateMethodCall(x => x.DeleteProjectCard(input), Octokit.GraphQL.Model.DeleteProjectCardPayload.Create);

        /// <summary>
        /// Deletes a project column.
        /// </summary>
        public DeleteProjectColumnPayload DeleteProjectColumn(Arg<DeleteProjectColumnInput> input) => this.CreateMethodCall(x => x.DeleteProjectColumn(input), Octokit.GraphQL.Model.DeleteProjectColumnPayload.Create);

        /// <summary>
        /// Deletes a pull request review.
        /// </summary>
        public DeletePullRequestReviewPayload DeletePullRequestReview(Arg<DeletePullRequestReviewInput> input) => this.CreateMethodCall(x => x.DeletePullRequestReview(input), Octokit.GraphQL.Model.DeletePullRequestReviewPayload.Create);

        /// <summary>
        /// Dismisses an approved or rejected pull request review.
        /// </summary>
        public DismissPullRequestReviewPayload DismissPullRequestReview(Arg<DismissPullRequestReviewInput> input) => this.CreateMethodCall(x => x.DismissPullRequestReview(input), Octokit.GraphQL.Model.DismissPullRequestReviewPayload.Create);

        /// <summary>
        /// Lock a lockable object
        /// </summary>
        public LockLockablePayload LockLockable(Arg<LockLockableInput> input) => this.CreateMethodCall(x => x.LockLockable(input), Octokit.GraphQL.Model.LockLockablePayload.Create);

        /// <summary>
        /// Moves a project card to another place.
        /// </summary>
        public MoveProjectCardPayload MoveProjectCard(Arg<MoveProjectCardInput> input) => this.CreateMethodCall(x => x.MoveProjectCard(input), Octokit.GraphQL.Model.MoveProjectCardPayload.Create);

        /// <summary>
        /// Moves a project column to another place.
        /// </summary>
        public MoveProjectColumnPayload MoveProjectColumn(Arg<MoveProjectColumnInput> input) => this.CreateMethodCall(x => x.MoveProjectColumn(input), Octokit.GraphQL.Model.MoveProjectColumnPayload.Create);

        /// <summary>
        /// Removes outside collaborator from all repositories in an organization.
        /// </summary>
        public RemoveOutsideCollaboratorPayload RemoveOutsideCollaborator(Arg<RemoveOutsideCollaboratorInput> input) => this.CreateMethodCall(x => x.RemoveOutsideCollaborator(input), Octokit.GraphQL.Model.RemoveOutsideCollaboratorPayload.Create);

        /// <summary>
        /// Removes a reaction from a subject.
        /// </summary>
        public RemoveReactionPayload RemoveReaction(Arg<RemoveReactionInput> input) => this.CreateMethodCall(x => x.RemoveReaction(input), Octokit.GraphQL.Model.RemoveReactionPayload.Create);

        /// <summary>
        /// Removes a star from a Starrable.
        /// </summary>
        public RemoveStarPayload RemoveStar(Arg<RemoveStarInput> input) => this.CreateMethodCall(x => x.RemoveStar(input), Octokit.GraphQL.Model.RemoveStarPayload.Create);

        /// <summary>
        /// Set review requests on a pull request.
        /// </summary>
        public RequestReviewsPayload RequestReviews(Arg<RequestReviewsInput> input) => this.CreateMethodCall(x => x.RequestReviews(input), Octokit.GraphQL.Model.RequestReviewsPayload.Create);

        /// <summary>
        /// Submits a pending pull request review.
        /// </summary>
        public SubmitPullRequestReviewPayload SubmitPullRequestReview(Arg<SubmitPullRequestReviewInput> input) => this.CreateMethodCall(x => x.SubmitPullRequestReview(input), Octokit.GraphQL.Model.SubmitPullRequestReviewPayload.Create);

        /// <summary>
        /// Updates an existing project.
        /// </summary>
        public UpdateProjectPayload UpdateProject(Arg<UpdateProjectInput> input) => this.CreateMethodCall(x => x.UpdateProject(input), Octokit.GraphQL.Model.UpdateProjectPayload.Create);

        /// <summary>
        /// Updates an existing project card.
        /// </summary>
        public UpdateProjectCardPayload UpdateProjectCard(Arg<UpdateProjectCardInput> input) => this.CreateMethodCall(x => x.UpdateProjectCard(input), Octokit.GraphQL.Model.UpdateProjectCardPayload.Create);

        /// <summary>
        /// Updates an existing project column.
        /// </summary>
        public UpdateProjectColumnPayload UpdateProjectColumn(Arg<UpdateProjectColumnInput> input) => this.CreateMethodCall(x => x.UpdateProjectColumn(input), Octokit.GraphQL.Model.UpdateProjectColumnPayload.Create);

        /// <summary>
        /// Updates the body of a pull request review.
        /// </summary>
        public UpdatePullRequestReviewPayload UpdatePullRequestReview(Arg<UpdatePullRequestReviewInput> input) => this.CreateMethodCall(x => x.UpdatePullRequestReview(input), Octokit.GraphQL.Model.UpdatePullRequestReviewPayload.Create);

        /// <summary>
        /// Updates a pull request review comment.
        /// </summary>
        public UpdatePullRequestReviewCommentPayload UpdatePullRequestReviewComment(Arg<UpdatePullRequestReviewCommentInput> input) => this.CreateMethodCall(x => x.UpdatePullRequestReviewComment(input), Octokit.GraphQL.Model.UpdatePullRequestReviewCommentPayload.Create);

        /// <summary>
        /// Updates viewers repository subscription state.
        /// </summary>
        public UpdateSubscriptionPayload UpdateSubscription(Arg<UpdateSubscriptionInput> input) => this.CreateMethodCall(x => x.UpdateSubscription(input), Octokit.GraphQL.Model.UpdateSubscriptionPayload.Create);

        /// <summary>
        /// Replaces the repository's topics with the given topics.
        /// </summary>
        public UpdateTopicsPayload UpdateTopics(Arg<UpdateTopicsInput> input) => this.CreateMethodCall(x => x.UpdateTopics(input), Octokit.GraphQL.Model.UpdateTopicsPayload.Create);

        internal static Mutation Create(Expression expression)
        {
            return new Mutation(expression);
        }
    }
}