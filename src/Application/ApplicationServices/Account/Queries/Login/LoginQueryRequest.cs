namespace UserManagement.Application.ApplicationServices.Account.Queries.Login;

public sealed record LoginQueryRequest(string Username, string Password) : IRequest<LoginQueryResponse>;