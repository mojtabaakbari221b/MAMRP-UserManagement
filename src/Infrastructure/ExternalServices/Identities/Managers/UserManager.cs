namespace UserManagement.Infrastructure.ExternalServices.Identities.Managers;

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
        return !result.Succeeded
            ? new LoginResult(result.Succeeded, Guid.Empty)
            : new LoginResult(result.Succeeded, user!.Id);
    }

    public async Task<RegisterResult> Register(RegisterDto registerDto)
    {
        User user = new()
        {
            UserName = registerDto.Username,
            FirstName = registerDto.FirstName,
            FamilyName = registerDto.FamilyName,
            Email = registerDto.Username + "@Mam.com",
        };
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        return new RegisterResult(result.Succeeded);
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

    public async Task SaveToken(TokenDto tokens)
    {
        UserToken token = new()
        {
            Value = tokens.AccessToken,
            UserId = tokens.UserId,
        };
        // UserRefreshToken refreshToken = new()
        // {
        //     UserId = tokens.UserId,
        //     to
        // };
        _context.UserTokens.Add(token);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AnyAsync(Guid userId, CancellationToken token = default)
        => await _userManager.Users.AsQueryable()
            .AnyAsync(user => user.Id == userId, token);

    public async Task<bool> AnyAsync(string userName, CancellationToken token = default)
    {
        return await _context.Users.AsQueryable()
            .AnyAsync(user => user.NormalizedUserName == userName.ToUpper(), token);
    }

    public async Task Delete(Guid userId, CancellationToken token)
    {
        //TODO: Add soft delete (ExcuteUpdate)
        await _context.Users.AsQueryable()
            .Where(u => u.Id == userId)
            .ExecuteDeleteAsync(token);
    }

    public async Task Update(UserDto userDto, CancellationToken token = default)
    {
        var user = await _userManager.Users.AsQueryable()
            .FirstAsync(user => user.Id == userDto.UserId, token);
        
        user.UserName = user.UserName;
        user.FirstName = userDto.FirstName;
        user.FamilyName = userDto.FamilyName;
        user.Email = userDto.UserName  + "@Mam.com";
        
        await _userManager.SetUserNameAsync(user, userDto.UserName);
        await _userManager.SetEmailAsync(user, userDto.UserName + "@Mam.com");
        await _userManager.UpdateAsync(user);
    }

    public async Task<IEnumerable<IResponse>> GetAll(int pageNumber, int pageSize, CancellationToken token = default)
        => await _context.Users.AsQueryable()
            .Select(u => u.Adapt<GetUserQueryResponse>())
            .ToListAsync(token);

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
    
    public async Task RemoveSectionClaimOfUserAsync(Guid userId)
    {
        await _context.UserClaims.Where(u => u.UserId == userId).ExecuteDeleteAsync();
    }
    
    public async Task AddSectionIdsToUserClaimAsync(Guid userId, IEnumerable<long> sectionIds)
    {
        var userClaims = sectionIds.Select(sectionId => new UserClaim()
        {
            UserId = userId,
            SectionId = sectionId
        }).ToList();

        await _context.UserClaims.AddRangeAsync(userClaims);
    }
}