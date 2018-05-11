namespace PagingDemo.Models
{
    class AuthorModel
    {
        public AuthorModel(string login, string avatarUrl)
        {
            Login = login;
            AvatarUrl = avatarUrl;
        }

        public string Login { get; }
        public string AvatarUrl { get; }
    }
}