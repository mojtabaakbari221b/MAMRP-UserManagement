namespace UserManagement.Application.ApplicationServices.Account.Queries.Login;

public readonly record struct LoginQueryResponse(string AccessToken, string RefreshToken);