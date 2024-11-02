using MediatR;
using UserManagement.Application.Dtos.SectionDtos;
using UserManagement.Domain.Enums;

namespace UserManagement.Application.Commands.Sections.Add;

public record AddSectionCommand(long GroupId, string Name, string Description, string Url, SectionType Type) : IRequest<SectionDto>;

