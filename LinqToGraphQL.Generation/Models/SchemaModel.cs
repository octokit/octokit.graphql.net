using System;
using System.Collections.Generic;

namespace LinqToGraphQL.Generation.Models
{
    public class SchemaModel
    {
        public IDictionary<string, TypeModel> Types { get; set; }
        public string QueryType { get; set; }
        public string MutationType { get; set; }
        public IList<DirectiveModel> Directives { get; set; }
    }
}
