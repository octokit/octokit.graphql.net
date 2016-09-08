using System;

namespace Octoqit
{
    public class User : ISearchResultItem
    {
        public string Id { get; }
        public string Login { get; }
        public string Name { get; }
        public string AvatarUrl { get; }
        public string WebsiteUrl { get; }
    }
}
