using System;

namespace LinqToGraphQL.Syntax
{
    public class Argument : ISyntaxNode
    {
        public Argument(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public object Value { get; set; }
    }
}
