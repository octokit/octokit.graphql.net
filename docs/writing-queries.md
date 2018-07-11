# Writing Queries

Note that this guide assumes you are familiar withthe  GraphQL query syntax. If you're not,
you can read more about it [here](https://graphql.org/learn/).

## The Root Query

There are two root query objects, `Query` and `Mutation`. To create a query, you first create
an instance of one of these root query objects and then use its properties and methods to build
your query.

## `Select`ing Fields

The GraphQL syntax in C# is designed to match the GraphQL query syntax as closely as possible,
while still remaning valid C#. In a GraphQL query, you must always specify the fields you want
returned and that also holds true for GraphQL queries in C#.

In a C# GraphQL query, fields are selected using the `Select` operator as in regular LINQ
queries:

```csharp
var query = new Query()
    .RepositoryOwner("octokit")
    .Repositories(first: 30)
    .Nodes
    .Select(x => new
    {
        Name = x.Name,
        Description = x.Description,
    });
```

This will build the following GraphQL query:

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

The above `Select` operator works on a list of repositories, but it also works on single items too:

```csharp
var query = new Query()
    .Repository("octokit", "octokit.net")
    .Select(r => new
    {
        r.Name,
        r.Description,
    });
```

Here `Repository(owner, name)` returns a single `Repository` object rather than a list.

## Nested Queries

GraphQL queries tend to be nested. This is achieved in C# by assigning the sub-query to a field in
the selected object. If you've hovered over a `Select` with intellisense, you may have noticed that
its return type is `IQueryableValue<T>` or `IQueryableList<T>` - so how do we convert values of these
types to the associated `T`?

The answer is by using `ToList()` where you have an `IQueryableList<T>`. The following example gets
the repository name and description, together with a list of the first 100 issue numbers and titles:

```csharp
var query = new Query()
    .Repository("octokit", "octokit.net")
    .Select(r => new
    {
        r.Name,
        r.Description,
        Issues = r.Issues(100, null, null, null, null, null, null).Select(i => new
        {
            i.Number,
            i.Title,
        }).ToList(),
    });
```

> You can see a bunch of `null` parameters in the `r.Issues()` call. This is because expressions in
  C# do not support default parameter values, so all the unused values need to be supplied an explicit
  null. It sucks.

For `IQueryableValue<T>` you can use `Single()` or `SingleOrDefault()`:

```csharp
var query = new Query()
    .Repository("octokit", "octokit.net")
    .Select(r => new
    {
        Parent = r.Parent.Select(p => new
        {
            p.Name,
            p.Owner,
        }).SingleOrDefault(),
    });
```

Notice that here, `Repository.Parent` may be null, so it makes sense to use the `SingleOrDefault`
operator, especially as the `?.` operator isn't supported in C# expressions.

## Selecting Sub-fields Directly

Sometimes it's easier to select a sub-field directly. Consider this query:

```csharp
var query = new Query()
    .Repository("octokit", "octokit.net")
    .Select(r => new
    {
        Login = r.Owner.Select(x => x.Login).Single(),
    });
```

It's a little more readable to write this as:

```csharp
var query = new Query()
    .Repository("octokit", "octokit.net")
    .Select(r => new
    {
        Login = r.Owner.Login,
    });
```

## Selecting into Named Classes

In the above examples, we've been selecting into anonymous classes. This has been for illustration
purposes only; in an actual application you would most likely be selecting into named model classes:

```csharp
class RepositoryModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<IssueModel> Issues { get; set; }
}

class IssueModel
{
    public int Number { get;set; }
    public string Title { get; set; }
}

var query = new Query()
    .Repository("octokit", "octokit.net")
    .Select(r => new RepositoryModel
    {
        r.Name,
        r.Description,
        Issues = r.Issues(100, null, null, null, null, null, null).Select(i => new IssueModel
        {
            i.Number,
            i.Title,
        }).ToList(),
    });
```