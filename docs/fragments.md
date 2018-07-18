# Fragments

Fragments can be used to define a selection set of data.

For example:

```
using static Octokit.GraphQL.Variable;

var query = new Query()
    .Repository(Var("owner"), Var("name"))
    .Select(repository => new RepositoryModel
    {
        Name = repository.Name,
        Description = repository.Name,
    }).Compile();
```

You can now use those variables by passing an `IDictionary<string, object>` into `Connection.Run`:

```
var vars = new Dictionary<string, object>
{
    { "owner", "octokit" },
    { "name", "Octokit.GraphQL.net" },
};

var result = await connection.Run(query, vars);
```
