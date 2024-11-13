namespace UserManagement.Domain.Services;

public interface IUserManager
{
    Task AddRoleAndTheirClaimsToUserAsync(UserDto userDto, RoleDto roleDto);
    Task<LoginResult> Login(string username, string password);
    Task<RegisterResult> Register(RegisterDto registerDto);
    Task RemoveUserRolesAndUserClaimsAsync(Guid userId);
    Task<UserDto?> GetUserById(string id);
    Task SaveToken(TokenDto tokens);
    Task RemoveSectionClaimOfUserAsync(Guid userId);
    Task AddSectionIdsToUserClaimAsync(Guid userId, IEnumerable<long> sectionIds);
    Task<bool> AnyAsync(Guid userId, CancellationToken token = default);
    Task<bool> AnyAsync(string userName, CancellationToken token = default);
    Task Delete(Guid userId, CancellationToken token);
    Task Update(UserDto userDto, CancellationToken token = default);
    Task<IEnumerable<IResponse>> GetAll(int pageNumber, int pageSize, CancellationToken token = default);
}