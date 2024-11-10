﻿namespace UserManagement.Infrastructure.ExternalServices.Identities;

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
        var userRoleIds = await _context.UserRoles
            .Where(u => u.UserId == userId)
            .Select(u => u.RoleId)
            .ToListAsync();

        var sectionIds = await _context.RoleClaims
            .Where(rc => userRoleIds.Contains(rc.RoleId))
            .Select(rc => rc.SectionId)
            .ToListAsync();

        await _context.UserRoles
            .Where(u => u.UserId == userId)
            .ExecuteDeleteAsync();

        await _context.UserClaims
            .Where(uc => uc.UserId == userId && sectionIds.Contains(uc.SectionId))
            .ExecuteDeleteAsync();
    }

    public async Task<UserDto?> GetUserById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        return user?.Adapt<UserDto>();
    }

    public Task SaveToken(TokenDto tokens)
    {
        UserToken token = new()
        {
            t = tokens.AccessToken,
        };
        _userManager.
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