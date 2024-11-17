namespace UserManagement.Application.ApplicationServices.MenuGroup.Queries.GetAll;

public sealed record GetAllMenuGroupQueryResponse(long Id, string Name) : IResponse;