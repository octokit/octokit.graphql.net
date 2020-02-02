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

        internal Mutation(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Accepts a pending invitation for a user to become an administrator of an enterprise.
        /// </summary>
        public AcceptEnterpriseAdministratorInvitationPayload AcceptEnterpriseAdministratorInvitation(Arg<AcceptEnterpriseAdministratorInvitationInput> input) => this.CreateMethodCall(x => x.AcceptEnterpriseAdministratorInvitation(input), Octokit.GraphQL.Model.AcceptEnterpriseAdministratorInvitationPayload.Create);

        /// <summary>
        /// Applies a suggested topic to the repository.
        /// </summary>
        public AcceptTopicSuggestionPayload AcceptTopicSuggestion(Arg<AcceptTopicSuggestionInput> input) => this.CreateMethodCall(x => x.AcceptTopicSuggestion(input), Octokit.GraphQL.Model.AcceptTopicSuggestionPayload.Create);

        /// <summary>
        /// Adds assignees to an assignable object.
        /// </summary>
        public AddAssigneesToAssignablePayload AddAssigneesToAssignable(Arg<AddAssigneesToAssignableInput> input) => this.CreateMethodCall(x => x.AddAssigneesToAssignable(input), Octokit.GraphQL.Model.AddAssigneesToAssignablePayload.Create);

        /// <summary>
        /// Adds a comment to an Issue or Pull Request.
        /// </summary>
        public AddCommentPayload AddComment(Arg<AddCommentInput> input) => this.CreateMethodCall(x => x.AddComment(input), Octokit.GraphQL.Model.AddCommentPayload.Create);

        /// <summary>
        /// Adds labels to a labelable object.
        /// </summary>
        public AddLabelsToLabelablePayload AddLabelsToLabelable(Arg<AddLabelsToLabelableInput> input) => this.CreateMethodCall(x => x.AddLabelsToLabelable(input), Octokit.GraphQL.Model.AddLabelsToLabelablePayload.Create);

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
        /// Marks a repository as archived.
        /// </summary>
        public ArchiveRepositoryPayload ArchiveRepository(Arg<ArchiveRepositoryInput> input) => this.CreateMethodCall(x => x.ArchiveRepository(input), Octokit.GraphQL.Model.ArchiveRepositoryPayload.Create);

        /// <summary>
        /// Cancels a pending invitation for an administrator to join an enterprise.
        /// </summary>
        public CancelEnterpriseAdminInvitationPayload CancelEnterpriseAdminInvitation(Arg<CancelEnterpriseAdminInvitationInput> input) => this.CreateMethodCall(x => x.CancelEnterpriseAdminInvitation(input), Octokit.GraphQL.Model.CancelEnterpriseAdminInvitationPayload.Create);

        /// <summary>
        /// Update your status on GitHub.
        /// </summary>
        public ChangeUserStatusPayload ChangeUserStatus(Arg<ChangeUserStatusInput> input) => this.CreateMethodCall(x => x.ChangeUserStatus(input), Octokit.GraphQL.Model.ChangeUserStatusPayload.Create);

        /// <summary>
        /// Clears all labels from a labelable object.
        /// </summary>
        public ClearLabelsFromLabelablePayload ClearLabelsFromLabelable(Arg<ClearLabelsFromLabelableInput> input) => this.CreateMethodCall(x => x.ClearLabelsFromLabelable(input), Octokit.GraphQL.Model.ClearLabelsFromLabelablePayload.Create);

        /// <summary>
        /// Creates a new project by cloning configuration from an existing project.
        /// </summary>
        public CloneProjectPayload CloneProject(Arg<CloneProjectInput> input) => this.CreateMethodCall(x => x.CloneProject(input), Octokit.GraphQL.Model.CloneProjectPayload.Create);

        /// <summary>
        /// Create a new repository with the same files and directory structure as a template repository.
        /// </summary>
        public CloneTemplateRepositoryPayload CloneTemplateRepository(Arg<CloneTemplateRepositoryInput> input) => this.CreateMethodCall(x => x.CloneTemplateRepository(input), Octokit.GraphQL.Model.CloneTemplateRepositoryPayload.Create);

        /// <summary>
        /// Close an issue.
        /// </summary>
        public CloseIssuePayload CloseIssue(Arg<CloseIssueInput> input) => this.CreateMethodCall(x => x.CloseIssue(input), Octokit.GraphQL.Model.CloseIssuePayload.Create);

        /// <summary>
        /// Close a pull request.
        /// </summary>
        public ClosePullRequestPayload ClosePullRequest(Arg<ClosePullRequestInput> input) => this.CreateMethodCall(x => x.ClosePullRequest(input), Octokit.GraphQL.Model.ClosePullRequestPayload.Create);

        /// <summary>
        /// Convert a project note card to one associated with a newly created issue.
        /// </summary>
        public ConvertProjectCardNoteToIssuePayload ConvertProjectCardNoteToIssue(Arg<ConvertProjectCardNoteToIssueInput> input) => this.CreateMethodCall(x => x.ConvertProjectCardNoteToIssue(input), Octokit.GraphQL.Model.ConvertProjectCardNoteToIssuePayload.Create);

        /// <summary>
        /// Create a new branch protection rule
        /// </summary>
        public CreateBranchProtectionRulePayload CreateBranchProtectionRule(Arg<CreateBranchProtectionRuleInput> input) => this.CreateMethodCall(x => x.CreateBranchProtectionRule(input), Octokit.GraphQL.Model.CreateBranchProtectionRulePayload.Create);

        /// <summary>
        /// Create a check run.
        /// </summary>
        public CreateCheckRunPayload CreateCheckRun(Arg<CreateCheckRunInput> input) => this.CreateMethodCall(x => x.CreateCheckRun(input), Octokit.GraphQL.Model.CreateCheckRunPayload.Create);

        /// <summary>
        /// Create a check suite
        /// </summary>
        public CreateCheckSuitePayload CreateCheckSuite(Arg<CreateCheckSuiteInput> input) => this.CreateMethodCall(x => x.CreateCheckSuite(input), Octokit.GraphQL.Model.CreateCheckSuitePayload.Create);

        /// <summary>
        /// Creates an organization as part of an enterprise account.
        /// </summary>
        public CreateEnterpriseOrganizationPayload CreateEnterpriseOrganization(Arg<CreateEnterpriseOrganizationInput> input) => this.CreateMethodCall(x => x.CreateEnterpriseOrganization(input), Octokit.GraphQL.Model.CreateEnterpriseOrganizationPayload.Create);

        /// <summary>
        /// Creates a new issue.
        /// </summary>
        public CreateIssuePayload CreateIssue(Arg<CreateIssueInput> input) => this.CreateMethodCall(x => x.CreateIssue(input), Octokit.GraphQL.Model.CreateIssuePayload.Create);

        /// <summary>
        /// Creates a new project.
        /// </summary>
        public CreateProjectPayload CreateProject(Arg<CreateProjectInput> input) => this.CreateMethodCall(x => x.CreateProject(input), Octokit.GraphQL.Model.CreateProjectPayload.Create);

        /// <summary>
        /// Create a new pull request
        /// </summary>
        public CreatePullRequestPayload CreatePullRequest(Arg<CreatePullRequestInput> input) => this.CreateMethodCall(x => x.CreatePullRequest(input), Octokit.GraphQL.Model.CreatePullRequestPayload.Create);

        /// <summary>
        /// Create a new Git Ref.
        /// </summary>
        public CreateRefPayload CreateRef(Arg<CreateRefInput> input) => this.CreateMethodCall(x => x.CreateRef(input), Octokit.GraphQL.Model.CreateRefPayload.Create);

        /// <summary>
        /// Create a new repository.
        /// </summary>
        public CreateRepositoryPayload CreateRepository(Arg<CreateRepositoryInput> input) => this.CreateMethodCall(x => x.CreateRepository(input), Octokit.GraphQL.Model.CreateRepositoryPayload.Create);

        /// <summary>
        /// Creates a new team discussion.
        /// </summary>
        public CreateTeamDiscussionPayload CreateTeamDiscussion(Arg<CreateTeamDiscussionInput> input) => this.CreateMethodCall(x => x.CreateTeamDiscussion(input), Octokit.GraphQL.Model.CreateTeamDiscussionPayload.Create);

        /// <summary>
        /// Creates a new team discussion comment.
        /// </summary>
        public CreateTeamDiscussionCommentPayload CreateTeamDiscussionComment(Arg<CreateTeamDiscussionCommentInput> input) => this.CreateMethodCall(x => x.CreateTeamDiscussionComment(input), Octokit.GraphQL.Model.CreateTeamDiscussionCommentPayload.Create);

        /// <summary>
        /// Rejects a suggested topic for the repository.
        /// </summary>
        public DeclineTopicSuggestionPayload DeclineTopicSuggestion(Arg<DeclineTopicSuggestionInput> input) => this.CreateMethodCall(x => x.DeclineTopicSuggestion(input), Octokit.GraphQL.Model.DeclineTopicSuggestionPayload.Create);

        /// <summary>
        /// Delete a branch protection rule
        /// </summary>
        public DeleteBranchProtectionRulePayload DeleteBranchProtectionRule(Arg<DeleteBranchProtectionRuleInput> input) => this.CreateMethodCall(x => x.DeleteBranchProtectionRule(input), Octokit.GraphQL.Model.DeleteBranchProtectionRulePayload.Create);

        /// <summary>
        /// Deletes an Issue object.
        /// </summary>
        public DeleteIssuePayload DeleteIssue(Arg<DeleteIssueInput> input) => this.CreateMethodCall(x => x.DeleteIssue(input), Octokit.GraphQL.Model.DeleteIssuePayload.Create);

        /// <summary>
        /// Deletes an IssueComment object.
        /// </summary>
        public DeleteIssueCommentPayload DeleteIssueComment(Arg<DeleteIssueCommentInput> input) => this.CreateMethodCall(x => x.DeleteIssueComment(input), Octokit.GraphQL.Model.DeleteIssueCommentPayload.Create);

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
        /// Deletes a pull request review comment.
        /// </summary>
        public DeletePullRequestReviewCommentPayload DeletePullRequestReviewComment(Arg<DeletePullRequestReviewCommentInput> input) => this.CreateMethodCall(x => x.DeletePullRequestReviewComment(input), Octokit.GraphQL.Model.DeletePullRequestReviewCommentPayload.Create);

        /// <summary>
        /// Delete a Git Ref.
        /// </summary>
        public DeleteRefPayload DeleteRef(Arg<DeleteRefInput> input) => this.CreateMethodCall(x => x.DeleteRef(input), Octokit.GraphQL.Model.DeleteRefPayload.Create);

        /// <summary>
        /// Deletes a team discussion.
        /// </summary>
        public DeleteTeamDiscussionPayload DeleteTeamDiscussion(Arg<DeleteTeamDiscussionInput> input) => this.CreateMethodCall(x => x.DeleteTeamDiscussion(input), Octokit.GraphQL.Model.DeleteTeamDiscussionPayload.Create);

        /// <summary>
        /// Deletes a team discussion comment.
        /// </summary>
        public DeleteTeamDiscussionCommentPayload DeleteTeamDiscussionComment(Arg<DeleteTeamDiscussionCommentInput> input) => this.CreateMethodCall(x => x.DeleteTeamDiscussionComment(input), Octokit.GraphQL.Model.DeleteTeamDiscussionCommentPayload.Create);

        /// <summary>
        /// Dismisses an approved or rejected pull request review.
        /// </summary>
        public DismissPullRequestReviewPayload DismissPullRequestReview(Arg<DismissPullRequestReviewInput> input) => this.CreateMethodCall(x => x.DismissPullRequestReview(input), Octokit.GraphQL.Model.DismissPullRequestReviewPayload.Create);

        /// <summary>
        /// Follow a user.
        /// </summary>
        public FollowUserPayload FollowUser(Arg<FollowUserInput> input) => this.CreateMethodCall(x => x.FollowUser(input), Octokit.GraphQL.Model.FollowUserPayload.Create);

        /// <summary>
        /// Invite someone to become an administrator of the enterprise.
        /// </summary>
        public InviteEnterpriseAdminPayload InviteEnterpriseAdmin(Arg<InviteEnterpriseAdminInput> input) => this.CreateMethodCall(x => x.InviteEnterpriseAdmin(input), Octokit.GraphQL.Model.InviteEnterpriseAdminPayload.Create);

        /// <summary>
        /// Creates a repository link for a project.
        /// </summary>
        public LinkRepositoryToProjectPayload LinkRepositoryToProject(Arg<LinkRepositoryToProjectInput> input) => this.CreateMethodCall(x => x.LinkRepositoryToProject(input), Octokit.GraphQL.Model.LinkRepositoryToProjectPayload.Create);

        /// <summary>
        /// Lock a lockable object
        /// </summary>
        public LockLockablePayload LockLockable(Arg<LockLockableInput> input) => this.CreateMethodCall(x => x.LockLockable(input), Octokit.GraphQL.Model.LockLockablePayload.Create);

        /// <summary>
        /// Merge a head into a branch.
        /// </summary>
        public MergeBranchPayload MergeBranch(Arg<MergeBranchInput> input) => this.CreateMethodCall(x => x.MergeBranch(input), Octokit.GraphQL.Model.MergeBranchPayload.Create);

        /// <summary>
        /// Merge a pull request.
        /// </summary>
        public MergePullRequestPayload MergePullRequest(Arg<MergePullRequestInput> input) => this.CreateMethodCall(x => x.MergePullRequest(input), Octokit.GraphQL.Model.MergePullRequestPayload.Create);

        /// <summary>
        /// Moves a project card to another place.
        /// </summary>
        public MoveProjectCardPayload MoveProjectCard(Arg<MoveProjectCardInput> input) => this.CreateMethodCall(x => x.MoveProjectCard(input), Octokit.GraphQL.Model.MoveProjectCardPayload.Create);

        /// <summary>
        /// Moves a project column to another place.
        /// </summary>
        public MoveProjectColumnPayload MoveProjectColumn(Arg<MoveProjectColumnInput> input) => this.CreateMethodCall(x => x.MoveProjectColumn(input), Octokit.GraphQL.Model.MoveProjectColumnPayload.Create);

        /// <summary>
        /// Regenerates the identity provider recovery codes for an enterprise
        /// </summary>
        public RegenerateEnterpriseIdentityProviderRecoveryCodesPayload RegenerateEnterpriseIdentityProviderRecoveryCodes(Arg<RegenerateEnterpriseIdentityProviderRecoveryCodesInput> input) => this.CreateMethodCall(x => x.RegenerateEnterpriseIdentityProviderRecoveryCodes(input), Octokit.GraphQL.Model.RegenerateEnterpriseIdentityProviderRecoveryCodesPayload.Create);

        /// <summary>
        /// Removes assignees from an assignable object.
        /// </summary>
        public RemoveAssigneesFromAssignablePayload RemoveAssigneesFromAssignable(Arg<RemoveAssigneesFromAssignableInput> input) => this.CreateMethodCall(x => x.RemoveAssigneesFromAssignable(input), Octokit.GraphQL.Model.RemoveAssigneesFromAssignablePayload.Create);

        /// <summary>
        /// Removes an administrator from the enterprise.
        /// </summary>
        public RemoveEnterpriseAdminPayload RemoveEnterpriseAdmin(Arg<RemoveEnterpriseAdminInput> input) => this.CreateMethodCall(x => x.RemoveEnterpriseAdmin(input), Octokit.GraphQL.Model.RemoveEnterpriseAdminPayload.Create);

        /// <summary>
        /// Removes an organization from the enterprise
        /// </summary>
        public RemoveEnterpriseOrganizationPayload RemoveEnterpriseOrganization(Arg<RemoveEnterpriseOrganizationInput> input) => this.CreateMethodCall(x => x.RemoveEnterpriseOrganization(input), Octokit.GraphQL.Model.RemoveEnterpriseOrganizationPayload.Create);

        /// <summary>
        /// Removes labels from a Labelable object.
        /// </summary>
        public RemoveLabelsFromLabelablePayload RemoveLabelsFromLabelable(Arg<RemoveLabelsFromLabelableInput> input) => this.CreateMethodCall(x => x.RemoveLabelsFromLabelable(input), Octokit.GraphQL.Model.RemoveLabelsFromLabelablePayload.Create);

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
        /// Reopen a issue.
        /// </summary>
        public ReopenIssuePayload ReopenIssue(Arg<ReopenIssueInput> input) => this.CreateMethodCall(x => x.ReopenIssue(input), Octokit.GraphQL.Model.ReopenIssuePayload.Create);

        /// <summary>
        /// Reopen a pull request.
        /// </summary>
        public ReopenPullRequestPayload ReopenPullRequest(Arg<ReopenPullRequestInput> input) => this.CreateMethodCall(x => x.ReopenPullRequest(input), Octokit.GraphQL.Model.ReopenPullRequestPayload.Create);

        /// <summary>
        /// Set review requests on a pull request.
        /// </summary>
        public RequestReviewsPayload RequestReviews(Arg<RequestReviewsInput> input) => this.CreateMethodCall(x => x.RequestReviews(input), Octokit.GraphQL.Model.RequestReviewsPayload.Create);

        /// <summary>
        /// Rerequests an existing check suite.
        /// </summary>
        public RerequestCheckSuitePayload RerequestCheckSuite(Arg<RerequestCheckSuiteInput> input) => this.CreateMethodCall(x => x.RerequestCheckSuite(input), Octokit.GraphQL.Model.RerequestCheckSuitePayload.Create);

        /// <summary>
        /// Marks a review thread as resolved.
        /// </summary>
        public ResolveReviewThreadPayload ResolveReviewThread(Arg<ResolveReviewThreadInput> input) => this.CreateMethodCall(x => x.ResolveReviewThread(input), Octokit.GraphQL.Model.ResolveReviewThreadPayload.Create);

        /// <summary>
        /// Submits a pending pull request review.
        /// </summary>
        public SubmitPullRequestReviewPayload SubmitPullRequestReview(Arg<SubmitPullRequestReviewInput> input) => this.CreateMethodCall(x => x.SubmitPullRequestReview(input), Octokit.GraphQL.Model.SubmitPullRequestReviewPayload.Create);

        /// <summary>
        /// Transfer an issue to a different repository
        /// </summary>
        public TransferIssuePayload TransferIssue(Arg<TransferIssueInput> input) => this.CreateMethodCall(x => x.TransferIssue(input), Octokit.GraphQL.Model.TransferIssuePayload.Create);

        /// <summary>
        /// Unarchives a repository.
        /// </summary>
        public UnarchiveRepositoryPayload UnarchiveRepository(Arg<UnarchiveRepositoryInput> input) => this.CreateMethodCall(x => x.UnarchiveRepository(input), Octokit.GraphQL.Model.UnarchiveRepositoryPayload.Create);

        /// <summary>
        /// Unfollow a user.
        /// </summary>
        public UnfollowUserPayload UnfollowUser(Arg<UnfollowUserInput> input) => this.CreateMethodCall(x => x.UnfollowUser(input), Octokit.GraphQL.Model.UnfollowUserPayload.Create);

        /// <summary>
        /// Deletes a repository link from a project.
        /// </summary>
        public UnlinkRepositoryFromProjectPayload UnlinkRepositoryFromProject(Arg<UnlinkRepositoryFromProjectInput> input) => this.CreateMethodCall(x => x.UnlinkRepositoryFromProject(input), Octokit.GraphQL.Model.UnlinkRepositoryFromProjectPayload.Create);

        /// <summary>
        /// Unlock a lockable object
        /// </summary>
        public UnlockLockablePayload UnlockLockable(Arg<UnlockLockableInput> input) => this.CreateMethodCall(x => x.UnlockLockable(input), Octokit.GraphQL.Model.UnlockLockablePayload.Create);

        /// <summary>
        /// Unmark an issue as a duplicate of another issue.
        /// </summary>
        public UnmarkIssueAsDuplicatePayload UnmarkIssueAsDuplicate(Arg<UnmarkIssueAsDuplicateInput> input) => this.CreateMethodCall(x => x.UnmarkIssueAsDuplicate(input), Octokit.GraphQL.Model.UnmarkIssueAsDuplicatePayload.Create);

        /// <summary>
        /// Marks a review thread as unresolved.
        /// </summary>
        public UnresolveReviewThreadPayload UnresolveReviewThread(Arg<UnresolveReviewThreadInput> input) => this.CreateMethodCall(x => x.UnresolveReviewThread(input), Octokit.GraphQL.Model.UnresolveReviewThreadPayload.Create);

        /// <summary>
        /// Create a new branch protection rule
        /// </summary>
        public UpdateBranchProtectionRulePayload UpdateBranchProtectionRule(Arg<UpdateBranchProtectionRuleInput> input) => this.CreateMethodCall(x => x.UpdateBranchProtectionRule(input), Octokit.GraphQL.Model.UpdateBranchProtectionRulePayload.Create);

        /// <summary>
        /// Update a check run
        /// </summary>
        public UpdateCheckRunPayload UpdateCheckRun(Arg<UpdateCheckRunInput> input) => this.CreateMethodCall(x => x.UpdateCheckRun(input), Octokit.GraphQL.Model.UpdateCheckRunPayload.Create);

        /// <summary>
        /// Modifies the settings of an existing check suite
        /// </summary>
        public UpdateCheckSuitePreferencesPayload UpdateCheckSuitePreferences(Arg<UpdateCheckSuitePreferencesInput> input) => this.CreateMethodCall(x => x.UpdateCheckSuitePreferences(input), Octokit.GraphQL.Model.UpdateCheckSuitePreferencesPayload.Create);

        /// <summary>
        /// Sets the action execution capability setting for an enterprise.
        /// </summary>
        public UpdateEnterpriseActionExecutionCapabilitySettingPayload UpdateEnterpriseActionExecutionCapabilitySetting(Arg<UpdateEnterpriseActionExecutionCapabilitySettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseActionExecutionCapabilitySetting(input), Octokit.GraphQL.Model.UpdateEnterpriseActionExecutionCapabilitySettingPayload.Create);

        /// <summary>
        /// Updates the role of an enterprise administrator.
        /// </summary>
        public UpdateEnterpriseAdministratorRolePayload UpdateEnterpriseAdministratorRole(Arg<UpdateEnterpriseAdministratorRoleInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseAdministratorRole(input), Octokit.GraphQL.Model.UpdateEnterpriseAdministratorRolePayload.Create);

        /// <summary>
        /// Sets whether private repository forks are enabled for an enterprise.
        /// </summary>
        public UpdateEnterpriseAllowPrivateRepositoryForkingSettingPayload UpdateEnterpriseAllowPrivateRepositoryForkingSetting(Arg<UpdateEnterpriseAllowPrivateRepositoryForkingSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseAllowPrivateRepositoryForkingSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseAllowPrivateRepositoryForkingSettingPayload.Create);

        /// <summary>
        /// Sets the default repository permission for organizations in an enterprise.
        /// </summary>
        public UpdateEnterpriseDefaultRepositoryPermissionSettingPayload UpdateEnterpriseDefaultRepositoryPermissionSetting(Arg<UpdateEnterpriseDefaultRepositoryPermissionSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseDefaultRepositoryPermissionSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseDefaultRepositoryPermissionSettingPayload.Create);

        /// <summary>
        /// Sets whether organization members with admin permissions on a repository can change repository visibility.
        /// </summary>
        public UpdateEnterpriseMembersCanChangeRepositoryVisibilitySettingPayload UpdateEnterpriseMembersCanChangeRepositoryVisibilitySetting(Arg<UpdateEnterpriseMembersCanChangeRepositoryVisibilitySettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseMembersCanChangeRepositoryVisibilitySetting(input), Octokit.GraphQL.Model.UpdateEnterpriseMembersCanChangeRepositoryVisibilitySettingPayload.Create);

        /// <summary>
        /// Sets the members can create repositories setting for an enterprise.
        /// </summary>
        public UpdateEnterpriseMembersCanCreateRepositoriesSettingPayload UpdateEnterpriseMembersCanCreateRepositoriesSetting(Arg<UpdateEnterpriseMembersCanCreateRepositoriesSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseMembersCanCreateRepositoriesSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseMembersCanCreateRepositoriesSettingPayload.Create);

        /// <summary>
        /// Sets the members can delete issues setting for an enterprise.
        /// </summary>
        public UpdateEnterpriseMembersCanDeleteIssuesSettingPayload UpdateEnterpriseMembersCanDeleteIssuesSetting(Arg<UpdateEnterpriseMembersCanDeleteIssuesSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseMembersCanDeleteIssuesSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseMembersCanDeleteIssuesSettingPayload.Create);

        /// <summary>
        /// Sets the members can delete repositories setting for an enterprise.
        /// </summary>
        public UpdateEnterpriseMembersCanDeleteRepositoriesSettingPayload UpdateEnterpriseMembersCanDeleteRepositoriesSetting(Arg<UpdateEnterpriseMembersCanDeleteRepositoriesSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseMembersCanDeleteRepositoriesSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseMembersCanDeleteRepositoriesSettingPayload.Create);

        /// <summary>
        /// Sets whether members can invite collaborators are enabled for an enterprise.
        /// </summary>
        public UpdateEnterpriseMembersCanInviteCollaboratorsSettingPayload UpdateEnterpriseMembersCanInviteCollaboratorsSetting(Arg<UpdateEnterpriseMembersCanInviteCollaboratorsSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseMembersCanInviteCollaboratorsSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseMembersCanInviteCollaboratorsSettingPayload.Create);

        /// <summary>
        /// Sets whether or not an organization admin can make purchases.
        /// </summary>
        public UpdateEnterpriseMembersCanMakePurchasesSettingPayload UpdateEnterpriseMembersCanMakePurchasesSetting(Arg<UpdateEnterpriseMembersCanMakePurchasesSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseMembersCanMakePurchasesSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseMembersCanMakePurchasesSettingPayload.Create);

        /// <summary>
        /// Sets the members can update protected branches setting for an enterprise.
        /// </summary>
        public UpdateEnterpriseMembersCanUpdateProtectedBranchesSettingPayload UpdateEnterpriseMembersCanUpdateProtectedBranchesSetting(Arg<UpdateEnterpriseMembersCanUpdateProtectedBranchesSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseMembersCanUpdateProtectedBranchesSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseMembersCanUpdateProtectedBranchesSettingPayload.Create);

        /// <summary>
        /// Sets the members can view dependency insights for an enterprise.
        /// </summary>
        public UpdateEnterpriseMembersCanViewDependencyInsightsSettingPayload UpdateEnterpriseMembersCanViewDependencyInsightsSetting(Arg<UpdateEnterpriseMembersCanViewDependencyInsightsSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseMembersCanViewDependencyInsightsSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseMembersCanViewDependencyInsightsSettingPayload.Create);

        /// <summary>
        /// Sets whether organization projects are enabled for an enterprise.
        /// </summary>
        public UpdateEnterpriseOrganizationProjectsSettingPayload UpdateEnterpriseOrganizationProjectsSetting(Arg<UpdateEnterpriseOrganizationProjectsSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseOrganizationProjectsSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseOrganizationProjectsSettingPayload.Create);

        /// <summary>
        /// Updates an enterprise's profile.
        /// </summary>
        public UpdateEnterpriseProfilePayload UpdateEnterpriseProfile(Arg<UpdateEnterpriseProfileInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseProfile(input), Octokit.GraphQL.Model.UpdateEnterpriseProfilePayload.Create);

        /// <summary>
        /// Sets whether repository projects are enabled for a enterprise.
        /// </summary>
        public UpdateEnterpriseRepositoryProjectsSettingPayload UpdateEnterpriseRepositoryProjectsSetting(Arg<UpdateEnterpriseRepositoryProjectsSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseRepositoryProjectsSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseRepositoryProjectsSettingPayload.Create);

        /// <summary>
        /// Sets whether team discussions are enabled for an enterprise.
        /// </summary>
        public UpdateEnterpriseTeamDiscussionsSettingPayload UpdateEnterpriseTeamDiscussionsSetting(Arg<UpdateEnterpriseTeamDiscussionsSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseTeamDiscussionsSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseTeamDiscussionsSettingPayload.Create);

        /// <summary>
        /// Sets whether two factor authentication is required for all users in an enterprise.
        /// </summary>
        public UpdateEnterpriseTwoFactorAuthenticationRequiredSettingPayload UpdateEnterpriseTwoFactorAuthenticationRequiredSetting(Arg<UpdateEnterpriseTwoFactorAuthenticationRequiredSettingInput> input) => this.CreateMethodCall(x => x.UpdateEnterpriseTwoFactorAuthenticationRequiredSetting(input), Octokit.GraphQL.Model.UpdateEnterpriseTwoFactorAuthenticationRequiredSettingPayload.Create);

        /// <summary>
        /// Updates an Issue.
        /// </summary>
        public UpdateIssuePayload UpdateIssue(Arg<UpdateIssueInput> input) => this.CreateMethodCall(x => x.UpdateIssue(input), Octokit.GraphQL.Model.UpdateIssuePayload.Create);

        /// <summary>
        /// Updates an IssueComment object.
        /// </summary>
        public UpdateIssueCommentPayload UpdateIssueComment(Arg<UpdateIssueCommentInput> input) => this.CreateMethodCall(x => x.UpdateIssueComment(input), Octokit.GraphQL.Model.UpdateIssueCommentPayload.Create);

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
        /// Update a pull request
        /// </summary>
        public UpdatePullRequestPayload UpdatePullRequest(Arg<UpdatePullRequestInput> input) => this.CreateMethodCall(x => x.UpdatePullRequest(input), Octokit.GraphQL.Model.UpdatePullRequestPayload.Create);

        /// <summary>
        /// Updates the body of a pull request review.
        /// </summary>
        public UpdatePullRequestReviewPayload UpdatePullRequestReview(Arg<UpdatePullRequestReviewInput> input) => this.CreateMethodCall(x => x.UpdatePullRequestReview(input), Octokit.GraphQL.Model.UpdatePullRequestReviewPayload.Create);

        /// <summary>
        /// Updates a pull request review comment.
        /// </summary>
        public UpdatePullRequestReviewCommentPayload UpdatePullRequestReviewComment(Arg<UpdatePullRequestReviewCommentInput> input) => this.CreateMethodCall(x => x.UpdatePullRequestReviewComment(input), Octokit.GraphQL.Model.UpdatePullRequestReviewCommentPayload.Create);

        /// <summary>
        /// Update a Git Ref.
        /// </summary>
        public UpdateRefPayload UpdateRef(Arg<UpdateRefInput> input) => this.CreateMethodCall(x => x.UpdateRef(input), Octokit.GraphQL.Model.UpdateRefPayload.Create);

        /// <summary>
        /// Update information about a repository.
        /// </summary>
        public UpdateRepositoryPayload UpdateRepository(Arg<UpdateRepositoryInput> input) => this.CreateMethodCall(x => x.UpdateRepository(input), Octokit.GraphQL.Model.UpdateRepositoryPayload.Create);

        /// <summary>
        /// Updates the state for subscribable subjects.
        /// </summary>
        public UpdateSubscriptionPayload UpdateSubscription(Arg<UpdateSubscriptionInput> input) => this.CreateMethodCall(x => x.UpdateSubscription(input), Octokit.GraphQL.Model.UpdateSubscriptionPayload.Create);

        /// <summary>
        /// Updates a team discussion.
        /// </summary>
        public UpdateTeamDiscussionPayload UpdateTeamDiscussion(Arg<UpdateTeamDiscussionInput> input) => this.CreateMethodCall(x => x.UpdateTeamDiscussion(input), Octokit.GraphQL.Model.UpdateTeamDiscussionPayload.Create);

        /// <summary>
        /// Updates a discussion comment.
        /// </summary>
        public UpdateTeamDiscussionCommentPayload UpdateTeamDiscussionComment(Arg<UpdateTeamDiscussionCommentInput> input) => this.CreateMethodCall(x => x.UpdateTeamDiscussionComment(input), Octokit.GraphQL.Model.UpdateTeamDiscussionCommentPayload.Create);

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