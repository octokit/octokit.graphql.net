<h1 align="center">Octokit.GraphQL.NET</h1>

<p align="center">
  <a style="text-decoration:none" href="https://github.com/octokit/octokit.graphql.net/actions/workflows/dotnetcore.yml">
    <img src="https://github.com/octokit/octokit.graphql.net/actions/workflows/dotnetcore.yml/badge.svg" alt="CI Status" /></a>
  <a style="text-decoration:none" href="https://codecov.io/gh/octokit/octokit.graphql.net">
    <img src="https://codecov.io/gh/octokit/octokit.graphql.net/branch/main/graph/badge.svg" alt="CodeCov Status" /></a>
  <a style="text-decoration:none" href="https://www.nuget.org/packages/Octokit.GraphQL">
    <img src="http://img.shields.io/nuget/v/Octokit.GraphQL.svg" alt="NuGet" /></a>
</p>

Octokit.GraphQL.NET gives you access to the GitHub GraphQL API from .NET. It exposes the GitHub GraphQL API as a strongly-typed LINQ-like API, aiming to follow the GraphQL query syntax as closely as possible, which giving the benefits of strong typing in your favorite .NET language.

> [!NOTE]
> This software is currently in beta. There are few things left, and there might be bugs - be warned!

## Getting started with GitHub GraphQL API

To learn more about GitHub GraphQL API, visit [GitHub Docs](https://docs.github.com/graphql/overview)

## Usage

1. Install the NuGet package
```ps1
Install-Package Octokit.GraphQL -IncludePrerelease
```
2. Feed the package
```C#
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

```C#
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
