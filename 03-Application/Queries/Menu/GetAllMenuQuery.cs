using MediatR;
using UserManagement.Domain.Entities;


public class GetAllMenuQuery : IRequest<IList<Menu>> { }

