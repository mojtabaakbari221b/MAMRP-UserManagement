namespace UserManagement.Application.ApplicationServices.Account.Commands.Register;

public record RegisterCommandRequest(string Username, string FamilyName, string Password) : IRequest;

public sealed class RegisterCommandHandler(IAcountManager acountManager)
    : IRequestHandler<RegisterCommandRequest>
{
    private readonly IAcountManager _acountManager = acountManager;

    public async Task Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
    {
        var model = request.Adapt<RegisterDto>();
        await _acountManager.Register(model);
    }
}