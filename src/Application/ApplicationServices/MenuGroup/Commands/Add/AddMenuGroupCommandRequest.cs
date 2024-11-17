namespace UserManagement.Application.ApplicationServices.MenuGroup.Commands.Add;

public sealed record AddMenuGrcoupCommandRequest(string Name)
    : IRequest<SectionGroupDto>;