using System;
using System.Collections.Generic;
using LinqToGraphQL.Introspection;

namespace LinqToGraphQL.Generation.Models
{
    public class DirectiveModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<DirectiveLocation> Locations { get; set; }
        public IList<InputValueModel> Args { get; set; }
    }
}
