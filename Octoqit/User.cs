using System;
using LinqToGraphQL;

namespace Octoqit
{
    public class User : ISearchResultItem
    {
        public string Id { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }

        [GraphQLIdentifier("avatarURL")]
        public string AvatarUrl { get; }

        [GraphQLIdentifier("websiteURL")]
        public string WebsiteUrl { get; }
    }
}
