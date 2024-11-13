using Share.QueryFilterings;
using UserManagement.Application.ApplicationServices.Roles.Queries.GetAll;
using UserManagement.Domain.Filterings;

namespace UserManagement.Infrastructure.ExternalServices.Identities.Managers;

public sealed class RoleManager(RoleManager<Role> roleManager, UserManagementDbContext context) : IRoleManager
{
    private readonly RoleManager<Role> _roleManager = roleManager;
    private readonly UserManagementDbContext _context = context;

    public async Task AddRole(string roleName, string displayName)
    {
        Role role = new()
        {
            Name = roleName,
            DisplayName = displayName
        };

        await _roleManager.CreateAsync(role);
    }

    public async Task<bool> RoleExistsAsync(string roleName)
        => await _roleManager.RoleExistsAsync(roleName);

    public async Task<bool> RoleExistsAsync(Guid roleId)
        => await _context.Roles.AsQueryable()
            .AnyAsync(role => role.Id == roleId);

    public async Task<RoleDto?> GetRoleById(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        return role?.Adapt<RoleDto>();
    }

    public async Task RemoveSectionClaimOfRoleAsync(Guid roleId)
    {
        await _context.RoleClaims.Where(u => u.RoleId == roleId).ExecuteDeleteAsync();
    }

    public async Task AddSectionIdsToRoleClaimAsync(Guid roleId, IEnumerable<long> sectionIds)
    {
        var roleClaims = sectionIds.Select(sectionId => new RoleClaim
        {
            RoleId = roleId,
            SectionId = sectionId
        }).ToList();

        await _context.RoleClaims.AddRangeAsync(roleClaims);
    }

    public async Task Delete(Guid roleId)
    {
        await _context.Roles.AsQueryable()
            .Where(role => role.Id == roleId)
            .ExecuteDeleteAsync();
    }

    public async Task Update(RoleDto roleDto)
    {
        var role = roleDto.Adapt<Role>();
        await _roleManager.UpdateAsync(role);
    }

    public async Task<IEnumerable<IResponse>> GetAll(PaginationFilter pagination, RoleFiltering filtering,
        CancellationToken token = default)
    {
        var query = _context.Roles.AsQueryable() ;

        query = QueryFilter.Filter(query, filtering);

        return await query.Select(r => r.Adapt<GetRoleQueryResponse>())
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync(token);
    }
}