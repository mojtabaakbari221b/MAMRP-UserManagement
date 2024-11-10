namespace UserManagement.Application.ApplicationServices.Roles.Queries.GetAll;

public sealed record GetRoleByIdQueryReqeust(Guid Id) : IRequest<GetRoleQueryResponse>;