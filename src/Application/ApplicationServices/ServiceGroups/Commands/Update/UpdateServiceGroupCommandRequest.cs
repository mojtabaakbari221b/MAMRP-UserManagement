namespace UserManagement.Application.ApplicationServices.ServiceGroups.Commands.Update;

public sealed record UpdateServiceGroupCommandRequest(long Id, string Name) : IRequest;