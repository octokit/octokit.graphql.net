# Octokit.GraphQL

**Note**: This software is currently alpha. There are lots of things missing, and there will be bugs - be warned!

Octokit.GraphQL gives you access to the GitHub GraphQL API from .NET. It exposes the GitHub GraphQL API as a strongly-typed LINQ-like API, aiming to follow the GraphQL query syntax as closely as possible, which giving the benefits of strong typing in your favorite .NET language.

## Quickstart

You must first sign up for the GraphQL early access program and create a OAUTH token to access the GraphQL API. See [this guide](https://developer.github.com/early-access/graphql/guides/accessing-graphql/) for more information.

Once you have an OAUTH token, you can create a connection from C# as follows:

```csharp
using Octokit.GraphQL;
using Octokit.GraphQL.Core;

var connection = new Connection("https://api.github.com/graphql", YOUR_OAUTH_TOKEN); 
```

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

```csharp
var result = await connection.Run(query);
Console.WriteLine(result.Single()); // Prints your username
```

Lets create a more complex query that gets the name and description of each repository in an organization:

```csharp
var query = new Query()
    .RepositoryOwner("octokit")
    .Repositories(first: 30)
    .Nodes
    .Select(x => new
    {
        x.Name,
        x.Description,
    });
```

This relates to the GraphQL query:

```
query { 
  repositoryOwner(login: "octokit") {
    repositories(first:30) {
      nodes {
        name
        description
      }
    }
  }
}
```

You can also place the results of the query into a model class:

```csharp
class ExampleModel
{
    public string Name { get; set; }
    public string Description { get; set; }
}

var query = new Query()
    .RepositoryOwner("octokit")
    .Repositories(first: 30)
    .Nodes
    .Select(x => new ExampleModel
    {
        Name = x.Name,
        Description = x.Description,
    });
```

