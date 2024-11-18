namespace UserManagement.Application.ApplicationServices.Account.Queries;

public sealed record GetUserQueryResponse(
    Guid UserId,
    string UserName,
    string FirstName,
    string FamilyName)
    : IResponse;