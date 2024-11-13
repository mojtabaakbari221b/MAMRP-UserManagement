﻿namespace UserManagement.Application.ApplicationServices.Account.Commands.Update;

public sealed class UpdateUserCommandHandler(IUnitOfWork uow) : IRequestHandler<UpdateUserCommandRequest>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(UpdateUserCommandRequest request, CancellationToken token)
    {
        if (!await _uow.Users.AnyAsync(request.UserId, token))
        {
            throw new UserNotFoundException();
        }

        await _uow.Users.Update(request.Adapt<UserDto>(), token);
    }
}