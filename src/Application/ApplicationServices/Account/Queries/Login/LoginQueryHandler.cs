namespace UserManagement.Application.ApplicationServices.Account.Queries.Login;

public sealed class LoginQueryHandler(IUnitOfWork uow, ITokenFactory tokenFactory)
    : IRequestHandler<LoginQueryRequest, LoginQueryResponse>
{
    private readonly ITokenFactory _tokenFactory = tokenFactory;
    private readonly IUnitOfWork _uow = uow;

    public async Task<LoginQueryResponse> Handle(LoginQueryRequest request, CancellationToken cancellationToken)
    {
        var result = await _uow.Users.Login(request.Username, request.Password);
        if (!result.IsSuccess)
        {
            throw new LoginFailedException();
        }

        var tokens = await _tokenFactory.CreateTokenAsync(result.UserId);
        await _uow.Users.SaveToken(tokens.Adapt<TokenDto>());
        return tokens.Adapt<LoginQueryResponse>();
    }
}