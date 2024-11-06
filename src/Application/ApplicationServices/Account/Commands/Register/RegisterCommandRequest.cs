namespace UserManagement.Application.ApplicationServices.Account.Commands.Register;

public record RegisterCommandRequest(string Username, string FamilyName, string Password) : IRequest;