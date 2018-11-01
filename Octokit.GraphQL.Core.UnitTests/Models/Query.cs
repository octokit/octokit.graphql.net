using System;
using System.Collections.Generic;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class Query : QueryableValue<Query>, IQuery
    {
        public Query() : base(null)
        {
        }

        public IQueryableList<License> Licenses => this.CreateProperty(x => x.Licenses);
        public INode Node(Arg<ID> id) => this.CreateMethodCall(x => x.Node(id), Models.StubINode.Create);
        public IQueryableList<INode> Nodes(Arg<IEnumerable<ID>> ids) => this.CreateMethodCall(x => x.Nodes(ids));
        public RateLimit RateLimit(Arg<bool>? dryRun = null) => this.CreateMethodCall(x => x.RateLimit(dryRun), Models.RateLimit.Create);
        public Repository Repository(Arg<string> owner, Arg<string> name) => this.CreateMethodCall(x => x.Repository(owner, name), Models.Repository.Create);
        public IRepositoryOwner RepositoryOwner(Arg<string> login) => this.CreateMethodCall(x => x.RepositoryOwner(login), Models.StubIRepositoryOwner.Create);

        // Mutations here for convenience.
        public IssueComment AddComment(Arg<AddCommentInput> input) => this.CreateMethodCall(x => x.AddComment(input), Models.IssueComment.Create);

        // There don't seem to be any float args in the GitHub API.
        public Repository FloatTest(Arg<float> value) => this.CreateMethodCall(x => x.FloatTest(value), Models.Repository.Create);
    }
}
