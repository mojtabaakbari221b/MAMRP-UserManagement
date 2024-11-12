namespace UserManagement.Application.ApplicationServices.Menus.Queries.GetById;

public sealed record GetMenuByIdQueryRequest(long Id) : IRequest<MenuDto>;