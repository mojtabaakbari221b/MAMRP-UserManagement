namespace UserManagement.Infrastructure.ExternalServices.Identities;

public sealed class AcountManager(
    SignInManager<User> signInManager,
    UserManager<User> userManager)
    : IAcountManager
{
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly UserManager<User> _userManager = userManager;

    public async Task<LoginResult> Login(string username, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);
        var user = await _userManager.FindByNameAsync(username);
        return new LoginResult(result.Succeeded, user!.Id);
    }

    public async Task Register(RegisterDto model)
    {
        User user = new()
        {
            Name = model.Username,
            FamilyName = model.FamilyName,
        };
        await _userManager.CreateAsync(user, model.Password);
    }
}