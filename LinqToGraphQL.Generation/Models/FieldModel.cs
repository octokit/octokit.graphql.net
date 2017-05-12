using System;
using System.Collections.Generic;

namespace Octokit.GraphQL.Core.Generation.Models
{
    public class FieldModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<InputValueModel> Args { get; set; }
        public TypeModel Type { get; set; }
        public bool IsDeprecated { get; set; }
        public string DeprecationReason { get; set; }
    }
}
