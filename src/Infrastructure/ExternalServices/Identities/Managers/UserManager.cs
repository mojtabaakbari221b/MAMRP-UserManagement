using Share.Dtos;
using Share.Ordering;
using UserManagement.Application.ApplicationServices.Account.Queries;
using UserManagement.Application.ApplicationServices.Account.Queries.GetAll;
using UserManagement.Domain.Orderings;

namespace UserManagement.Infrastructure.ExternalServices.Identities.Managers;

public sealed class UserManager(
    SignInManager<User> signInManager,
    UserManager<User> userManager,
    UserManagementDbContext context,
    IOptions<TokenOption> options)
    : IUserManager
{
    private readonly BearerTokenOption _optionBearer = options.Value.BearerTokenOption;
    private readonly UserManagementDbContext _context = context;
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;

    public async Task<OperationResult<LoginResult>> Login(string username, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);
        var user = await _userManager.FindByNameAsync(username);

        return result.Succeeded && user != null
            ? OperationResult<LoginResult>.Success(new LoginResult(true, user.Id))
            : OperationResult<LoginResult>.Failure(string.Empty, ErrorType.Failure);
    }

    public async Task<OperationResult<RegisterResult>> Register(RegisterDto registerDto)
    {
        User user = new()
        {
            UserName = registerDto.UserName,
            FirstName = registerDto.FirstName,
            FamilyName = registerDto.FamilyName,
            Email = registerDto.UserName + "@Mam.com",
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        return result.Succeeded
            ? OperationResult<RegisterResult>.Success(new RegisterResult(true))
            : OperationResult<RegisterResult>.Failure(result.Errors.Select(e => e.Description).ToList(),
                ErrorType.Errors);
    }

    public async Task<OperationResult> RemoveUserRolesAndUserClaimsAsync(Guid userId)
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
            .ExecuteUpdateAsync(s => s.SetProperty(ur => ur.IsActive, false));

        await _context.UserClaims
            .Where(uc => uc.UserId == userId && sectionIds.Contains(uc.SectionId))
            .ExecuteUpdateAsync(s => s.SetProperty(uc => uc.IsActive, true));

        return OperationResult.Success();
    }

    public async Task<OperationResult<UserDto?>> GetUserById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        return OperationResult<UserDto?>.Success(user?.Adapt<UserDto>());
    }

    public async Task<OperationResult> SaveToken(TokenDto tokens)
    {
        // UserToken token = new()
        // {
        //     Value = tokens.AccessToken,
        //     UserId = tokens.UserId,
        // };
        //
        // context.UserTokens.Add(token);
        // await context.SaveChangesAsync();

        return OperationResult.Success();
    }

    public async Task<OperationResult<bool>> AnyAsync(Guid userId, CancellationToken token = default)
    {
        var exists = await _userManager.Users.AsQueryable()
            .AnyAsync(user => user.Id == userId, token);
        return OperationResult<bool>.Success(exists);
    }

    public async Task<OperationResult<bool>> AnyAsync(string userName, CancellationToken token = default)
    {
        var exists = await _context.Users.AsQueryable()
            .AnyAsync(user => user.NormalizedUserName == userName.ToUpper(), token);
        return OperationResult<bool>.Success(exists);
    }

    public async Task<OperationResult> Delete(Guid userId, CancellationToken token)
    {
        var updateResult = await _context.Users.AsQueryable()
            .Where(u => u.Id == userId)
            .ExecuteUpdateAsync(s => s.SetProperty(a => a.IsActive, false), token);

        return updateResult > 0
            ? OperationResult.Success()
            : OperationResult.Failure(ErrorType.Failure);
    }

    public async Task<OperationResult> Update(UserDto userDto, CancellationToken token = default)
    {
        var user = await _userManager.Users.AsQueryable()
            .FirstOrDefaultAsync(user => user.Id == userDto.UserId, token);

        if (user is null)
            return OperationResult.Failure(ErrorType.NotFound);


        user.UserName = userDto.UserName;
        user.FirstName = userDto.FirstName;
        user.FamilyName = userDto.FamilyName;
        user.Email = userDto.UserName + "@Mam.com";

        await _userManager.SetUserNameAsync(user, userDto.UserName);
        await _userManager.SetEmailAsync(user, userDto.UserName + "@Mam.com");
        await _userManager.UpdateAsync(user);

        return OperationResult.Success();
    }

    public async Task<ListDto> GetAll(PaginationFilter pagination, UserFiltering filtering,
        UserOrdering ordering,
        CancellationToken token = default)
    {
        var query = _context.Users.AsQueryable();

        query = QueryFilter.Filter(query, filtering);

        query = QueryOrdering.ApplyOrdering(query, ordering);

        var count = await query.CountAsync(token);

        return new ListDto(
            count,
            await query.Select(c => c.Adapt<GetUserQueryResponse>())
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync(token)
        );
    }

    public async Task<OperationResult> AddRoleAndTheirClaimsToUserAsync(UserDto userDto, RoleDto roleDto)
    {
        var user = userDto.Adapt<User>();
        var addRoleResult = await _userManager.AddToRoleAsync(user, roleDto.Name);

        if (!addRoleResult.Succeeded)
            return OperationResult.Failure(ErrorType.Failure);

        var claims = await _context.RoleClaims
            .Where(rc => rc.RoleId == roleDto.Id)
            .Select(rc => new Claim(rc.Section.Name, rc.Section.Url, ClaimValueTypes.String, _optionBearer.Issuer))
            .ToListAsync();

        var addClaimsResult = await _userManager.AddClaimsAsync(user, claims);

        return addClaimsResult.Succeeded
            ? OperationResult.Success()
            : OperationResult.Failure(addClaimsResult.Errors.Select(e => e.Description).ToList(), ErrorType.Errors);
    }

    public async Task<OperationResult> RemoveSectionClaimOfUserAsync(Guid userId)
    {
        await _context.UserClaims.Where(u => u.UserId == userId)
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

        await _context.UserClaims.AddRangeAsync(userClaims);
        return OperationResult.Success();
    }
}