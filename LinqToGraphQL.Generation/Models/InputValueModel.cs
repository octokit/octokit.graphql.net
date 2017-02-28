using System;

namespace LinqToGraphQL.Generation.Models
{
    public class InputValueModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TypeModel Type { get; set; }
        public string DefaultValue { get; set; }
    }
}
