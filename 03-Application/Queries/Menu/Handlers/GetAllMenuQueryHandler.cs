using MediatR;
using Mapster;
using UserManagement.Infrastructure.Repositories;
using UserManagement.Application.Dtos.MenuDtos;


public class GetAllMenuQueryHandler(IMenuRepository _repository) : IRequestHandler<GetAllMenuQuery, IList<MenuDto>>
{
    public async Task<IList<MenuDto>> Handle(GetAllMenuQuery request, CancellationToken cancellationToken)
    {
        var menus = _repository.List();
        var menuDtos = menus.Adapt<IList<MenuDto>>();
        return menuDtos;
    }
}
