namespace UserManagement.Application.ApplicationServices.Account.Commands.Login;

public readonly record struct LoginCommandResponse(string AccessToken, string RefreshToken);