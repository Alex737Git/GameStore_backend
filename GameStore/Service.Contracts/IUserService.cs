using Microsoft.AspNetCore.Http;

namespace Service.Contracts
{
    public interface IUserService
    {
        // Task<IdentityResult> UpdateUserAsync(string name ,UserForUpdateRoleDto userForUpdate, bool trackChanges);
        // Task<UserForUpdateMe> GetUser(string name, bool trackChanges);
       Task<string> UploadPhoto(IFormFile file);
       string GetValueFromAppJson(string section, string value);
       Task UpdateUserAvatar(string name, string avatarUrl);
    }
}