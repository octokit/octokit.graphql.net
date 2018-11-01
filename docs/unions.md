# Unions

GraphQL union types have a single method on then: `Switch` which is used to select a model
depending on the type returned in the query data. For example, the `IssueOrPullRequest` type
can contain an issue or a pull request. Supposing we have the following model classes:

```csharp
class IssueishModel
{
    public int Number { get; set; }
    public string Title { get; set; }
}

class IssueModel : IssueishModel
{
	public bool Closed { get; set; }
}

class PullRequestModel : IssueishModel
{
	public int ChangedFiles { get; set; }
}
```

We can use the `Switch` method to select the correct model:

```csharp
var expression = new Query()
    .Repository("octokit", "octokit.net")
    .IssueOrPullRequest(1)
    .Select(issueOrPr => issueOrPr.Switch<IssueishModel>(when =>
        when.Issue(issue => new IssueModel
        {
            Number = issue.Number,
			Title = issue.Title,
			Closed = issue.Closed,
        }).PullRequest(pr => new PullRequestModel
        {
            Number = pr.Number,
			Title = pr.Title,
			ChangedFiles = pr.ChangedFiles
        })));
```

The generic parameter passed to `Switch<T>` is the type of model to return. In this case,
`IssueModel` and `PullRequestModel` have a common base class so we can use that. If the models
don't have a common base class, `object` can be used.

> Note that currently, if a type is returned for which there is no selection in the `Switch`
  then `null` will be returned. This may change in future.