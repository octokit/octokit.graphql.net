using System;

namespace LinqToGraphQL.Syntax
{
    public class Argument : ISyntaxNode
    {
        public Argument(FieldSelection parent, string name)
        {
            Name = name;
            parent.Arguments.Add(this);
        }

        public string Name { get; }
        public object Value { get; set; }
    }
}
