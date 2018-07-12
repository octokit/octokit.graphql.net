# Variables

Variables can be used to pass parameters to [compiled queries](compiling-queries.md).

To define a variable, you should:

- Add a `using static Octokit.GraphQL.Variable` statement to your file
- Add a `Var(name)` call to the queries where you want to pass in a variable

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
