using UserManagement.Application.ApplicationServices.Roles.Exceptions;

namespace UserManagement.Application.ApplicationServices.Roles.Commands.Delete;

public sealed class DeleteRoleCommandHandler(IUnitOfWork uow)
    : IRequestHandler<DeleteRoleCommandRequest>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(DeleteRoleCommandRequest request, CancellationToken token)
    {
        if (!await _uow.Roles.RoleExistsAsync(request.Id))
        {
            throw new RoleNotFoundException();
        }
        await _uow.Roles.Delete(request.Id);
    }
}