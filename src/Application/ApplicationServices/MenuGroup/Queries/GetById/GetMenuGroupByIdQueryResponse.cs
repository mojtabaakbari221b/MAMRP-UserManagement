namespace UserManagement.Application.ApplicationServices.MenuGroup.Queries.GetById;

public sealed record GetMenuGroupByIdQueryResponse(long Id, string Name) : IResponse;