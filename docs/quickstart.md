# Octokit.GraphQL.net Quickstart

## Creating a connection

The first thing you will need to do is to create a `Connection` with a personal access token.

For more information on creating personal access tokens, see
[the article](https://help.github.com/articles/creating-a-personal-access-token-for-the-command-line/)

```csharp
using Octokit.GraphQL;

var productInformation = new ProductHeaderValue("YOUR_PRODUCT_NAME", "YOUR_PRODUCT_VERSION");
var connection = new Connection(productInformation, Connection.GithubApiUri, YOUR_OAUTH_TOKEN);
```

## Showing the Viewer's Username

Lets get started with a simple query that shows your username:

```csharp
var query = new Query().Viewer.Select(x => x.Login);
```

This represents the following GraphQL query:

```
query {
  viewer {
    login
  }
}
```

To run the query, use the connection you created:

```
var result = await connection.Run(query);
Console.WriteLine(result.Single()); // Prints your username
```
