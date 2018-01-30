using System;
using System.Collections.Generic;

namespace Octokit.GraphQL.Core.Syntax
{
    public class OperationDefinition : SelectionSet
    {
        public OperationDefinition(OperationType type, string name)
        {
            Type = type;
            Name = name;
            VariableDefinitions = new List<VariableDefinition>();
        }

        public OperationType Type { get; }
        public string Name { get; }
        public IList<VariableDefinition> VariableDefinitions { get; }
    }
}
