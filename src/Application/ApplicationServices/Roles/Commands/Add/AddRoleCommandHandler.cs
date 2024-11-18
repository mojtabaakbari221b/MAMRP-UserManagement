namespace UserManagement.Application.ApplicationServices.Roles.Commands.Add;

public sealed class AddRoleCommandHandler(IUnitOfWork uow)
    : IRequestHandler<AddRoleCommandRequest, AddRoleCommandResponse>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<AddRoleCommandResponse> Handle(AddRoleCommandRequest request, CancellationToken cancellationToken)
    {
        var isExsit = await _uow.Roles.RoleExistsAsync(request.RoleName);
        if (isExsit.IsSuccess)
        {
            throw new RoleAlredyExistException();
        }
        
        var result = await _uow.Roles.AddRole(request.RoleName, request.DisplayName);
        if (!result.IsSuccess)
        {
            throw new RoleNotAddedException(result.Errors);
        }
        return request.Adapt<AddRoleCommandResponse>();
    }
}