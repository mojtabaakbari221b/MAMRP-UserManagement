namespace UserManagement.Infrastructure.ExternalServices.Identities;

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

        var result = await _roleManager.CreateAsync(role);
    }

    public async Task<bool> RoleExistsAsync(string roleName)
    {
        return await _roleManager.RoleExistsAsync(roleName);
    }

    //TODO: "Business logic should not reside in the infrastructure layer.",
    //TODO: Move the exceptions to the Application layer
    public async Task<RoleDto> GetRoleById(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        return role.Adapt<RoleDto>();
    }

    public async Task RemoveSectionClaimOfRoleAsync(Guid roleId)
    {
        await _context.RoleClaims.Where(u => u.RoleId == roleId).ExecuteDeleteAsync();
    }

    public async Task AddSectionIdsToRoleClaimAsync(Guid roleId, IEnumerable<long> sectionIds)
    {
        List<RoleClaim> roleClaims = [];

        foreach (var sectionId in sectionIds)
        {
            RoleClaim roleClaim = new()
            {
                RoleId = roleId,
                SectionId = sectionId,
            };
            roleClaims.Append(roleClaim);
        }

        await _context.RoleClaims.AddRangeAsync(roleClaims);
    }

    public async Task Delete(RoleDto roleDto)
    {
        var role = roleDto.Adapt<Role>();
        await _roleManager.DeleteAsync(role);
    }
}