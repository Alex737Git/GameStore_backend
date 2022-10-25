namespace Service.Contracts
{
    public interface IServiceManager
    {
        IGameService GameService { get; }
        IAuthenticationService AuthenticationService { get; }
        IUserService UserService { get; }
        ICategoryService CategoryService { get; }
        ICommentService CommentService { get; }
    }
}