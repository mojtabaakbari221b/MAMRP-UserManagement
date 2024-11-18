using UserManagement.Application.ApplicationServices.Account.Commands.Login;

namespace UserManagement.Application.ApplicationServices.Account.Commands.LoginByRefreshToken;

public sealed record LoginByRefreshTokenCommandRequest(Guid UserId) : IRequest<LoginCommandResponse>;

public sealed class LoginByRefreshTokenCommandHandler(ITokenFactory tokenFactory, IUnitOfWork uow)
    : IRequestHandler<LoginByRefreshTokenCommandRequest, LoginCommandResponse>
{
    private readonly ITokenFactory _tokenFactory = tokenFactory;
    private readonly IUnitOfWork _uow = uow;

    public async Task<LoginCommandResponse> Handle(LoginByRefreshTokenCommandRequest request,
        CancellationToken cancellationToken)
    {
        var tokens = await _tokenFactory.CreateTokenAsync(request.UserId);
        await _uow.Users.SaveToken(tokens.Adapt<TokenDto>());
        return tokens.Adapt<LoginCommandResponse>();
    }
}