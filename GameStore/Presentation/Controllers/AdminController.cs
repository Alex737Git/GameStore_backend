using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.ForUpdate;

namespace Presentation.Controllers
{
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IServiceManager _service;

        public AdminController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet("allUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
         
            var users = await _service.AdminService.GetAllUsersAsync(false);
            return Ok(users);

        }
        
        [HttpGet("allRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
        
            var roles = await _service.AdminService.GetAllRolesAsync(false);
            return Ok(roles);
        }
        
        [HttpPut("editUserRole")]
        public async Task<IActionResult> EditUserRole( [FromBody] UserForUpdateRoleDto user)
        {
           await _service.AdminService.UpdateUserRoleAsync(user,true);
            
            return NoContent();

        }
        
        // [HttpDelete("user/{id:guid}")]
        // public async Task<IActionResult> DeleteUser(Guid id)
        // {
        //   
        //
        //     await _service.AdminService.DeleteUserAsync(id, false);
        //     
        //     return NoContent();
        //     
        // }

      
    }
}
