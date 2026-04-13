namespace Octokit.GraphQL.Core.Deserializers
{
    public class RateLimitExceededException : ResponseDeserializerException
    {
        public RateLimitExceededException(string message, int line, int column)
            : base(message, line, column)
        {
        }

        public bool IsSecondary => Message?.IndexOf("secondary rate limit", System.StringComparison.OrdinalIgnoreCase) >= 0;
    }
}