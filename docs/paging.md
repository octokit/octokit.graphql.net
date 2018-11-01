# Paging

## Manual Paging

For more information see: https://graphql.org/learn/pagination/

Many GraphQL fields can be paged. These fields typically define at least four paging parameters:

|Parameter|Description|
|---------|-----------|
|`first`|Pages forward N results at a time|
|`after`|The end cursor of the previous page|
|`last`|Pages backwards N results at a time|
|`before`|The start cursor of the previous page|

In addition, these fields produce a `Connection` object which contains a list of `edges` and
`nodes`, together with a `totalCount` and a `pageInfo` object.

To run a paged query, you would write code like this:

```csharp
// Read a page of issues from a Repsitory.
var query = new Query()
    .Repository("octokit", "octokit.net")
    .Issues(first: 100, after: Var("after"))
    .Select(connection => new
    {
        connection.PageInfo.EndCursor,
        connection.PageInfo.HasNextPage,
        connection.TotalCount,
        Items = connection.Nodes.Select(issue => new
        {
            issue.Number,
            issue.Title,
        }).ToList(),
    }).Compile();

// For the first page, set `after` to null.
var vars = new Dictionary<string, object>
{
    { "after", null },
};

// Read the first page.
var result = await connection.Run(query, vars);

// If there are more pages, set `after` to the end cursor.
vars["after"] = result.HasNextPage ? result.EndCursor : null;

while (vars["after"] != null)
{
    // Read the next page.
    var page = await connection.Run(query, vars);

    // Add the results from the page to the result.
    result.Items.AddRange(page.Items);

    // If there are more pages, set `after` to the end cursor.
    vars["after"] = page.HasNextPage ? page.EndCursor : null;
}
```

As you can see, it's rather complex for such a common task, and it gets even more complicated if
you need to do nested paging.

Thankfully, there is an easier way - auto-paging!

## Auto-Paging

With auto-paging, the above query can be rewritten as:

```csharp
var query = new Query()
    .Repository("octokit", "octokit.net")
    .Issues()
    .AllPages() // Auto-pages Issues
    .Select(issue => new
    {
        issue.Number,
        issue.Title,
    }).Compile();

var result = await connection.Run(query);
```

To enable auto-paging, you simply call `.AllPages()` where you would usually call `.Nodes`.
Because Octokit.GraphQL.net understands your query, it can rewrite the query to automatically
do the paging for you.

This also works for nested queries:

```csharp
var query = new Query()
    .Repository("octokit", "octokit.net")
    .Issues()
    .AllPages() // Auto-pages Issues
    .Select(issue => new
    {
        issue.Id,
        Comments = issue
            .Comments(null, null, null, null)
            .AllPages() // Auto-pages Issue Comments
            .Select(comment => comment.Body)
            .ToList(),
    });
```
