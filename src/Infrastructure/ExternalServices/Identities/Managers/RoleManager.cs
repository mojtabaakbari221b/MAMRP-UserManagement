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

    public async Task Delete(RoleDto roleDto)
    {
        var role = roleDto.Adapt<Role>();
        await _roleManager.DeleteAsync(role);
    }
}