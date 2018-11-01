using System;
using System.Collections.Generic;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.Core.UnitTests.Models;
using Xunit;
using static Octokit.GraphQL.Variable;

namespace Octokit.GraphQL.Core.UnitTests
{
    public class QueryBuilderTests
    {
        [Fact]
        public void Repository_Select_Single_Member()
        {
            var expected = "query{repository(owner:\"foo\",name:\"bar\"){name}}";

            var expression = new Query()
                .Repository("foo", "bar")
                .Select(x => x.Name);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Repository_Select_Multiple_Members()
        {
            var expected = "query{repository(owner:\"foo\",name:\"bar\"){name description}}";

            var expression = new Query()
                .Repository("foo", "bar")
                .Select(x => new { x.Name, x.Description });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Repository_Select_Single_Member_Append_String()
        {
            var expected = "query{repository(owner:\"foo\",name:\"bar\"){name}}";

            var expression = new Query()
                .Repository("foo", "bar")
                .Select(x => x.Name + " World!");

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Repository_Select_Append_Two_Members()
        {
            var expected = "query{repository(owner:\"foo\",name:\"bar\"){name description}}";

            var expression = new Query()
                .Repository("foo", "bar")
                .Select(x => x.Name + x.Description);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Repository_Select_Append_Two_Identical_Members()
        {
            var expected = "query{repository(owner:\"foo\",name:\"bar\"){name}}";

            var expression = new Query()
                .Repository("foo", "bar")
                .Select(x => x.Name + x.Name);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Repository_Cast_Member_To_Enum()
        {
            var expected = "query{repository(owner:\"foo\",name:\"bar\"){enum: forkCount}}";

            var expression = new Query()
                .Repository("foo", "bar")
                .Select(x => new
                {
                    Enum = (DayOfWeek)x.ForkCount
                });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Repository_Cast_Nullable_Member_To_Enum()
        {
            var expected = "query{repository(owner:\"foo\",name:\"bar\"){enum: databaseId}}";

            var expression = new Query()
                .Repository("foo", "bar")
                .Select(x => new
                {
                    Enum = (DayOfWeek)x.DatabaseId.Value
                });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Licenses_Select_Single_Member()
        {
            var expected = "query{licenses{body}}";

            var expression = new Query()
                .Licenses
                .Select(x => x.Body);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Licenses_Select_Multiple_Members()
        {
            var expected = "query{licenses{body description}}";

            var expression = new Query()
                .Licenses
                .Select(x => new{ x.Body, x.Description});

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Repository_Issue_Select_Multiple_Members()
        {
            var expected = "query{repository(owner:\"foo\",name:\"bar\"){issue(number:1){body closed}}}";

            var expression = new Query()
                .Repository("foo", "bar")
                .Issue(1)
                .Select(x => new { x.Body, x.Closed });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Repository_Licenses_Nested_Selects()
        {
            var expected = "query{licenses{body items: conditions{description}}}";

            var expression = new Query()
                .Licenses
                .Select(x => new
                {
                    x.Body,
                    Items = x.Conditions.Select(i => i.Description).ToList(),
                });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Repository_Licenses_Conditions_Select_ToDictionary()
        {
            var expected = "query{licenses{body items: conditions{key description}}}";

            var expression = new Query()
                .Licenses
                .Select(x => new
                {
                    x.Body,
                    Items = x.Conditions.Select(i => new
                    {
                        i.Key,
                        i.Description,
                    }).ToDictionary(d => d.Key, d => d.Description),
                });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Repository_Issues_Nested_Select_With_Captured_Parameter()
        {
            var expected = @"query {
  repository(owner: ""foo"", name: ""bar"") {
    items: issues(first: 10, after: ""foo"") {
      totalCount
    }
  }
}";

            var arg1 = "foo";
            var expression = new Query()
                .Repository("foo", "bar")
                .Select(x => new
                {
                    Items = x.Issues(10, arg1, null, null, null).Select(y => new
                    {
                        y.TotalCount,
                    }).Single()
                });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(2), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Nodes_Inline_Fragment_Issue_Comments()
        {
            var expected = "query{nodes(ids:[\"123\"]){__typename ... on Issue{number items: comments{nodes{body}}}}}";

            var expression = new Query()
                .Nodes(new[] { new ID("123") })
                .OfType<Issue>()
                .Select(x => new
                {
                    x.Number,
                    Items = x.Comments(null, null, null, null).Nodes.Select(i => i.Body).ToList(),
                });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Repository_Field_Aliases()
        {
            var expected = @"query {
  repository(owner: ""foo"", name: ""bar"") {
    foo: name
    bar: description
  }
}";

            var expression = new Query()
                .Repository("foo", "bar")
                .Select(x => new
                {
                    Foo = x.Name,
                    Bar = x.Description,
                });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Boolean_Parameter()
        {
            var expected = "query{rateLimit(dryRun:false){cost}}";

            var expression = new Query()
                .RateLimit(false)
                .Select(x => x.Cost);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Enumerable_Parameter()
        {
            var expected = "query{repository(owner:\"foo\",name:\"bar\"){issues(labels:[\"baz\",\"qux\"]){totalCount}}}";

            var expression = new Query()
                .Repository("foo", "bar")
                .Issues(labels: new[] { "baz", "qux" })
                .Select(x => x.TotalCount);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Integer_Parameter()
        {
            var expected = "query{repository(owner:\"foo\",name:\"bar\"){issue(number:5){body}}}";

            var expression = new Query()
                .Repository("foo", "bar")
                .Issue(5)
                .Select(x => x.Body);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Float_Parameter()
        {
            var expected = "query{floatTest(value:123.3){name}}";

            var expression = new Query()
                .FloatTest(123.3f)
                .Select(x => x.Name);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void ID_Parameter()
        {
            var expected = "query{node(id:\"123\"){__typename ... on Repository{name}}}";

            var expression = new Query()
                .Node(new ID("123"))
                .Cast<Repository>()
                .Select(x => x.Name);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void InputObject_Parameter()
        {
            var expected = "query{addComment(input:{subjectId:\"x\",body:\"body\",clientMutationId:\"1\"}){body}}";

            var input = new AddCommentInput
            {
                Body = "body",
                ClientMutationId = "1",
                SubjectId = new ID("x"),
            };

            var expression = new Query()
                .AddComment(input)
                .Select(x => x.Body);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void InputObject_Parameter_With_Null_Field()
        {
            var expected = "query{addComment(input:{subjectId:\"x\",body:null,clientMutationId:\"1\"}){body}}";

            var input = new AddCommentInput
            {
                Body = null,
                ClientMutationId = "1",
                SubjectId = new ID("x"),
            };

            var expression = new Query()
                .AddComment(input)
                .Select(x => x.Body);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void InputObject_Parameter_Hit_Cache()
        {
            var expected = "query{addComment(input:{subjectId:\"x\",body:\"body\",clientMutationId:\"1\"}){body}}";

            var expression = new Query()
                .AddComment(new AddCommentInput
                {
                    Body = "body",
                    ClientMutationId = "1",
                    SubjectId = new ID("x"),
                })
                .Select(x => x.Body);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));

            expected = "query{addComment(input:{subjectId:\"x\",body:\"different body\",clientMutationId:\"1\"}){body}}";

            expression = new Query()
                .AddComment(new AddCommentInput
                {
                    Body = "different body",
                    ClientMutationId = "1",
                    SubjectId = new ID("x"),
                })
                .Select(x => x.Body);

            query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Repository_Variable()
        {
            var expected = "query($var1:String!){repository(owner:\"foo\",name:$var1){name}}";

            var expression = new Query()
                .Repository("foo", Var("var1"))
                .Select(x => x.Name);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void IntValue_Variable()
        {
            var expected = "query($var1:Int!){repository(owner:\"foo\",name:\"bar\"){issue(number:$var1){body}}}";

            var expression = new Query()
                .Repository("foo", "bar")
                .Issue(Var("var1"))
                .Select(x => x.Body);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void InputObject_Variable()
        {
            var expected = "query($var1:AddCommentInput!){addComment(input:$var1){body}}";

            var expression = new Query()
                .AddComment(Var("var1"))
                .Select(x => x.Body);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Interface_Cast()
        {
            var expected = @"query {
  node(id: ""123"") {
    __typename
    ... on Repository {
      name
    }
  }
}";
            var expression = new Query()
                .Node(new ID("123"))
                .Cast<Repository>()
                .Select(x => x.Name);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Enumerable_Variable()
        {
            var expected = "query($var1:[String!]){repository(owner:\"foo\",name:\"bar\"){issues(labels:$var1){totalCount}}}";

            var expression = new Query()
                .Repository("foo", "bar")
                .Issues(labels: Var("var1"))
                .Select(x => x.TotalCount);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Multiple_Variables()
        {
            var expected = "query($foo:String!,$bar:String!){repository(owner:$foo,name:$bar){name}}";

            var expression = new Query()
                .Repository(Var("foo"), Var("bar"))
                .Select(x => x.Name);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Double_Quotes_In_String_Arg_Are_Escaped()
        {
            var expected = "query{repository(owner:\"string with \\\"quotes\\\" in it\",name:\"bar\"){name}}";

            var expression = new Query()
                .Repository("string with \"quotes\" in it", "bar")
                .Select(x => x.Name);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Backslash_In_String_Arg_Is_Escaped()
        {
            var expected = "query{repository(owner:\"string with \\\\ in it\",name:\"bar\"){name}}";

            var expression = new Query()
                .Repository("string with \\ in it", "bar")
                .Select(x => x.Name);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Double_Quotes_In_InputObject_Arg_Are_Escaped()
        {
            var expected = "query{addComment(input:{subjectId:\"\",body:null,clientMutationId:\"string with \\\"quotes\\\" in it\"}){body}}";

            var expression = new Query()
                .AddComment(new AddCommentInput { ClientMutationId = "string with \"quotes\" in it" })
                .Select(x => x.Body);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Backslash_In_InputObject_Arg_Is_Escaped()
        {
            var expected = "query{addComment(input:{subjectId:\"\",body:null,clientMutationId:\"string with \\\\ in it\"}){body}}";

            var expression = new Query()
                .AddComment(new AddCommentInput { ClientMutationId = "string with \\ in it" })
                .Select(x => x.Body);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Can_Select_To_New_Array()
        {
            var expected = "query{repository(owner:\"foo\",name:\"bar\"){issue(number:5){title body}}}";

            var expression = new Query()
                .Repository("foo", "bar")
                .Issue(5)
                .Select(x => new[] { x.Title, x.Body });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(0));
        }

        [Fact]
        public void Can_Select_Repo_Twice()
        {
            var expected = @"query {
  repo1: repository(owner: ""foo"", name: ""bar"") {
    name
  }
  repo2: repository(owner: ""foo"", name: ""bar"") {
    name
  }
}";

            var expression = new Query()
                .Select(q => new
                {
                    repo1 = q.Repository("foo", "bar").Select(repository => new { repository.Name }).Single(),
                    repo2 = q.Repository("foo", "bar").Select(repository => new { repository.Name }).Single()
                });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(2), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Cannot_Select_QueryableValue()
        {
            var expression = new Query()
                .Select(q => new
                {
                    repo1 = q.Repository("foo", "bar").Select(repository => new { repository.Name }),
                    repo2 = q.Repository("foo", "bar").Select(repository => new { repository.Name })
                });

            var exception = Assert.Throws<GraphQLException>(() =>
            {
                expression.Compile();
            });

            Assert.Equal(
                "Cannot directly select \'IQueryableValue<>\'. Use Single() or SingleOrDefault() to unwrap the value.",
                exception.Message);
        }

        [Fact]
        public void Cannot_Select_QueryableList()
        {
            var expression = new Query()
                .Select(x => new
                {
                    x.Licenses,
                });

            var exception = Assert.Throws<GraphQLException>(() =>
            {
                expression.Compile();
            });

            Assert.Equal(
                "Cannot directly select \'IQueryableList<>\'. Use ToList() to unwrap the value.",
                exception.Message);
        }

        [Fact]
        public void Repository_Select_Simple_Fragment()
        {
            var expected = @"query {
  repository(owner: ""foo"", name: ""bar"") {
    ...repositoryName
  }
}
fragment repositoryName on Repository {
  name
}";

            var fragment = new Fragment<Repository, string>("repositoryName", repository => repository.Name);

            var expression = new Query()
                .Repository("foo", "bar")
                .Select(fragment);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(2), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void RepositoryOwner_Select_Simple_Fragment()
        {
            var expected = @"query {
  repositoryOwner(login: ""foo"") {
    ...ownerLogin
  }
}
fragment ownerLogin on RepositoryOwner {
  login
}";

            var fragment = new Fragment<IRepositoryOwner, string>("ownerLogin", owner => owner.Login);

            var expression = new Query()
                .RepositoryOwner(login: "foo")
                .Select(fragment);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(2), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Repository_Select_Object()
        {
            var expected = @"query {
  repository(owner: ""foo"", name: ""bar"") {
    intField1: forkCount
    stringField1: name
    stringField2: description
  }
}";

            var expression = new Query()
                .Repository("foo", "bar")
                .Select(repository => new TestModelObject
                {
                    IntField1 = repository.ForkCount,
                    StringField1 = repository.Name,
                    StringField2 = repository.Description
                });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(2), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Repository_Select_Anon_Object()
        {
            var expected = @"query {
  repository(owner: ""foo"", name: ""bar"") {
    intField1: forkCount
    stringField1: name
    stringField2: description
  }
}";

            var expression = new Query()
                .Repository("foo", "bar")
                .Select(repository => new
                {
                    IntField1 = repository.ForkCount,
                    StringField1 = repository.Name,
                    StringField2 = repository.Description
                });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(2), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Repository_Select_Object_Fragment()
        {
            var expected = @"query {
  repository(owner: ""foo"", name: ""bar"") {
    ...repositoryName
  }
}
fragment repositoryName on Repository {
  intField1: forkCount
  stringField1: name
  stringField2: description
}";

            var fragment = new Fragment<Repository, TestModelObject>("repositoryName", repository => new TestModelObject
            {
                IntField1 = repository.ForkCount,
                StringField1 = repository.Name,
                StringField2 = repository.Description
            });

            var expression = new Query()
                .Repository("foo", "bar")
                .Select(fragment);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(2), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Repository_Select_Use_Simple_Fragment_Twice()
        {
            var expected = @"query {
  repo1: repository(owner: ""foo"", name: ""bar"") {
    ...repositoryName
  }
  repo2: repository(owner: ""foo"", name: ""bar"") {
    ...repositoryName
  }
}
fragment repositoryName on Repository {
  name
}";

            var fragment = new Fragment<Repository, string>("repositoryName", repository => repository.Name);

            var expression = new Query()
                .Select(q => new
                {
                    repo1 = q.Repository("foo", "bar").Select(fragment).SingleOrDefault(),
                    repo2 = q.Repository("foo", "bar").Select(fragment).SingleOrDefault()
                });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(2), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Repository_Select_Use_Object_Fragment_Twice()
        {
            var expected = @"query {
  repo1: repository(owner: ""foo"", name: ""bar"") {
    ...repositoryName
  }
  repo2: repository(owner: ""foo"", name: ""bar"") {
    ...repositoryName
  }
}
fragment repositoryName on Repository {
  intField1: forkCount
  stringField1: name
  stringField2: description
}";

            var repositoryName = new Fragment<Repository, TestModelObject>("repositoryName", repository => new TestModelObject
            {
                IntField1 = repository.ForkCount,
                StringField1 = repository.Name,
                StringField2 = repository.Description
            });

            var expression = new Query()
                .Select(q => new
                {
                    repo1 = q.Repository("foo", "bar").Select(repositoryName).SingleOrDefault(),
                    repo2 = q.Repository("foo", "bar").Select(repositoryName).SingleOrDefault()
                });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(2), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Issue_Select_Use_Simple_Fragment_With_Nodes_List()
        {
            var expected = @"query {
  repository(owner: ""foo"", name: ""bar"") {
    repos: issues {
      nodes {
        ...issueTitle
      }
    }
  }
}
fragment issueTitle on Issue {
  title
}";

            var fragment = new Fragment<Issue, string>("issueTitle", repo => repo.Title);

            var expression = new Query()
                .Repository("foo", "bar")
                .Select(org => new
                {
                    Repos = org.Issues(null, null, null, null, null)
                        .Nodes.Select(fragment).ToList(),
                });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(2), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Issue_Select_Use_Simple_Fragment_With_AllPages_List()
        {
            var masterQuery = @"query {
  repository(owner: ""foo"", name: ""bar"") {
    id
    repos: issues(first: 100) {
      pageInfo {
        hasNextPage
        endCursor
      }
      nodes {
        ...issueTitle
      }
    }
  }
}
fragment issueTitle on Issue {
  title
}";

            var subQuery = @"query($__id: ID!, $__after: String) {
  node(id: $__id) {
    __typename
    ... on Repository {
      issues(first: 100, after: $__after) {
        pageInfo {
          hasNextPage
          endCursor
        }
        nodes {
          ...issueTitle
        }
      }
    }
  }
}
fragment issueTitle on Issue {
  title
}";

            var fragment = new Fragment<Issue, string>("issueTitle", repo => repo.Title);

            var expression = new Query()
                .Repository("foo", "bar")
                .Select(org => new
                {
                    Repos = org.Issues(null, null, null, null, null)
                        .AllPages().Select(fragment).ToList(),
                });

            var compiledQuery = expression.Compile();

            Assert.Equal(masterQuery, compiledQuery.GetMasterQuery().ToString(2), ignoreLineEndingDifferences: true);

            Assert.Equal(subQuery, compiledQuery.GetSubqueries()[0].ToString(2), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Issue_Select_Two_In_Anon_Object()
        {
            var expected = @"query {
  repository(owner: ""foo"", name: ""bar"") {
    someData: issues(labels: [""asdf""]) {
      nodes {
        title
      }
    }
    someData2: issues(labels: [""asdf""]) {
      nodes {
        title
      }
    }
  }
}";

            Arg<IEnumerable<string>>? labels = new[] { "asdf" };

            var expression = new Query().Repository("foo", "bar").Select(repository => new
            {
                SomeData = repository.Issues(null, null, null, null, labels)
                    .Nodes
                    .Select(issue => new { issue.Title })
                    .ToList(),
                SomeData2 = repository.Issues(null, null, null, null, labels)
                    .Nodes
                    .Select(issue => new { issue.Title })
                    .ToList(),
            });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(2), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Issue_Select_Two_In_Object()
        {
            var expected = @"query {
  repository(owner: ""foo"", name: ""bar"") {
    someData: issues(labels: [""asdf""]) {
      nodes {
        title
      }
    }
    someData2: issues(labels: [""asdf""]) {
      nodes {
        title
      }
    }
  }
}";

            Arg<IEnumerable<string>>? labels = new[] { "asdf" };

            var expression = new Query().Repository("foo", "bar").Select(repository => new TestIssueSets
            {
                SomeData = repository.Issues(null, null, null, null, labels)
                    .Nodes
                    .Select(issue => new { issue.Title })
                    .ToList(),
                SomeData2 = repository.Issues(null, null, null, null, labels)
                    .Nodes
                    .Select(issue => new { issue.Title })
                    .ToList(),
            });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(2), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Union_IssueOrPullRequest()
        {
            var expected = @"query {
  repository(owner: ""foo"", name: ""bar"") {
    issueOrPullRequest(number: 1) {
      __typename
      ... on Issue {
        number
      }
      ... on PullRequest {
        title
      }
    }
  }
}";

            Arg<IEnumerable<string>>? labels = new[] { "asdf" };

            var expression = new Query()
                .Repository("foo", "bar")
                .IssueOrPullRequest(1)
                .Select(issueOrPr => issueOrPr.Switch<object>(when =>
                    when.Issue(issue => new IssueModel
                    {
                        Number = issue.Number,
                    }).PullRequest(pr => new PullRequestModel
                    {
                        Title = pr.Title,
                    })));

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(2), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Union_PullRequest_Timeline()
        {
            var expected = @"query {
  repository(owner: ""foo"", name: ""bar"") {
    pullRequest(number: 1) {
      timeline(first: 100) {
        nodes {
          __typename
          ... on Commit {
            oid: abbreviatedOid
          }
          ... on IssueComment {
            body
          }
        }
      }
    }
  }
}";

            var expression = new Query()
                .Repository("foo", "bar")
                .PullRequest(1)
                .Timeline(first: 100)
                .Nodes
                .Select(node => node.Switch<TimelineItemModel>(when =>
                    when.Commit(commit => new CommitModel
                    {
                        Oid = commit.AbbreviatedOid,
                    }).IssueComment(comment => new IssueCommentModel
                    {
                        Body = comment.Body,
                    })));

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(2), ignoreLineEndingDifferences: true);
        }

        class TestModelObject
        {
            public string StringField1;
            public string StringField2;
            public int IntField1;
            public int IntField2;
        }

        class IssueModel
        {
            public int Number { get; set; }
        }

        class PullRequestModel
        {
            public string Title { get; set; }
        }

        class TimelineItemModel
        {
        }

        class CommitModel : TimelineItemModel
        {
            public string Oid { get; set; }
        }

        class IssueCommentModel : TimelineItemModel
        {
            public string Body { get; set; }
        }

        public class TestIssueSets
        {
            public IReadOnlyList<object> SomeData { get; set; }
            public IReadOnlyList<object> SomeData2 { get; set; }
        }
    }
}
