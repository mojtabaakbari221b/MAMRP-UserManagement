namespace UserManagement.Application.ApplicationServices.Menus.Commands.Update;

public sealed record UpdateMenuCommandRequest(
    long Id,
    string Name,
    string DisplayName,
    string Url,
    long GroupId,
    string Description) : IRequest
{
    public static UpdateMenuCommandRequest Create(long id, UpdateMenuDto model)
        => new(id, model.Name, model.DisplayName, model.Url, model.GroupId, model.Description);
}

public sealed record UpdateMenuDto(
    string Name,
    string DisplayName,
    string Url,
    long GroupId,
    string Description);