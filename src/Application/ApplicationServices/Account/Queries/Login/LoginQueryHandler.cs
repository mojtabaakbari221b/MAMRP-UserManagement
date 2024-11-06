namespace UserManagement.Application.ApplicationServices.Account.Queries.Login;

public sealed class LoginQueryHandler(IAccountManager accountManager, ITokenFactory tokenFactory)
    : IRequestHandler<LoginQueryRequest, LoginQueryResponse>
{
    private readonly IAccountManager _accountManager = accountManager;
    private readonly ITokenFactory _tokenFactory = tokenFactory;

    public async Task<LoginQueryResponse> Handle(LoginQueryRequest request, CancellationToken cancellationToken)
    {
        var result = await _accountManager.Login(request.Username, request.Password);
        if (!result.IsSuccess)
        {
            throw new LoginFailedException();
        }

        var tokens = await _tokenFactory.CreateTokenAsync(result.UserId);
        return new LoginQueryResponse(tokens.AccessToken, tokens.RefreshToken);
    }
}