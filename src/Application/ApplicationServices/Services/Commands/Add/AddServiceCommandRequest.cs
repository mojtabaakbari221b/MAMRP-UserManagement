namespace UserManagement.Application.ApplicationServices.Services.Commands.Add;

public sealed record AddServiceCommandRequest(
    string Name,
    string Code,
    long GroupId, // کاربر می تواند گروه آن را تغییر دهد یا سمت ما هست مثل خود سکشن(سرویس)
    string Url,
    string DisplayName,
    string Description) : IRequest ;