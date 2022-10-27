using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using Shared.DataTransferObjects.ForShow;
using Shared.DataTransferObjects.ForUpdate;

namespace Service
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repository;


        public AdminService(UserManager<User> userManager, IConfiguration configuration,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper, IRepositoryManager repository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync(bool trackChanges)
        {
            var res = await _roleManager.Roles.ToListAsync();

            return _mapper.Map<IEnumerable<RoleDto>>(res);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync(bool trackChanges)
        {
            var result = await _userManager.Users.ToListAsync();


            var userDto = _mapper.Map<IEnumerable<UserDto>>(result);
            int number = 0;
            foreach (var u in userDto)
            {
                u.Role = (await _userManager.GetRolesAsync(result[number])).FirstOrDefault();
                number++;
            }

            return userDto;
        }

        // public async Task DeleteUserAsync(Guid userId, bool trackChanges)
        // {
        //     var user = await _userManager.FindByIdAsync(userId.ToString());
        //     if (user != null)
        //     {
        //         var comments = await _repository.Comment.GetAllCommentsByUser(user.Id);
        //
        //         foreach (var comment in comments)
        //         {
        //             _repository.Comment.DeleteComment(comment);
        //         }
        //
        //         await _userManager.DeleteAsync(user);
        //     }
        // }

        public async Task UpdateUserRoleAsync(UserForUpdateRoleDto userForUpdateRole, bool trackChanges)
        {
            User user = await _userManager.FindByIdAsync(userForUpdateRole.UserId.ToString());
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);

                await _userManager.AddToRoleAsync(user, userForUpdateRole.RoleName);
                await _userManager.RemoveFromRoleAsync(user, roles[0]);
            }
        }
    }
}