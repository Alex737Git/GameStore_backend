using AutoMapper;
using AWSCloudService;
using Contracts;
using Entities.Models;
using LoggingService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IGameService> _gameService;
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<ICommentService> _commentService;
        private readonly Lazy<IAdminService> _adminService;

        private readonly Lazy<IUserService> _userService;

        public ServiceManager(IRepositoryManager repositoryManager,
            ILoggerManager logger,
            IMapper mapper,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IAwsServices aws
        )
        {
            _gameService =
                new Lazy<IGameService>(() => new GameService(logger, repositoryManager, mapper, userManager));
            _categoryService = new Lazy<ICategoryService>(() => new CategoryService(logger, repositoryManager, mapper));
            _commentService =
                new Lazy<ICommentService>(() => new CommentService(logger, repositoryManager, mapper, userManager));
            _authenticationService = new Lazy<IAuthenticationService>(() =>
                new AuthenticationService(logger, mapper, userManager, configuration));
            _userService = new Lazy<IUserService>(() => new UserService(mapper, userManager, configuration, aws));
            _adminService = new Lazy<IAdminService>(() => new AdminService(userManager, configuration,roleManager,mapper,repositoryManager ));
        }

        public IAuthenticationService AuthenticationService => _authenticationService.Value;

        public IGameService GameService => _gameService.Value;

        public ICommentService CommentService => _commentService.Value;
        public ICategoryService CategoryService => _categoryService.Value;
        public IUserService UserService => _userService.Value;
        public IAdminService AdminService => _adminService.Value;
    }
}