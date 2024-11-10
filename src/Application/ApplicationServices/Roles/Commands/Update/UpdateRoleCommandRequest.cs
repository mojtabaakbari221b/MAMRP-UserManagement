namespace UserManagement.Application.ApplicationServices.Roles.Commands.Update;

public sealed record UpdateRoleCommandRequest(Guid Id, string Name, string DisplayName) : IRequest
{
    public static UpdateRoleCommandRequest Create(Guid id, UpdateRoleDto model)
        => new(id, model.Name, model.DisplayName);
}

public sealed record UpdateRoleDto(string Name, string DisplayName);