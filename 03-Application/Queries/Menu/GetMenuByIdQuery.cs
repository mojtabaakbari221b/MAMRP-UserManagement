using MediatR;
using UserManagement.Application.Dtos.MenuDtos;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Queries.Handlers;


public class GetMenuByIdQuery : IRequest<MenuDto>
{
    public int Id { get; set; }
}

