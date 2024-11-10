namespace UserManagement.Domain.Services.DTOs;

public sealed record RegisterDto(string Username, string FamilyName, string Password);

public sealed record RegisterLogin(bool Successed);