namespace MyClassroom.MVC.Github
{
    public static class GithubDefaults
    {
        public const string AuthenticationScheme = "Github";
        public static readonly string DisplayName = "Github";
        public static readonly string AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
        public static readonly string TokenEndpoint = "https://github.com/login/oauth/access_token";
        public static readonly string UserInformationEndpoint = "https://api.github.com/user";
        public static readonly int RefreshTokenExpireIn = 15811200;
    }
}
