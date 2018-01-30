using System;
using System.Collections.Generic;
using System.Text;

namespace Octokit.GraphQL.Core.Syntax
{
    public class VariableDefinition
    {
        public VariableDefinition(Type type, string name)
        {
            Type = type;
            Name = name;
        }

        public Type Type { get; }
        public string Name { get; }
    }
}
