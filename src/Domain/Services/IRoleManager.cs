﻿using Share.QueryFilterings;
using UserManagement.Domain.Filterings;

namespace UserManagement.Domain.Services;

public interface IRoleManager
{
    Task AddRole(string roleName, string displayName);
    Task<bool> RoleExistsAsync(string roleName);
    Task<bool> RoleExistsAsync(Guid roleId);
    Task<RoleDto?> GetRoleById(string id);
    Task RemoveSectionClaimOfRoleAsync(Guid roleId);
    Task AddSectionIdsToRoleClaimAsync(Guid roleId, IEnumerable<long> sectionIds);
    Task Delete(Guid roleId);
    Task Update(RoleDto roleDto);
    Task<IEnumerable<IResponse>> GetAll(PaginationFilter pagination, RoleFiltering filtering, CancellationToken token = default);
}