## Updating the schema

Note: the OAuth token referenced below represents a [personal access token](https://github.com/settings/tokens)

1. Delete the `Model` folder from `Octokit.GraphQL`
2. Set the Application arguments for the `Tools\Generate` project to `[OAuth Token] [Octokit.GraphQL Project Path]`
3. Build and run the `Tools\Generate` project

It will recreate the `Model` folder in `Octokit.GraphQL`


## Running Integration Tests

Integration tests in the project `Octokit.GraphQL.IntegrationTests` require an OAuth token.
The OAuth token and corresponding username should be set as the following two environment variables.
- OCTOKIT_GQL_OAUTHTOKEN
- OCTOKIT_GQL_GITHUBUSERNAME