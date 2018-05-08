using System;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;

namespace Octokit.GraphQL.Core
{
    public class Subquery
    {
        public ICompiledQuery Query { get; internal set; }
        public Expression<Func<JObject, JToken>> PageInfo { get; internal set; }
        public Expression<Func<JObject, JToken>> ParentPageInfo { get; internal set; }
    }
}
