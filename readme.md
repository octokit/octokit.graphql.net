# Octokit.GraphQL

[![Build status](https://ci.appveyor.com/api/projects/status/falhvlth7og0nkw4/branch/main?svg=true)](https://ci.appveyor.com/project/github-windows/octokit-graphql/branch/main)
[![codecov](https://codecov.io/gh/octokit/octokit.graphql.net/branch/main/graph/badge.svg)](https://codecov.io/gh/octokit/octokit.graphql.net)
[![NuGet](http://img.shields.io/nuget/v/Octokit.GraphQL.svg)](https://www.nuget.org/packages/Octokit.GraphQL)

**Note**: This software is currently beta. There are few things left, and there might be bugs - be warned!

Octokit.GraphQL gives you access to the GitHub GraphQL API from .NET. It exposes the GitHub GraphQL API as a strongly-typed LINQ-like API, aiming to follow the GraphQL query syntax as closely as possible, which giving the benefits of strong typing in your favorite .NET language.

## Documentation

You can find our documentation [here](docs/readme.md).

## Installing from Nuget

```
Install-Package Octokit.GraphQL -IncludePrerelease
```

## Usage Example

```csharp
using Octokit.GraphQL;
using static Octokit.GraphQL.Variable;

var productInformation = new ProductHeaderValue("YOUR_PRODUCT_NAME", "YOUR_PRODUCT_VERSION");
var connection = new Connection(productInformation, YOUR_OAUTH_TOKEN);

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

```csharp
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Octokit
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var productInformation = new ProductHeaderValue("ExampleCode", "1.0");

            // Personal Access Token - needs a scope of 'read:user'
            var connection = new Connection(productInformation, "LOGGED_IN_GITHUB_USER_TOKEN");

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
                            .Select(x => new {
                                x.Login,
                                x.Name,
                                x.Id
                            })
                            .Single()
                })
                .Compile();

            var result = await connection.Run(query);

            // Sponsoring 'warrenbuckley' ?
            var activeSponsor = result.SingleOrDefault(x => x.User.Login.ToLowerInvariant() == "warrenbuckley");
            if(activeSponsor != null)
            {
                Console.WriteLine("Thanks for sponsoring Warren");
            }
        }
    }
}
```
