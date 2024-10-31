using MediatR;
using Mapster;
using UserManagement.Application.Dtos.MenuDtos;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.Application.Queries.Handlers;


public class GetMenuByIdQueryHandler(IMenuRepository _repository) : IRequestHandler<GetMenuByIdQuery, MenuDto>
{
    public async Task<MenuDto> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
    {
        var menu = _repository.GetById(request.Id);
        var menuDto = menu.Adapt<MenuDto>();
        return menuDto;
    }
}
