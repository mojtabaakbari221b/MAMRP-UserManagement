namespace UserManagement.Application.ApplicationServices.Services.Dtos;

public sealed record ServiceDto(
    long Id,
    long GroupId,
    string DisplayName,
    string Url,
    string Code,
    string Description) : IResponse;