using System.Threading.Tasks;

namespace Octokit.GraphQL.Internal
{
    public class InMemoryCredentialStore : ICredentialStore
    {
        readonly string token;
        public InMemoryCredentialStore(string token) => this.token = token;
        public Task<string> GetCredentials() => Task.FromResult(token);
    }
}