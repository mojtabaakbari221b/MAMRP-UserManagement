using MediatR;
using UserManagement.Infrastructure.Repositories;
using UserManagement.Domain.Entities;


public class GetAllMenuQueryHandler(IMenuRepository _repository) : IRequestHandler<GetAllMenuQuery, IList<Menu>>
{
    public async Task<IList<Menu>> Handle(GetAllMenuQuery request, CancellationToken cancellationToken)
    {
        return _repository.List();
    }
}
