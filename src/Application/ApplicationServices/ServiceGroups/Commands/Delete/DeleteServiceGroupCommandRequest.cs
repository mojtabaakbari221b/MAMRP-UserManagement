namespace UserManagement.Application.ApplicationServices.ServiceGroups.Commands.Delete;

public sealed record DeleteServiceGroupCommandRequest(long Id) : IRequest;