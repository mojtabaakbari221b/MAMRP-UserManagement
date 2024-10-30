using MediatR;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.Application.Queries.Handlers;


public class GetMenuByIdQueryHandler(IMenuRepository _repository) : IRequestHandler<GetMenuByIdQuery, Menu>
{
    public async Task<Menu> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
    {
        return _repository.GetById(request.Id);
    }
}
