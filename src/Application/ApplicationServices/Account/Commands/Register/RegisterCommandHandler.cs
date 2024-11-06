namespace UserManagement.Application.ApplicationServices.Account.Commands.Register;



public sealed class RegisterCommandHandler(IAccountManager accountManager)
    : IRequestHandler<RegisterCommandRequest>
{
    private readonly IAccountManager _accountManager = accountManager;

    public async Task Handle(RegisterCommandRequest request, CancellationToken token)
    {
        var model = request.Adapt<RegisterDto>();
        await _accountManager.Register(model);
    }
}