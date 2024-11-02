using MediatR;
using UserManagement.Domain.Repositories;
using UserManagement.Domain.Common;

namespace UserManagement.Application.Queries.Sections.GetById;


public class GetSectionByIdQueryHandler(ISectionRepository _repository) : IRequestHandler<GetSectionByIdQuery, IResponse>
{
    public async Task<IResponse> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetById(request.Id);
    }
}
