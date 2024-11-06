using Mapster;
using UserManagement.Infrastructure.ExternalServices.Identities.Exceptions;

namespace UserManagement.Infrastructure.ExternalServices.Identities;

public sealed class AccountManager(
    SignInManager<User> signInManager,
    UserManager<User> userManager,
    UserManagementDbContext context,
    RoleManager<Role> roleManager)
    : IAccountManager
{
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly UserManager<User> _userManager = userManager;
    private readonly RoleManager<Role> _roleManager = roleManager;
    private readonly UserManagementDbContext _context = context;
    public async Task<LoginResult> Login(string username, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);
        var user = await _userManager.FindByNameAsync(username);
        return new LoginResult(result.Succeeded, user!.Id);
    }

    public async Task Register(RegisterDto registerDto)
    {
        User user = new()
        {
            Name = registerDto.Username,
            FamilyName = registerDto.FamilyName,
        };
        await _userManager.CreateAsync(user, registerDto.Password);
    }

    public async Task RemoveUserRolesAndUserClaimsAsync(Guid userID)
    {
        var userRoleIds = _context.UserRoles.Where(u => u.UserId == userID).Select(u => u.RoleId);
        var sectionIds = _context.RoleClaims.Where(u => userRoleIds.Contains(u.RoleId)).Select(u => u.SectionId);

        await _context.UserRoles.Where(u => u.UserId == userID).ExecuteDeleteAsync();

        _context.UserClaims.Where(ur => ur.UserId == userID).Where(u => sectionIds.Contains(u.SectionId));   
    }

    public async Task<UserDto> GetUserById(string id) {
        var user = await _userManager.FindByIdAsync(id) ?? throw new UserNotFoundException();
        var userDto = user.Adapt<UserDto>();
        return userDto;
    }

    public async Task<RoleDto> GetRoleById(string id) {
        var role = await _roleManager.FindByIdAsync(id) ?? throw new RoleNotFoundException();
        var roleDto = role.Adapt<RoleDto>();
        return roleDto;
    }

    public async Task AddRoleAndTheirClaimsToUserAsync(UserDto userDto, RoleDto roleDto)
    {
        var user = userDto.Adapt<User>();
        var role = roleDto.Adapt<Role>();
        await _userManager.AddToRoleAsync(user, role.Name);

        var claims = await _context.RoleClaims.Where(rc => rc.RoleId == roleDto.Id).ToListAsync();
        
        await _userManager.AddClaimsAsync(user, (IEnumerable<Claim>)claims);
    }
}