using Mapster;
using MediatR;
using UserManagement.Application.Dtos.SectionDtos;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Repositories;


public class AddSectionCommandHandler(ISectionRepository _repository) : IRequestHandler<AddSectionCommand, SectionDto>
{
    public async Task<SectionDto> Handle(AddSectionCommand command, CancellationToken cancellationToken)
    {
        Section menu = new Section(){
            Name = command.Name,
            Url = command.Url,
            Description = command.Description,
            GroupId = command.GroupId,
        };
        var insertedSection = await _repository.Add(menu);
        return insertedSection.Adapt<SectionDto>();
    }
}