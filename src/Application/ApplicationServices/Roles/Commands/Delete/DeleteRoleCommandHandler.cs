namespace UserManagement.Application.ApplicationServices.Roles.Commands.Delete;

public sealed class DeleteRoleCommandHandler(IUnitOfWork uow)
    : IRequestHandler<DeleteRoleCommandRequest>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(DeleteRoleCommandRequest request, CancellationToken token)
    {
        var isExist = await _uow.Roles.RoleExistsAsync(request.Id);
        if (!isExist.IsSuccess)
        {
            throw new RoleNotFoundException();
        }
        var result = await _uow.Roles.Delete(request.Id);
        if (!result.IsSuccess)
        {
            throw new RoleNotDeletedException(result.Errors);
        }
    }
}