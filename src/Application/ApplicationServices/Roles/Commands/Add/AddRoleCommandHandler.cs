using UserManagement.Application.ApplicationServices.Roles.Exceptions;

namespace UserManagement.Application.ApplicationServices.Roles.Commands.Add;

public sealed class AddRoleCommandHandler(IUnitOfWork uow)
    : IRequestHandler<AddRoleCommandRequest, AddRoleCommandResponse>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<AddRoleCommandResponse> Handle(AddRoleCommandRequest request, CancellationToken cancellationToken)
    {
        if (!await _uow.Roles.RoleExistsAsync(request.RoleName))
        {
            throw new RoleAlredyExistException();
        }
        await _uow.Roles.AddRole(request.RoleName, request.DisplayName);
        return request.Adapt<AddRoleCommandResponse>();
    }
}