namespace Octokit.GraphQL.Core.Deserializers
{
    public class RateLimitExceededException : ResponseDeserializerException
    {
        public RateLimitExceededException(string message, int line, int column, string errorPayload)
            : base(message, line, column, errorPayload)
        {
        }

        public bool IsSecondary => Message?.IndexOf("secondary rate limit", System.StringComparison.OrdinalIgnoreCase) >= 0;
    }
}