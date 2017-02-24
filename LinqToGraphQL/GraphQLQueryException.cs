using System;

namespace LinqToGraphQL
{
    public class GraphQLQueryException : Exception
    {
        public GraphQLQueryException(string message, int line, int column)
            : base(message)
        {
            Line = line;
            Column = column;
        }

        public int Line { get; }
        public int Column { get; }
    }
}
