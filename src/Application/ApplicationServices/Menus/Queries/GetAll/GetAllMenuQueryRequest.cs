namespace UserManagement.Application.ApplicationServices.Menus.Queries.GetAll;

public sealed record GetAllMenuQueryRequest(int PageNumber, int PageSize) 
    : IRequest<IEnumerable<MenuDto>>;