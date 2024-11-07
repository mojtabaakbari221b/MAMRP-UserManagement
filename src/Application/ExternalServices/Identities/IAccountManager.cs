namespace UserManagement.Application.ExternalServices.Identities;

public interface IAccountManager
{
    Task<LoginResult> Login(string username, string password);
    Task Register(RegisterDto registerDto);
    Task RemoveUserRolesAndUserClaimsAsync(Guid userID);
    Task<UserDto> GetUserById(string id);
    Task<RoleDto> GetRoleById(string id);
    Task AddRoleAndTheirClaimsToUserAsync(UserDto userDto, RoleDto roleDto);
    Task RemoveSectionClaimOfRoleAsync(RoleDto roleDto);
    Task AddSectionIdsToRoleClaimAsync(RoleDto roleDto, IEnumerable<long> sectionIds);
}