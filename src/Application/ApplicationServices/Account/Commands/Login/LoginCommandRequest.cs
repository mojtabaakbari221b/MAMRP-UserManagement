namespace UserManagement.Application.ApplicationServices.Account.Commands.Login;

public sealed record LoginCommandRequest(string Username, string Password, string CaptchaValue) : IRequest<LoginCommandResponse>;