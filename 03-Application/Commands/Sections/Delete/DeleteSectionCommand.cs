using MediatR;

public sealed record DeleteSectionCommand(long Id) : IRequest;
