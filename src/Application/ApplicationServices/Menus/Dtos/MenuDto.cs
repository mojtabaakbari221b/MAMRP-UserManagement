namespace UserManagement.Application.ApplicationServices.Menus.Dtos;

public record MenuDto(
    long Id,
    string Name,
    long GroupId,
    string DisplayName,
    string Url,
    string Description) : IResponse;