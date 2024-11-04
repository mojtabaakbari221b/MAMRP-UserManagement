namespace UserManagement.Application.ApplicationServices.Sections.Commands.Update;

public sealed record UpdateSectionCommandRequest(
    long Id,
    long GroupId,
    string Name,
    string Url,
    string Description) : IRequest
{
    public static UpdateSectionCommandRequest Create(long id, UpdateSectionDto model)
        => new(id, model.GroupId, model.Name, model.Url, model.Description);
}

public sealed record UpdateSectionDto(long GroupId, string Name, string Url, string Description);