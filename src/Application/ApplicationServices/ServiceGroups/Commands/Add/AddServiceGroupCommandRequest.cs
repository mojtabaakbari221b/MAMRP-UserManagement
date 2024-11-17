namespace UserManagement.Application.ApplicationServices.ServiceGroups.Commands.Add;

public sealed record AddServiceGroupCommandRequest(string Name)
    : IRequest<SectionGroupDto>;