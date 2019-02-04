# Query Examples

This page contains examples of the GraphQL syntax in C#. This is provided purely as 
an example of some of the types of queries that can be written

## Branch History Query

```csharp
var query = new Query()
    .Repository("octokit.graphql.net", "octokit")
    .Ref(qualifiedName: "refs/heads/master")
    .Target.Cast<Commit>()
    .History(10, null, null, null, null, null, null, null)
    .Edges
    .Select(e => new
    {
        e.Node.Id,
        e.Cursor,
        e.Node.Oid,
        e.Node.Author.Name,
        e.Node.Message
    }).Compile();
```