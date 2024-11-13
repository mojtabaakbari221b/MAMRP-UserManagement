namespace UserManagement.Application.ApplicationServices.Account.Queries.GetById;

public sealed record GetAllUserQueryRequest(int PageNumber, int PageSize)
    : IRequest<IEnumerable<GetUserQueryResponse>>;

public sealed record GetUserQueryResponse(
    Guid UserId,
    string FirstName,
    string FamilyName,
    string UserName)
    : IResponse;