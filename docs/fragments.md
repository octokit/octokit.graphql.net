# Fragments

Fragments can be used to define a selection set of data.

Fragments can be reused multiple times within the same query or different queries.

_*Note*_: Fragments cannot select anonymous types.

#### Example:

Fragments can select fields and be reused in different queries.

```
var fragment = new Fragment<Model.Repository, string>("repositoryName", repo => repo.Name);

var query1 = new Query()
    .Repository("octokit", "octokit.net")
    .Select(fragment);

var repositoryName = Connection.Run(query1).Result;

Assert.Equal("octokit.net", repositoryName);

var query2 = new Query()
    .Organization("octokit")
    .Repositories(first: 100)
    .Nodes
    .Select(fragment);

var repositoryName = Connection.Run(query2).Result.OrderByDescending(s => s).First();

Assert.Equal("webhooks.js", repositoryName);
```

#### Example:
Fragments can select objects and be reused multiple times in the same query

```
public class RepositoryModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int ForkCount { get; set; }
}
```

```
var fragment = new Fragment<Model.Repository, RepositoryModel>("repositoryName", repo => new TestModelObject
{
    Name = repo.Name,
    Description = repo.Description
    ForkCount = repo.ForkCount,
});

var query = new Query()
    .Select(q => new
    {
        repo1 = q.Repository("octokit", "octokit.net").Select(fragment).Single(),
        repo2 = q.Repository("octokit", "octokit.graphql.net").Select(fragment).Single(),
    });

var result = Connection.Run(query).Result;

Assert.Equal("octokit.net", result.repo1.StringField1);
Assert.Equal("octokit.graphql.net", result.repo2.StringField1);
```