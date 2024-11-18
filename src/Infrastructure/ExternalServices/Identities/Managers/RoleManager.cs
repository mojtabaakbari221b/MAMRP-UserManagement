using Share.Dtos;

namespace UserManagement.Infrastructure.ExternalServices.Identities.Managers;

public sealed class RoleManager(RoleManager<Role> roleManager, UserManagementDbContext context)
    : IRoleManager
{
    public async Task<OperationResult> AddRole(string roleName, string displayName)
    {
        Role role = new()
        {
            Name = roleName,
            DisplayName = displayName
        };

        var result = await roleManager.CreateAsync(role);
        return result.Succeeded
            ? OperationResult.Success()
            : OperationResult.Failure(result.Errors.Select(e => e.Description).ToList(), ErrorType.Errors);
    }

    public async Task<OperationResult> RoleExistsAsync(string roleName)
    {
        var exists = await roleManager.RoleExistsAsync(roleName);
        return exists
            ? OperationResult.Success()
            : OperationResult.Failure(ErrorType.NotFound);
    }

    public async Task<OperationResult> RoleExistsAsync(Guid roleId)
    {
        var exists = await context.Roles.AsQueryable()
            .AnyAsync(role => role.Id == roleId);

        return exists ? OperationResult.Success() : OperationResult.Failure(ErrorType.NotFound);
    }

    public async Task<OperationResult<RoleDto?>> GetRoleById(string id)
    {
        var role = await roleManager.FindByIdAsync(id);
        var roleDto = role?.Adapt<RoleDto>();
        return OperationResult<RoleDto?>.Success(roleDto);
    }

    public async Task<OperationResult> RemoveSectionClaimOfRoleAsync(Guid roleId)
    {
        var updateResult = await context.RoleClaims
            .Where(rc => rc.RoleId == roleId)
            .ExecuteUpdateAsync(s => s.SetProperty(rc => rc.IsActive, false));

        return updateResult > 0
            ? OperationResult.Success()
            : OperationResult.Failure(ErrorType.Failure);
    }

    public async Task<OperationResult> AddSectionIdsToRoleClaimAsync(Guid roleId, IEnumerable<long> sectionIds)
    {
        var roleClaims = sectionIds.Select(sectionId => new RoleClaim
        {
            RoleId = roleId,
            SectionId = sectionId
        }).ToList();

        await context.RoleClaims.AddRangeAsync(roleClaims);
        return OperationResult.Success();
    }

    public async Task<OperationResult> Delete(Guid roleId)
    {
        var updateResult = await context.Roles.AsQueryable()
            .Where(role => role.Id == roleId)
            .ExecuteUpdateAsync(s => s.SetProperty(r => r.IsActive, false));

        return updateResult > 0
            ? OperationResult.Success()
            : OperationResult.Failure(ErrorType.NotFound);
    }

    public async Task<OperationResult> Update(RoleDto roleDto)
    {
        var role = roleDto.Adapt<Role>();
        var result = await roleManager.UpdateAsync(role);

        return result.Succeeded
            ? OperationResult.Success()
            : OperationResult.Failure(result.Errors.Select(e => e.Description).ToList(), ErrorType.Errors);
    }

    public async Task<ListDto> GetAll(PaginationFilter pagination,
        RoleFiltering? filtering,
        RoleOrdering? ordering,
        CancellationToken token = default)
    {
        var query = context.Roles.AsQueryable();

        query = QueryFilter.Filter(query, filtering);

        query = QueryOrdering.ApplyOrdering(query, ordering);

        var count = await query.CountAsync(token);

        var roles = await query
            .Select(r => r.Adapt<GetRoleQueryResponse>())
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync(token);

        return new ListDto(
            count,
            await query.Select(c => c.Adapt<GetRoleQueryResponse>())
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync(token)
        );
    }

    public async Task<OperationResult<IEnumerable<IResponse>>> GetAll(PaginationFilter pagination,
        RoleFiltering? filtering, CancellationToken token = default)
    {
        var query = context.Roles.AsQueryable();

        query = QueryFilter.Filter(query, filtering);

        var roles = await query
            .Select(r => r.Adapt<GetRoleQueryResponse>())
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync(token);

        return OperationResult<IEnumerable<IResponse>>.Success(roles);
    }
}