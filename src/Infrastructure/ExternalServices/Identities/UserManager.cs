using UserManagement.Domain.Services;
using UserManagement.Domain.Services.DTOs;

namespace UserManagement.Infrastructure.ExternalServices.Identities;

public sealed class UserManager(
    SignInManager<User> signInManager,
    UserManager<User> userManager,
    UserManagementDbContext context,
    IOptions<TokenOption> options) : IUserManager
{
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly UserManager<User> _userManager = userManager;
    private readonly UserManagementDbContext _context = context;
    private readonly BearerTokenOption _optionBearer = options.Value.BearerTokenOption;

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

    public async Task RemoveUserRolesAndUserClaimsAsync(Guid userId)
    {
        var userRoleIds = _context.UserRoles
            .Where(u => u.UserId == userId)
            .Select(u => u.RoleId);

        var sectionIds = _context.RoleClaims
            .Where(u => userRoleIds.Contains(u.RoleId))
            .Select(u => u.SectionId);

        await _context.UserRoles.Where(u => u.UserId == userId).ExecuteDeleteAsync();

        await _context.UserClaims
            .Where(u => u.UserId == userId && sectionIds.Contains(u.SectionId))
            .ExecuteDeleteAsync();
    }
    
    //TODO: "Business logic should not reside in the infrastructure layer.",
    //TODO: Move the exceptions to the Application layer 
    public async Task<UserDto> GetUserById(string id)
    {
        var user = await _userManager.FindByIdAsync(id)
                   ?? throw new UserNotFoundException();

        return user.Adapt<UserDto>();
    }

    // Refactor
    public async Task AddRoleAndTheirClaimsToUserAsync(UserDto userDto, RoleDto roleDto)
    {
        var user = userDto.Adapt<User>();
        await _userManager.AddToRoleAsync(user, roleDto.Name);

        var claims = await _context.RoleClaims
            .Where(rc => rc.RoleId == roleDto.Id)
            .Select(rc => new Claim(rc.Section.Name, rc.Section.Url, ClaimValueTypes.String, _optionBearer.Issuer))
            .ToListAsync();

        await _userManager.AddClaimsAsync(user, claims);
    }
}