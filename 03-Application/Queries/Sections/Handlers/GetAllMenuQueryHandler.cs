using MediatR;
using Mapster;
using UserManagement.Infrastructure.Repositories;
using UserManagement.Application.Dtos.SectionDtos;

namespace UserManagement.Application.Queries.Sections.Handlers;


public class GetAllSectionQueryHandler(ISectionRepository _repository) : IRequestHandler<GetAllSectionQuery, IList<SectionDto>>
{
    public async Task<IList<SectionDto>> Handle(GetAllSectionQuery request, CancellationToken cancellationToken)
    {
        var menus = _repository.List();
        var SectionDtos = menus.Adapt<IList<SectionDto>>();
        await Task.CompletedTask;
        return SectionDtos;
    }
}
