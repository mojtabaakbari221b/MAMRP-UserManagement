namespace UserManagement.Domain.Services;

public interface IRoleManager
{
    Task AddRole(string roleName, string displayName);
    Task<bool> RoleExistsAsync(string roleName);
    Task<RoleDto?> GetRoleById(string id);
    Task RemoveSectionClaimOfRoleAsync(Guid roleId);
    Task AddSectionIdsToRoleClaimAsync(Guid roleId, IEnumerable<long> sectionIds);
    Task Delete(RoleDto roleDto);
}