using System;
using LinqToGraphQL;

namespace Octoqit
{
    public class User : ISearchResultItem
    {
        public string Id { get; }
        public string Login { get; }
        public string Name { get; }

        [GraphQLIdentifier("avatarURL")]
        public string AvatarUrl { get; }

        [GraphQLIdentifier("websiteURL")]
        public string WebsiteUrl { get; }
    }
}
