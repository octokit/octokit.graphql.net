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

## Select

All queries must end in `Select` statements that select primitive types. In the above `Viewer` example, we're selecting the `Login` field which is a string:

```csharp
var query = new Query().Viewer.Select(x => x.Login);
```

If we were to try to instead select the `Viewer` object itself, GraphQL will throw an exception:

```csharp
var query = new Query().Viewer.Select(x => x);
```

```
Objects must have selections (field 'viewer' returns User but has no selections)
```

Because a `Viewer` is a GraphQL `Object` type, you cannot select it: you must select which fields you want returned.

## More examples

Get the pull request number for a repository branch:

```csharp
var query = new Query()
    .Repository("github", "VisualStudio")
    .PullRequests(first: 30, headRefName: "fixes/971-show-docs-when-in-github-repo")
    .Nodes
    .Select(x => x.Number);
```

## TODO

There's a lot left to do:

- Mutations
- Work out a better way to handle paging
- More documentation
- API Deprecation support
- Unions don't work properly