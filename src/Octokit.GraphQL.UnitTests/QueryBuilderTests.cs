using System;
using System.Collections.Generic;
using System.Linq;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Model;
using Xunit;

namespace Octokit.GraphQL.UnitTests
{
    public class QueryBuilderTests
    {
        [Fact]
        public void RepositoryOwner_Repository_Query()
        {
            var expected = @"query {
  repositoryOwner(login: ""foo"") {
    repository(name: ""bar"") {
      id
      name
      owner {
        login
      }
      isFork
      isPrivate
    }
  }
}";

            var expression = new Query()
                .RepositoryOwner("foo")
                .Repository("bar")
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    Owner = x.Owner.Select(o => new
                    {
                        o.Login
                    }).Single(),
                    x.IsFork,
                    x.IsPrivate,
                });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void RepositoryOwner_Repositories_Query()
        {
            var expected = @"query {
  repositoryOwner(login: ""foo"") {
    repositories(first: 30) {
      edges {
        node {
          id
          name
          owner {
            login
          }
          isFork
          isPrivate
        }
      }
    }
  }
}";

            var expression = new Query()
                .RepositoryOwner(login: "foo")
                .Repositories(first: 30)
                .Edges
                .Select(x => x.Node)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    Owner = x.Owner.Select(o => new
                    {
                        o.Login
                    }).Single(),
                    x.IsFork,
                    x.IsPrivate,
                });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void RepositoryOwner_Repositories_Query_Viewer()
        {
            var expected = @"query {
  repositoryOwner(login: ""foo"") {
    repositories(first: 30) {
      edges {
        node {
          id
          name
          owner {
            login
            avatarUrl
          }
          isFork
          isPrivate
        }
      }
    }
  }
  login: viewer {
    login
  }
  email: viewer {
    email
  }
}";

            var expression = new Query()
                .Select(root => root
                    .RepositoryOwner("foo")
                    .Repositories(30, null, null, null, null, null, null, null, null, null, null, null, null)
                    .Edges.Select(x => x.Node)
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        Owner = x.Owner.Select(o => new
                        {
                            o.Login,
                            AvatarUrl = o.AvatarUrl(null),
                        }).Single(),
                        x.IsFork,
                        x.IsPrivate,
                        Login = root.Viewer.Select(l => l.Login).Single(),
                        root.Viewer.Email
                    }));

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void User_Email_Query()
        {
            var expected = @"query {
  search(query: ""foo"", type: USER, first: 1) {
    userCount
    edges {
      node {
        __typename
        ... on User {
          id
          login
          avatarUrl
          websiteUrl
          name
        }
      }
    }
  }
}";

            var expression = new Query()
                .Search(first: 1, type: SearchType.User, query: "foo")
                .Select(x => new
                {
                    x.UserCount,
                    User = x.Edges.Select(e => e.Node).OfType<User>().Select(u => new
                    {
                        u.Id,
                        u.Login,
                        AvatarUrl = u.AvatarUrl(null),
                        u.WebsiteUrl,
                        u.Name,
                    }).ToList(),
                });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Viewer_Login_Email()
        {
            var expected = @"query {
  viewer {
    login
    email
  }
}";

            var expression = new Query().Viewer.Select(x => new { x.Login, x.Email });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Repository_Details_With_Viewer()
        {
            var expected = @"query {
  repositoryOwner(login: ""foo"") {
    repositories(first: 30) {
      edges {
        node {
          name
          isPrivate
        }
      }
    }
  }
  viewer {
    login
  }
}";

            var expression = new Query()
                .Select(x => x.RepositoryOwner("foo")
                              .Repositories(30, null, null, null, null, null, null, null, null, null, null, null, null)
                              .Edges
                              .Select(y => y.Node)
                              .Select(y => new
                              {
                                  y.Name,
                                  y.IsPrivate,
                                  Viewer = x.Viewer.Select(z => new
                                  {
                                      z.Login
                                  }).Single()
                              }));

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Search_User_Name()
        {
            var expected = @"query {
  search(query: ""foo"", type: USER, first: 30) {
    nodes {
      __typename
      ... on User {
        name
      }
    }
  }
}";

            var expression = new Query()
                .Search("foo", SearchType.User, 30)
                .Nodes
                .Select(x => x.Switch<string>(when =>
                    when.User(user => user.Name)));

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Search_User_Name_Login()
        {
            var expected = @"query {
  search(query: ""foo"", type: USER, first: 30) {
    nodes {
      __typename
      ... on User {
        name
        login
      }
    }
  }
}";

            var expression = new Query()
                .Search("foo", SearchType.User, 30)
                .Nodes
                .Select(x => x.Switch<object>(when =>
                    when.User(user => new
                    {
                        user.Name,
                        user.Login,
                    })));

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact(Skip = "Not yet working")]
        public void Search_User_Name_Via_Edges()
        {
            var expected = @"{
  search(query: ""foo"", type: USER, first: 30) {
    edges {
      node {
        ... on User {
          __typename
          name
        }
      }
    }
  }
}";

            var expression = new Query()
                .Search("foo", SearchType.User, 30)
                .Edges.Select(x => x.Node)
                .Select(x => x.Switch<string>(when =>
                    when.User(user => user.Name)));

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void DateTimeOffsetVariable()
        {
            var expected = @"query($start: DateTime) {
  user(login: ""grokys"") {
    contributionsCollection(from: $start) {
      latestRestrictedContributionDate
    }
  }
}";

            var expression = new Query()
                             .User("grokys")
                             .ContributionsCollection(@from: Variable.Var("start"))
                             .Select(c => c.LatestRestrictedContributionDate);

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(), ignoreLineEndingDifferences: true);
        }
        
        
        [Fact]
        public void TestAllPagesSubqueryUsesCorrectEntityName()
        {
            var expectedMasterQuery = @"query {
  repositoryOwner(login: ""foo"") {
    id
    repositories(first: 100) {
      pageInfo {
        hasNextPage
        endCursor
      }
      nodes {
        name
      }
    }
  }
}";
            
            // The actual type that we map RepositoryOwner to is called StubIRepositoryOwner
            // So make sure it gets serialized to the query correctly. 
            var expectedSubQuery = @"query($__id: ID!, $__after: String) {
  node(id: $__id) {
    __typename
    ... on RepositoryOwner {
      repositories(first: 100, after: $__after) {
        pageInfo {
          hasNextPage
          endCursor
        }
        nodes {
          name
        }
      }
    }
  }
}";
            
            var query = new Query()
                .RepositoryOwner("foo")
                .Repositories()
                .AllPages(100)
                .Select(x => x.Name)
                .Compile();
            
            var subQuery = (query as PagedQuery<IEnumerable<string>>).Subqueries.First();
            
            Assert.Equal(expectedMasterQuery, query.ToString(), ignoreLineEndingDifferences: true);
            Assert.Equal(expectedSubQuery, subQuery.ToString(), ignoreLineEndingDifferences: true);


        }

        [Fact]
        public void CreateRepositoryRuleset_Mutation_Should_Not_Include_Null_Fields_In_Parameters()
        {
            // This test demonstrates issue #320: When creating a repository ruleset with
            // RuleParametersInput that has only one field set, the mutation should NOT
            // serialize all the other null fields because GitHub API spec says
            // "Only one rule parameter type can be specified."
            //
            // Expected: Only the non-null parameter field (requiredStatusChecks) should appear
            // Actual (current bug): All parameter fields appear with most being null

            var expected = @"mutation {
  createRepositoryRuleset(input: {
    sourceId: ""test-id""
    name: ""main""
    target: BRANCH
    rules: [{
      type: REQUIRED_STATUS_CHECKS
      parameters: {
        requiredStatusChecks: {
          requiredStatusChecks: [{context: ""ng test""}, {context: ""ng lint""}]
          strictRequiredStatusChecksPolicy: true
        }
      }
    }]
    conditions: {
      refName: {
        exclude: []
        include: [""~DEFAULT_BRANCH""]
      }
    }
    enforcement: ACTIVE
  }) {
    ruleset {
      id
    }
  }
}";

            var mutation = new Mutation()
                .CreateRepositoryRuleset(new CreateRepositoryRulesetInput
                {
                    SourceId = new ID("test-id"),
                    Name = "main",
                    Target = RepositoryRulesetTarget.Branch,
                    Rules = new[]
                    {
                        new RepositoryRuleInput
                        {
                            Type = RepositoryRuleType.RequiredStatusChecks,
                            Parameters = new RuleParametersInput
                            {
                                // Only one field is set - all others should NOT be serialized
                                RequiredStatusChecks = new RequiredStatusChecksParametersInput
                                {
                                    RequiredStatusChecks = new[]
                                    {
                                        new StatusCheckConfigurationInput { Context = "ng test" },
                                        new StatusCheckConfigurationInput { Context = "ng lint" }
                                    },
                                    StrictRequiredStatusChecksPolicy = true
                                }
                                // These fields are null and should NOT appear in the output:
                                // Update, RequiredDeployments, PullRequest, CommitMessagePattern,
                                // CommitAuthorEmailPattern, CommitterEmailPattern, BranchNamePattern,
                                // TagNamePattern, Workflows
                            }
                        }
                    },
                    Conditions = new RepositoryRuleConditionsInput
                    {
                        RefName = new RefNameConditionTargetInput
                        {
                            Include = new[] { "~DEFAULT_BRANCH" },
                            Exclude = new string[] { }
                        }
                    },
                    Enforcement = RuleEnforcement.Active
                })
                .Select(x => new
                {
                    Ruleset = x.Ruleset.Select(r => new
                    {
                        r.Id
                    }).Single()
                });

            var query = mutation.Compile();

            // This assertion will FAIL with the current implementation because
            // the actual output includes all the null fields in parameters, like:
            // parameters: {
            //   update: null
            //   requiredDeployments: null
            //   pullRequest: null
            //   requiredStatusChecks: { ... }
            //   commitMessagePattern: null
            //   ...
            // }
            Assert.Equal(expected, query.ToString(), ignoreLineEndingDifferences: true);
        }
    }
}
