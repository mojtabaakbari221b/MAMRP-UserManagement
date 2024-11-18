namespace UserManagement.Application.ApplicationServices.Account.Queries.GetById;

public sealed record GetUserByIdQueryRequest(Guid UserId) : IRequest<GetUserQueryResponse>;