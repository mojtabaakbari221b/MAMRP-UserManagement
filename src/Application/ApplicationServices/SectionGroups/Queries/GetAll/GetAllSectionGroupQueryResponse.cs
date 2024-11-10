namespace UserManagement.Application.ApplicationServices.SectionGroups.Queries.GetAll;

public sealed record GetAllSectionGroupQueryResponse(long Id, string Name) : IResponse;