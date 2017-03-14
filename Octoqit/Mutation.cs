namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// The root query for implementing GraphQL mutations.
    /// </summary>
    public class Mutation : QueryEntity
    {
        public Mutation(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Adds a comment to an Issue or Pull Request.
        /// </summary>
        public AddCommentPayload AddComment(AddCommentInput input) => this.CreateMethodCall(x => x.AddComment(input), Octoqit.AddCommentPayload.Create);

        /// <summary>
        /// Adds a card to a ProjectColumn. Either `contentId` or `note` must be provided but **not** both.
        /// </summary>
        public AddProjectCardPayload AddProjectCard(AddProjectCardInput input) => this.CreateMethodCall(x => x.AddProjectCard(input), Octoqit.AddProjectCardPayload.Create);

        /// <summary>
        /// Adds a column to a Project.
        /// </summary>
        public AddProjectColumnPayload AddProjectColumn(AddProjectColumnInput input) => this.CreateMethodCall(x => x.AddProjectColumn(input), Octoqit.AddProjectColumnPayload.Create);

        /// <summary>
        /// Adds a review to a Pull Request.
        /// </summary>
        public AddPullRequestReviewPayload AddPullRequestReview(AddPullRequestReviewInput input) => this.CreateMethodCall(x => x.AddPullRequestReview(input), Octoqit.AddPullRequestReviewPayload.Create);

        /// <summary>
        /// Adds a comment to a review.
        /// </summary>
        public AddPullRequestReviewCommentPayload AddPullRequestReviewComment(AddPullRequestReviewCommentInput input) => this.CreateMethodCall(x => x.AddPullRequestReviewComment(input), Octoqit.AddPullRequestReviewCommentPayload.Create);

        /// <summary>
        /// Adds a reaction to a subject.
        /// </summary>
        public AddReactionPayload AddReaction(AddReactionInput input) => this.CreateMethodCall(x => x.AddReaction(input), Octoqit.AddReactionPayload.Create);

        /// <summary>
        /// Creates a new project.
        /// </summary>
        public CreateProjectPayload CreateProject(CreateProjectInput input) => this.CreateMethodCall(x => x.CreateProject(input), Octoqit.CreateProjectPayload.Create);

        /// <summary>
        /// Deletes a project.
        /// </summary>
        public DeleteProjectPayload DeleteProject(DeleteProjectInput input) => this.CreateMethodCall(x => x.DeleteProject(input), Octoqit.DeleteProjectPayload.Create);

        /// <summary>
        /// Deletes a project card.
        /// </summary>
        public DeleteProjectCardPayload DeleteProjectCard(DeleteProjectCardInput input) => this.CreateMethodCall(x => x.DeleteProjectCard(input), Octoqit.DeleteProjectCardPayload.Create);

        /// <summary>
        /// Deletes a project column.
        /// </summary>
        public DeleteProjectColumnPayload DeleteProjectColumn(DeleteProjectColumnInput input) => this.CreateMethodCall(x => x.DeleteProjectColumn(input), Octoqit.DeleteProjectColumnPayload.Create);

        /// <summary>
        /// Deletes a pull request review.
        /// </summary>
        public DeletePullRequestReviewPayload DeletePullRequestReview(DeletePullRequestReviewInput input) => this.CreateMethodCall(x => x.DeletePullRequestReview(input), Octoqit.DeletePullRequestReviewPayload.Create);

        /// <summary>
        /// Dismisses an approved or rejected pull request review.
        /// </summary>
        public DismissPullRequestReviewPayload DismissPullRequestReview(DismissPullRequestReviewInput input) => this.CreateMethodCall(x => x.DismissPullRequestReview(input), Octoqit.DismissPullRequestReviewPayload.Create);

        /// <summary>
        /// Moves a project card to another place.
        /// </summary>
        public MoveProjectCardPayload MoveProjectCard(MoveProjectCardInput input) => this.CreateMethodCall(x => x.MoveProjectCard(input), Octoqit.MoveProjectCardPayload.Create);

        /// <summary>
        /// Moves a project column to another place.
        /// </summary>
        public MoveProjectColumnPayload MoveProjectColumn(MoveProjectColumnInput input) => this.CreateMethodCall(x => x.MoveProjectColumn(input), Octoqit.MoveProjectColumnPayload.Create);

        /// <summary>
        /// Removes outside collaborator from all repositories in an organization.
        /// </summary>
        public RemoveOutsideCollaboratorPayload RemoveOutsideCollaborator(RemoveOutsideCollaboratorInput input) => this.CreateMethodCall(x => x.RemoveOutsideCollaborator(input), Octoqit.RemoveOutsideCollaboratorPayload.Create);

        /// <summary>
        /// Removes a reaction from a subject.
        /// </summary>
        public RemoveReactionPayload RemoveReaction(RemoveReactionInput input) => this.CreateMethodCall(x => x.RemoveReaction(input), Octoqit.RemoveReactionPayload.Create);

        /// <summary>
        /// Set review requests on a pull request.
        /// </summary>
        public RequestReviewsPayload RequestReviews(RequestReviewsInput input) => this.CreateMethodCall(x => x.RequestReviews(input), Octoqit.RequestReviewsPayload.Create);

        /// <summary>
        /// Submits a pending pull request review.
        /// </summary>
        public SubmitPullRequestReviewPayload SubmitPullRequestReview(SubmitPullRequestReviewInput input) => this.CreateMethodCall(x => x.SubmitPullRequestReview(input), Octoqit.SubmitPullRequestReviewPayload.Create);

        /// <summary>
        /// Updates an existing project.
        /// </summary>
        public UpdateProjectPayload UpdateProject(UpdateProjectInput input) => this.CreateMethodCall(x => x.UpdateProject(input), Octoqit.UpdateProjectPayload.Create);

        /// <summary>
        /// Updates an existing project card.
        /// </summary>
        public UpdateProjectCardPayload UpdateProjectCard(UpdateProjectCardInput input) => this.CreateMethodCall(x => x.UpdateProjectCard(input), Octoqit.UpdateProjectCardPayload.Create);

        /// <summary>
        /// Updates an existing project column.
        /// </summary>
        public UpdateProjectColumnPayload UpdateProjectColumn(UpdateProjectColumnInput input) => this.CreateMethodCall(x => x.UpdateProjectColumn(input), Octoqit.UpdateProjectColumnPayload.Create);

        /// <summary>
        /// Updates the body of a pull request review.
        /// </summary>
        public UpdatePullRequestReviewPayload UpdatePullRequestReview(UpdatePullRequestReviewInput input) => this.CreateMethodCall(x => x.UpdatePullRequestReview(input), Octoqit.UpdatePullRequestReviewPayload.Create);

        /// <summary>
        /// Updates a pull request review comment.
        /// </summary>
        public UpdatePullRequestReviewCommentPayload UpdatePullRequestReviewComment(UpdatePullRequestReviewCommentInput input) => this.CreateMethodCall(x => x.UpdatePullRequestReviewComment(input), Octoqit.UpdatePullRequestReviewCommentPayload.Create);

        /// <summary>
        /// Updates viewers repository subscription state.
        /// </summary>
        public UpdateSubscriptionPayload UpdateSubscription(UpdateSubscriptionInput input) => this.CreateMethodCall(x => x.UpdateSubscription(input), Octoqit.UpdateSubscriptionPayload.Create);

        internal static Mutation Create(IQueryProvider provider, Expression expression)
        {
            return new Mutation(provider, expression);
        }
    }
}