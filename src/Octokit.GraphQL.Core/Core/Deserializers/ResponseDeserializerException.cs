namespace Octokit.GraphQL.Core.Deserializers
{
    public class ResponseDeserializerException : GraphQLException
    {
        public ResponseDeserializerException(string message, int line, int column, string errorPayload)
            : base(message)
        {
            Line = line;
            Column = column;
            ErrorPayload = errorPayload;
        }

        public int Line { get; }
        public int Column { get; }
        public string ErrorPayload { get; }
    }
}
