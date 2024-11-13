namespace UserManagement.Application.ApplicationServices.Roles.Queries.GetAll;

public sealed record GetAllRoleQueryReqeust(Guid Id) : IRequest<GetRoleQueryResponse>;