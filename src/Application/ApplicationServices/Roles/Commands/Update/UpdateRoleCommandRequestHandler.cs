namespace UserManagement.Application.ApplicationServices.Roles.Commands.Update;

public sealed class UpdateRoleCommandRequestHandler(IUnitOfWork uow) : IRequestHandler<UpdateRoleCommandRequest>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(UpdateRoleCommandRequest request, CancellationToken cancellationToken)
    {
        if (!await _uow.Roles.RoleExistsAsync(request.Id))
        {
            throw new RoleNotFoundException();
        }

        await _uow.Roles.Update(request.Adapt<RoleDto>());
    }
}