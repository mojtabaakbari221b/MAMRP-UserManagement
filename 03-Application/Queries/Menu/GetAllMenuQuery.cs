using MediatR;
using UserManagement.Application.Dtos.MenuDtos;


public class GetAllMenuQuery : IRequest<IList<MenuDto>> { }

