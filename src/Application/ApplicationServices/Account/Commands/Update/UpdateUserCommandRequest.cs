namespace UserManagement.Application.ApplicationServices.Account.Commands.Update;

public sealed record UpdateUserCommandRequest(Guid UserId, string UserName, string FamilyName) : IRequest
{
    public static UpdateUserCommandRequest Create(Guid userId, UpdateUserDto model)
        => new(userId, model.UserName, model.FamilyName);
}

public sealed record UpdateUserDto(string UserName, string FamilyName);