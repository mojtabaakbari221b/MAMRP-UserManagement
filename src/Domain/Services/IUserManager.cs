namespace UserManagement.Domain.Services;

public interface IUserManager
{
    Task AddRoleAndTheirClaimsToUserAsync(UserDto userDto, RoleDto roleDto);
    Task<LoginResult> Login(string username, string password);
    Task<RegisterLogin> Register(RegisterDto registerDto);
    Task RemoveUserRolesAndUserClaimsAsync(Guid userId);
    Task<UserDto?> GetUserById(string id);
    Task SaveToken(TokenDto tokens);
}