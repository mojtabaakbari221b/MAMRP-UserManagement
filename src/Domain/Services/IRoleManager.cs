using Share.QueryFilterings;
using UserManagement.Domain.Filterings;

namespace UserManagement.Domain.Services;

public interface IRoleManager
{
    Task<OperationResult> AddRole(string roleName, string displayName);
    Task<OperationResult> RoleExistsAsync(string roleName);
    Task<OperationResult> RoleExistsAsync(Guid roleId);
    Task<OperationResult<RoleDto?>> GetRoleById(string id);
    Task<OperationResult> RemoveSectionClaimOfRoleAsync(Guid roleId);
    Task<OperationResult> AddSectionIdsToRoleClaimAsync(Guid roleId, IEnumerable<long> sectionIds);
    Task<OperationResult> Delete(Guid roleId);
    Task<OperationResult> Update(RoleDto roleDto);
    Task<OperationResult<IEnumerable<IResponse>>> GetAll(PaginationFilter pagination, RoleFiltering filtering, CancellationToken token = default);
}
