using Share.Dtos;

namespace UserManagement.Domain.Services;

public interface IUserManager
{
    Task<OperationResult> AddRoleAndTheirClaimsToUserAsync(UserDto userDto, RoleDto roleDto);
    Task<OperationResult<LoginResult>> Login(string username, string password);
    Task<OperationResult<RegisterResult>> Register(RegisterDto registerDto);
    Task<OperationResult> RemoveUserRolesAndUserClaimsAsync(Guid userId);
    Task<OperationResult<UserDto?>> GetUserById(string id);
    Task<OperationResult> SaveToken(TokenDto tokens);
    Task<OperationResult> RemoveSectionClaimOfUserAsync(Guid userId);
    Task<OperationResult> AddSectionIdsToUserClaimAsync(Guid userId, IEnumerable<long> sectionIds);
    Task<OperationResult<bool>> AnyAsync(Guid userId, CancellationToken token = default);
    Task<OperationResult<bool>> AnyAsync(string userName, CancellationToken token = default);
    Task<OperationResult> Delete(Guid userId, CancellationToken token);
    Task<OperationResult> Update(UserDto userDto, CancellationToken token = default);

    Task<ListDto> GetAll(PaginationFilter pagination, UserFiltering filtering,
        UserOrdering ordering, CancellationToken token = default);
}