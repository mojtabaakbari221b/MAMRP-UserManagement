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
        await _context.RoleClaims.Where(rc => rc.RoleId == roleId)
            .ExecuteUpdateAsync(s => s.SetProperty(rc => rc.IsActive, false));
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
            .ExecuteUpdateAsync(s => s.SetProperty(r => r.IsActive, false));
    }

    public async Task Update(RoleDto roleDto)
    {
        var role = roleDto.Adapt<Role>();
        await _roleManager.UpdateAsync(role);
    }

    public async Task<IEnumerable<IResponse>> GetAll(int pageNumber, int pageSize, CancellationToken token = default)
        => await _context.Roles.AsQueryable()
            .Select(r => r.Adapt<GetRoleQueryResponse>())
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);
}