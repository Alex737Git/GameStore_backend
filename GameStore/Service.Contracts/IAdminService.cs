using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForShow;
using Shared.DataTransferObjects.ForUpdate;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IAdminService
{
     Task<IEnumerable<RoleDto>> GetAllRolesAsync( bool trackChanges);
     Task<IEnumerable<UserDto>> GetAllUsersAsync( bool trackChanges);
    
    // Task DeleteUserAsync(Guid userId, bool trackChanges);
     Task UpdateUserRoleAsync( UserForUpdateRoleDto userForUpdateRole, bool trackChanges);
    
    
    
}