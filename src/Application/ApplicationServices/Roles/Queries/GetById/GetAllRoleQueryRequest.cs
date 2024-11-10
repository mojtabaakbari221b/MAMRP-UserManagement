namespace UserManagement.Application.ApplicationServices.Roles.Queries.GetById;

public sealed record GetAllRoleQueryRequest(int PageNumber, int PageSize)
    : IRequest<IEnumerable<GetRoleQueryResponse>>;