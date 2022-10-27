namespace Shared.DataTransferObjects.ForUpdate
{
    public class UserForUpdateRoleDto
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public string? RoleName { get; set; }
        
    }
}