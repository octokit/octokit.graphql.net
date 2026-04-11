using System.Linq;
using System.Threading.Tasks;
using Octokit.GraphQL.Core.Deserializers;
using Octokit.GraphQL.IntegrationTests.Utilities;
using Xunit;

namespace Octokit.GraphQL.IntegrationTests.Queries
{
    /// <summary>
    /// Failing tests that prove the two bugs triggered when GitHub's secondary rate limit
    /// is hit during heavy autopaging:
    ///
    ///   Bug 1 – HTTP 200 + RATE_LIMITED error body:
    ///     GitHub returns HTTP 200 with an "errors" array whose entry has no "locations" field.
    ///     ResponseDeserializer.DeserializeException unconditionally dereferences
    ///     error["locations"][0], causing a NullReferenceException instead of a
    ///     ResponseDeserializerException.
    ///
    ///   Bug 2 – HTTP 403 Forbidden:
    ///     Connection.Run calls EnsureSuccessStatusCode() with no retry or backoff, so a
    ///     secondary-rate-limit 403 immediately surfaces as an HttpRequestException and the
    ///     autopaging run is aborted.
    /// </summary>
    public class RateLimitBugTests : IntegrationTestBase
    {
        /// <summary>
        /// Proves Bug 1: ResponseDeserializer throws NullReferenceException (not
        /// ResponseDeserializerException) when the response contains a RATE_LIMITED error that
        /// has no "locations" field.
        ///
        /// This is the exact JSON shape GitHub sends for secondary-rate-limit errors returned as
        /// HTTP 200.  The test is deterministic – it does not make any network calls.
        ///
        /// Expected result once fixed: ResponseDeserializerException with a message that
        /// contains "rate limit".
        /// Current (buggy) result: NullReferenceException.
        /// </summary>
        [IntegrationTest]
        public void Deserializer_Throws_NullReferenceException_On_RATE_LIMITED_Error()
        {
            // This is the exact JSON body GitHub sends when the secondary rate limit is hit
            // via the GraphQL API (HTTP 200 + error payload, no "locations" key).
            const string rateLimitedResponseBody = @"{
  ""data"": null,
  ""errors"": [
    {
      ""type"": ""RATE_LIMITED"",
      ""message"": ""API rate limit exceeded for your GitHub account. See the documentation for more details.""
    }
  ]
}";
            var deserializer = new ResponseDeserializer();

            // The test asserts the CORRECT (post-fix) behaviour: a ResponseDeserializerException.
            // Until the bug is fixed, NullReferenceException is thrown instead, making this test
            // fail – which is intentional: the failure is the proof that the bug exists.
            var ex = Assert.Throws<ResponseDeserializerException>(
                () => deserializer.Deserialize(rateLimitedResponseBody));

            Assert.Contains("rate limit", ex.Message, System.StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Proves Bug 2: heavy nested autopaging triggers GitHub's secondary rate limit in CI,
        /// causing Connection.Run to throw an unhandled exception instead of retrying.
        ///
        /// The query fetches every issue in octokit/octokit.net (100 per page), every comment
        /// on each issue (10 per page), and every reaction on each comment.  This generates
        /// hundreds of API calls and reliably trips the secondary rate limit under CI load.
        ///
        /// Expected result once fixed: the query completes successfully.
        /// Current (buggy) result: HttpRequestException on HTTP 403, or NullReferenceException
        ///                         on an HTTP 200 RATE_LIMITED response.
        /// </summary>
        [IntegrationTest]
        public async Task Heavy_AutoPaging_Triggers_Secondary_Rate_Limit()
        {
            var query = new Query()
                .Repository(owner: "octokit", name: "octokit.net")
                .Issues().AllPages(100)
                .Select(issue => new
                {
                    issue.Id,
                    Comments = issue.Comments(null, null, null, null, null).AllPages(10).Select(comment => new
                    {
                        comment.Body,
                        Reactions = comment.Reactions(null, null, null, null, null, null)
                            .AllPages()
                            .Select(r => r.Id)
                            .ToList(),
                    }).ToList(),
                });

            // Should complete successfully once Connection.Run has retry/backoff for 403 and
            // ResponseDeserializer handles RATE_LIMITED errors without locations.
            var result = (await Connection.Run(query)).ToList();

            Assert.True(result.Count > 0);
            Assert.Contains(result, x => x.Comments.Count > 20);
        }
    }
}
