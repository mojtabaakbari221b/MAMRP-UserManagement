namespace UserManagement.Application.ApplicationServices.Menus.Commands.Add;

public sealed record AddMenuCommandReqeust(
    string Name,
    string DisplayName,
    string Url,
    long GroupId,
    string Description) : IRequest<MenuDto>;