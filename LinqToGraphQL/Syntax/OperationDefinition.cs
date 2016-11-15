using System;

namespace LinqToGraphQL.Syntax
{
    public class OperationDefinition : SelectionSet
    {
        public OperationDefinition(OperationType type, string name)
        {
            Type = type;
            Name = name;
        }

        public OperationType Type { get; }
        public string Name { get; }
    }
}
