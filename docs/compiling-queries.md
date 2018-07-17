## Compiling Queries

In the [quickstart](quickstart.md) we created a query and ran it directly on the connection:

```csharp
var query = new Query().Viewer.Select(x => x.Login);
var productInformation = new ProductHeaderValue("YOUR_PRODUCT_NAME", "YOUR_PRODUCT_VERSION");
var connection = new Connection(productInformation, Connection.GithubApiUri, YOUR_OAUTH_TOKEN);
var result = await connection.Run(query);
```

This is fine for queries you only want to run once, but each time this query is run, the query
expression needs to be parsed and the GraphQL query created. If you're going to be re-using a
query it is better to compile the query and store it somewhere:

```csharp
class MyService
{
    static readonly ICompiledQuery<string> myQuery = new Query()
        .Viewer
        .Select(x => x.Login)
        .Compile();
}
```

If you need to pass parameters to a compiled query, you can use [variables](variables.md).
