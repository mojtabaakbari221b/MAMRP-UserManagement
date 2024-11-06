using Mapster;
using UserManagement.Infrastructure.ExternalServices.Identities.Exceptions;

namespace UserManagement.Infrastructure.ExternalServices.Identities;

public sealed class AcountManager(
    SignInManager<User> signInManager,
    UserManager<User> userManager,
    UserManagementDbContext context)
    : IAcountManager
{
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly UserManager<User> _userManager = userManager;
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

    public async Task<UserDto> GetById(string id) {
        var user = await _userManager.FindByIdAsync(id) ?? throw new UserNotFoundException();
        var userDto = user.Adapt<UserDto>();
        return userDto;
    }
}