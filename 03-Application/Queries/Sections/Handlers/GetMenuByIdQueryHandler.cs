using MediatR;
using Mapster;
using UserManagement.Infrastructure.Repositories;
using UserManagement.Application.Dtos.SectionDtos;

namespace UserManagement.Application.Queries.Sections.Handlers;


public class GetSectionByIdQueryHandler(ISectionRepository _repository) : IRequestHandler<GetSectionByIdQuery, SectionDto>
{
    public async Task<SectionDto> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
    {
        var menu = _repository.GetById(request.Id);
        var menuDto = menu.Adapt<SectionDto>();
        await Task.CompletedTask;
        return menuDto;
    }
}
