namespace UserManagement.Domain.Services;

public interface IUserManager
{
    Task AddRoleAndTheirClaimsToUserAsync(UserDto userDto, RoleDto roleDto);
    Task<LoginResult> Login(string username, string password);
    Task RemoveUserRolesAndUserClaimsAsync(Guid userId);
    Task Register(RegisterDto registerDto);
    Task<UserDto> GetUserById(string id);
}