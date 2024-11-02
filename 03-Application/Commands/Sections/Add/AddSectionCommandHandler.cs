using Mapster;
using MediatR;
using UserManagement.Application.Commands.Sections.Add;
using UserManagement.Application.Dtos.SectionDtos;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;


public class AddSectionCommandHandler(ISectionRepository _repository) : IRequestHandler<AddSectionCommand, SectionDto>
{
    public async Task<SectionDto> Handle(AddSectionCommand command, CancellationToken cancellationToken)
    {
        Section section = new(){
            Name = command.Name,
            Url = command.Url,
            Description = command.Description,
            GroupId = command.GroupId,
        };
        await _repository.Add(section);
        return section.Adapt<SectionDto>();
    }
}