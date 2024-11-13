namespace UserManagement.Application.ApplicationServices.Account.Commands.Register;

public record RegisterCommandRequest(
    string UserName,
    string FirstName,
    string FamilyName,
    string Password) : IRequest;