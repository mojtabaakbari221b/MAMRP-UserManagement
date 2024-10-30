using MediatR;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Queries.Handlers;


public class GetMenuByIdQuery : IRequest<Menu>
{
    public int Id { get; set; }
}

