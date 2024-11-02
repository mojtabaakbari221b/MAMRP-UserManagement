using MediatR;
using Mapster;
using UserManagement.Application.Dtos.SectionDtos;
using UserManagement.Domain.Repositories;
using UserManagement.Domain.Common;

namespace UserManagement.Application.Queries.Sections.GetAll;


public class GetAllSectionQueryHandler(ISectionRepository _repository) : IRequestHandler<GetAllSectionQuery, IList<IResponse>>
{
    public async Task<IList<IResponse>> Handle(GetAllSectionQuery request, CancellationToken cancellationToken)
    {
        var section = await _repository.List();
        return section;
    }
}
