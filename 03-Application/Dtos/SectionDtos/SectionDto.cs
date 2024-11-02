using UserManagement.Domain.Common;

namespace UserManagement.Application.Dtos.SectionDtos;

public sealed record SectionDto(long Id, string Name, string Url, long GroupId, string Description) : IResponse;
