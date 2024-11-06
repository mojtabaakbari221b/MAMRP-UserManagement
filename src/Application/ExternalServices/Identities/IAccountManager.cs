namespace UserManagement.Application.ExternalServices.Identities;

public interface IAccountManager
{
    Task AddRoleAndTheirClaimsToUserAsync(UserDto userDto, RoleDto roleDto);
    Task<LoginResult> Login(string username, string password);
    Task<bool> AddRole(string roleName, string displayName);
    Task RemoveUserRolesAndUserClaimsAsync(Guid userId);
    Task Register(RegisterDto registerDto);
    Task<UserDto> GetUserById(string id);
    Task<RoleDto> GetRoleById(string id);
}