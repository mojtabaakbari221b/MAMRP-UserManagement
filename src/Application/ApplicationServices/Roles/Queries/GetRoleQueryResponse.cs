namespace UserManagement.Application.ApplicationServices.Roles.Queries;

public sealed record GetRoleQueryResponse(Guid Id, string Name, string DisplayName) : IResponse;