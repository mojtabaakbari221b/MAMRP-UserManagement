namespace UserManagement.Application.ApplicationServices.Roles.Queries.GetById;

public sealed record GetByIdRoleQueryReqeust(Guid Id) : IRequest<GetRoleQueryResponse>;