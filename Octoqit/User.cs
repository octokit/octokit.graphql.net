using System;
using System.Linq;
using System.Linq.Expressions;
using LinqToGraphQL;

namespace Octoqit
{
    public class User : QueryEntity, ISearchResultItem
    {
        public User(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
        }

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
