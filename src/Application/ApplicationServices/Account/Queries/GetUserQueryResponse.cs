namespace UserManagement.Application.ApplicationServices.Account.Queries;

public sealed record GetUserQueryResponse(
    Guid Id,
    string UserName,
    string FirstName,
    string FamilyName)
    : IResponse;