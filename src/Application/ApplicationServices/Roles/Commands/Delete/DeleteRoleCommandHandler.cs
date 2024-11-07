namespace UserManagement.Application.ApplicationServices.Roles.Commands.Delete;

public sealed class DeleteRoleCommandHandler(IAccountManager accountManager)
    : IRequestHandler<DeleteRoleCommandRequest>
{
    private readonly IAccountManager _accountManager = accountManager;

    public Task Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}