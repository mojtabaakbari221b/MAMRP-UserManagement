namespace UserManagement.Application.ApplicationServices.Roles.Commands.Update;

public sealed class UpdateRoleCommandRequestHandler(IUnitOfWork uow) : IRequestHandler<UpdateRoleCommandRequest>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(UpdateRoleCommandRequest request, CancellationToken cancellationToken)
    {
        var isExist = await _uow.Roles.RoleExistsAsync(request.Id);
        if (!isExist.IsSuccess)
        {
            throw new RoleNotFoundException();
        }
        
        var result = await _uow.Roles.Update(request.Adapt<RoleDto>());
        if (!result.IsSuccess)
        {
            throw new RoleNotUpdatedException(result.Errors);
        }
    }
}