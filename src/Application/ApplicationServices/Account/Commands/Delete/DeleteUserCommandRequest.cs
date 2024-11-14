namespace UserManagement.Application.ApplicationServices.Account.Commands.Delete;

public sealed record DeleteUserCommandRequest(Guid UserId) : IRequest;

public sealed class DeleteUserCommandHandler(IUnitOfWork uow) : IRequestHandler<DeleteUserCommandRequest>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(DeleteUserCommandRequest request, CancellationToken token)
    {
        var result = await _uow.Users.AnyAsync(request.UserId, token);
        if (!result.Data)
        {
            throw new UserNotFoundException();
        }
        await _uow.Users.Delete(request.UserId, token);
    }
}