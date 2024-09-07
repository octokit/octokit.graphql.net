<h1 align="center">Octokit.GraphQL.NET</h1>

<p align="center">
  <a style="text-decoration:none" href="https://github.com/octokit/octokit.graphql.net/actions/workflows/dotnetcore.yml">
    <img src="https://github.com/octokit/octokit.graphql.net/actions/workflows/dotnetcore.yml/badge.svg" alt="CI Status" /></a>
  <a style="text-decoration:none" href="https://codecov.io/gh/octokit/octokit.graphql.net">
    <img src="https://codecov.io/gh/octokit/octokit.graphql.net/branch/main/graph/badge.svg" alt="CodeCov Status" /></a>
  <a style="text-decoration:none" href="https://www.nuget.org/packages/Octokit.GraphQL">
    <img src="http://img.shields.io/nuget/v/Octokit.GraphQL.svg" alt="NuGet" /></a>
</p>

Octokit.GraphQL.NET gives you access to the GitHub GraphQL API from within the .NET Framework. It exposes the GitHub GraphQL API as a strongly-typed LINQ-like API, aiming to follow the GraphQL query syntax as closely as possible, giving the benefits of strong typing across the world's leading programming framework, .NET.

> [!NOTE]
> This software is currently in beta. There are few things left, and there might be bugs - be warned!

## Getting started

The full documentation of GraphQL API can be found on [GitHub Docs](https://docs.github.com/graphql/overview):

- [GraphQL public schema](https://docs.github.com/graphql/overview/public-schema)
- [GraphQL explorer](https://docs.github.com/graphql/overview/explorer)
- [API specs of Octokit.GraphQL.NET](https://github.com/octokit/octokit.graphql.net/tree/main/docs)

To install the package from the command line, run the following command:

```ps1
Install-Package Octokit.GraphQL -IncludePrerelease
```

## Usage scenarios

```cs
using Octokit.GraphQL;
using static Octokit.GraphQL.Variable;

// Authenticate with a PAT with a scope 'read:user'
var connection = new Connection(new("Octokit.GraphQL.Net.SampleApp", "1.0"), "LOGGED_IN_GITHUB_USER_TOKEN");

var query = new Query()
    .RepositoryOwner(Var("owner"))
    .Repository(Var("name"))
    .Select(repo => new
    {
        repo.Id,
        repo.Name,
        repo.Owner.Login,
        repo.IsFork,
        repo.IsPrivate,
    }).Compile();

var vars = new Dictionary<string, object>
{
    { "owner", "octokit" },
    { "name", "octokit.graphql.net" },
};

var result =  await connection.Run(query, vars);

Console.WriteLine(result.Login + " & " + result.Name + " Rocks!");
```

```cs
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

// Authenticate with a PAT with a scope 'read:user'
var connection = new Connection(new("Octokit.GraphQL.Net.SampleApp", "1.0"), "LOGGED_IN_GITHUB_USER_TOKEN");

// A query to list out who you are actively sponsoring
// That auto pages through all sponsors
var query = new Query()
    .Viewer
    .SponsorshipsAsSponsor()
    .AllPages()
    .Select(sponsoring => new
    {
        User = sponsoring.Sponsorable
            .Cast<User>()
            .Select(x => new
            {
                x.Login,
                x.Name,
                x.Id
            }.Single()
    }).Compile();

// Queries from the GraphQL API end point
var result = await connection.Run(query);

// Check if sponsoring 'warrenbuckley'
var activeSponsor = result.SingleOrDefault(x => x.User.Login.ToLowerInvariant() == "warrenbuckley");
if(activeSponsor != null)
{
    Console.WriteLine("Thanks for sponsoring Warren");
}
```

## Contributing & Feedback

There are multiple ways to participate in the community:

- Upvote popular feature requests
- [Submit a new feature](https://github.com/octokit/octokit.graphql.net/pulls)
- [File bugs and feature requests](https://github.com/octokit/octokit.graphql.net/issues/new/choose).
- Review source [code changes](https://github.com/octokit/octokit.graphql.net/commits)

## Building from source

### Prerequisites

1. Ensure you have following components:
    - [Git](https://git-scm.com/)
    - [Visual Studio and the .NET SDK](https://visualstudio.microsoft.com/vs/)
2. Clone the repository:
    ```git
    git clone https://github.com/octokit/octokit.graphql.net
    ```

### Building the project

- Open `Octokit.GraphQL.sln`.
- Set the Startup Project to  `Octokit.GraphQL` or a test project as appropriate
- Build with `DEBUG|x64` (or `DEBUG|Any CPU`)

### Codebase structure

```
.
├──Scripts                                    // Code quality scripts
│  └──configure-integration-tests.ps1         // Integration tests configuration script
├──Tools                                      // Code quality tools
│  └──Generate                                // GraphQL .NET entity generator
├──Octokit.GraphQL                            // Main API data contracts library
├──Octokit.GraphQL.Core                       // Octokit core code
├──Octokit.GraphQL.Core.Generation            // Core entity generator tools
├──Octokit.GraphQL.Core.Generation.UnitTests  // Core entity generator unit tests
├──Octokit.GraphQL.Core.UnitTests             // Octokit core unit tests
├──Octokit.GraphQL.IntegrationTests           // Octokit integration tests
└──Octokit.GraphQL.UnitTests                  // Octokit unit tests
```
