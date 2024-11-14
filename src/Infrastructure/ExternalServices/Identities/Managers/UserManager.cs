namespace UserManagement.Infrastructure.ExternalServices.Identities.Managers;

public sealed class UserManager(
    SignInManager<User> signInManager,
    UserManager<User> userManager,
    UserManagementDbContext context,
    IOptions<TokenOption> options)
    : IUserManager
{
    private readonly BearerTokenOption _optionBearer = options.Value.BearerTokenOption;

    public async Task<OperationResult<LoginResult>> Login(string username, string password)
    {
        var result = await signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);
        var user = await userManager.FindByNameAsync(username);

        return result.Succeeded && user != null
            ? OperationResult<LoginResult>.Success(new LoginResult(true, user.Id))
            : OperationResult<LoginResult>.Failure(string.Empty, ErrorType.Failure);
    }

    public async Task<OperationResult<RegisterResult>> Register(RegisterDto registerDto)
    {
        User user = new()
        {
            UserName = registerDto.Username,
            FirstName = registerDto.FirstName,
            FamilyName = registerDto.FamilyName,
            Email = registerDto.Username + "@Mam.com",
        };

        var result = await userManager.CreateAsync(user, registerDto.Password);

        return result.Succeeded
            ? OperationResult<RegisterResult>.Success(new RegisterResult(true))
            : OperationResult<RegisterResult>.Failure(result.Errors.Select(e => e.Description).ToList(), ErrorType.Errors);
    }

    public async Task<OperationResult> RemoveUserRolesAndUserClaimsAsync(Guid userId)
    {
        var userRoleIds = await context.UserRoles
            .Where(u => u.UserId == userId)
            .Select(u => u.RoleId)
            .ToListAsync();

        var sectionIds = await context.RoleClaims
            .Where(rc => userRoleIds.Contains(rc.RoleId))
            .Select(rc => rc.SectionId)
            .ToListAsync();

        await context.UserRoles
            .Where(u => u.UserId == userId)
            .ExecuteUpdateAsync(s => s.SetProperty(ur => ur.IsActive, false));

        await context.UserClaims
            .Where(uc => uc.UserId == userId && sectionIds.Contains(uc.SectionId))
            .ExecuteUpdateAsync(s => s.SetProperty(uc => uc.IsActive, true));

        return OperationResult.Success();
    }

    public async Task<OperationResult<UserDto?>> GetUserById(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        return OperationResult<UserDto?>.Success(user?.Adapt<UserDto>());
    }

    public async Task<OperationResult> SaveToken(TokenDto tokens)
    {
        UserToken token = new()
        {
            Value = tokens.AccessToken,
            UserId = tokens.UserId,
        };

        context.UserTokens.Add(token);
        await context.SaveChangesAsync();

        return OperationResult.Success();
    }

    public async Task<OperationResult<bool>> AnyAsync(Guid userId, CancellationToken token = default)
    {
        var exists = await userManager.Users.AsQueryable()
            .AnyAsync(user => user.Id == userId, token);
        return OperationResult<bool>.Success(exists);
    }

    public async Task<OperationResult<bool>> AnyAsync(string userName, CancellationToken token = default)
    {
        var exists = await context.Users.AsQueryable()
            .AnyAsync(user => user.NormalizedUserName == userName.ToUpper(), token);
        return OperationResult<bool>.Success(exists);
    }

    public async Task<OperationResult> Delete(Guid userId, CancellationToken token)
    {
        var updateResult = await context.Users.AsQueryable()
            .Where(u => u.Id == userId)
            .ExecuteUpdateAsync(s => s.SetProperty(a => a.IsActive, false), token);

        return updateResult > 0
            ? OperationResult.Success()
            : OperationResult.Failure(ErrorType.Failure);
    }

    public async Task<OperationResult> Update(UserDto userDto, CancellationToken token = default)
    {
        var user = await userManager.Users.AsQueryable()
            .FirstOrDefaultAsync(user => user.Id == userDto.UserId, token);

        if (user is null)
            return OperationResult.Failure(ErrorType.NotFound);


        user.UserName = userDto.UserName;
        user.FirstName = userDto.FirstName;
        user.FamilyName = userDto.FamilyName;
        user.Email = userDto.UserName + "@Mam.com";

        await userManager.SetUserNameAsync(user, userDto.UserName);
        await userManager.SetEmailAsync(user, userDto.UserName + "@Mam.com");
        await userManager.UpdateAsync(user);

        return OperationResult.Success();
    }

    public async Task<OperationResult<IEnumerable<IResponse>>> GetAll(int pageNumber, int pageSize,
        CancellationToken token = default)
    {
        var users = await context.Users.AsQueryable()
            .Select(u => u.Adapt<GetUserQueryResponse>())
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);

        return OperationResult<IEnumerable<IResponse>>.Success(users);
    }

    public async Task<OperationResult> AddRoleAndTheirClaimsToUserAsync(UserDto userDto, RoleDto roleDto)
    {
        var user = userDto.Adapt<User>();
        var addRoleResult = await userManager.AddToRoleAsync(user, roleDto.Name);

        if (!addRoleResult.Succeeded)
            return OperationResult.Failure(ErrorType.Failure);

        var claims = await context.RoleClaims
            .Where(rc => rc.RoleId == roleDto.Id)
            .Select(rc => new Claim(rc.Section.Name, rc.Section.Url, ClaimValueTypes.String, _optionBearer.Issuer))
            .ToListAsync();

        var addClaimsResult = await userManager.AddClaimsAsync(user, claims);

        return addClaimsResult.Succeeded
            ? OperationResult.Success()
            : OperationResult.Failure(addClaimsResult.Errors.Select(e => e.Description).ToList(), ErrorType.Errors);
    }

    public async Task<OperationResult> RemoveSectionClaimOfUserAsync(Guid userId)
    {
        await context.UserClaims.Where(u => u.UserId == userId)
            .ExecuteUpdateAsync(s => s.SetProperty(uc => uc.IsActive, false));

        return OperationResult.Success();
    }

    public async Task<OperationResult> AddSectionIdsToUserClaimAsync(Guid userId, IEnumerable<long> sectionIds)
    {
        var userClaims = sectionIds.Select(sectionId => new UserClaim
        {
            UserId = userId,
            SectionId = sectionId
        }).ToList();

        await context.UserClaims.AddRangeAsync(userClaims);
        await context.SaveChangesAsync();

        return OperationResult.Success();
    }
}