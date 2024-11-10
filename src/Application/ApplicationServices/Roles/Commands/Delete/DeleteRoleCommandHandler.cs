namespace UserManagement.Application.ApplicationServices.Roles.Commands.Delete;

public sealed class DeleteRoleCommandHandler(IUnitOfWork uow)
    : IRequestHandler<DeleteRoleCommandRequest>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(DeleteRoleCommandRequest request, CancellationToken token)
    {
        var role = await _uow.Roles.GetRoleById(request.Id.ToString());
        if (role is null)
        {
            throw new RoleNotFoundException();
        }
        await _uow.Roles.Delete(role);
    }
}