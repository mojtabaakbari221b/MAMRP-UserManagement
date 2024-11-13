﻿using Share.QueryFilterings;
using UserManagement.Application.ApplicationServices.Menus.Filterings;

namespace UserManagement.Application.ApplicationServices.Menus.Queries.GetAll;

public sealed record GetAllMenuQueryRequest(PaginationFilter Pagination, MenuFiltering? Filtering) 
    : IRequest<PaginationResult<IEnumerable<MenuDto>>>;