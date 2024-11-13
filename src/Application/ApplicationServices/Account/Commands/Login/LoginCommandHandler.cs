namespace UserManagement.Application.ApplicationServices.Account.Commands.Login;

public sealed class LoginCommandHandler(IUnitOfWork uow, ITokenFactory tokenFactory)
    : IRequestHandler<LoginCommandRequest, LoginCommandResponse>
{
    private readonly ITokenFactory _tokenFactory = tokenFactory;
    private readonly IUnitOfWork _uow = uow;

    public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _uow.Users.Login(request.Username, request.Password);
        if (!result.IsSuccess)
        {
            throw new LoginFailedException();
        }

        var tokens = await _tokenFactory.CreateTokenAsync(result.UserId);
        await _uow.Users.SaveToken(tokens.Adapt<TokenDto>());
        return tokens.Adapt<LoginCommandResponse>();
    }
}