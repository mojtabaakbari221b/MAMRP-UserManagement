namespace UserManagement.Application.ApplicationServices.Services.Commands.Update;

public sealed record UpdateServiceCommandRequest(
    long Id,
    long GroupId, // کاربر می تواند گروه آن را تغییر دهد یا سمت ما هست مثل خود سکشن(سرویس)
    string Url,
    string DisplayName,
    string Description) : IRequest
{
    public static UpdateServiceCommandRequest Create(long id, UpdateSectionDto model)
        => new(id, model.GroupId, model.Url, model.DisplayName, model.Description);
}

public sealed record UpdateSectionDto(
    long GroupId,
    string Url,
    string DisplayName,
    string Description);