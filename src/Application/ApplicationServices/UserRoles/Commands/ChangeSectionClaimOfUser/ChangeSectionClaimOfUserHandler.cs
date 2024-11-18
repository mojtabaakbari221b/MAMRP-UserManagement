namespace UserManagement.Application.ApplicationServices.UserRoles.Commands.ChangeSectionClaimOfUser;

public sealed class ChangeSectionClaimOfUserHandler(IUnitOfWork uow)
    : IRequestHandler<ChangeSectionClaimOfUserRequest>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(ChangeSectionClaimOfUserRequest request, CancellationToken token)
    {
        await _uow.BeginTransactionAsync(token);
        try
        {
            await _uow.Users.RemoveSectionClaimOfUserAsync(request.UserId);
            await _uow.Users.AddSectionIdsToUserClaimAsync(request.UserId, request.SelectionIds);

            await _uow.SaveChangesAsync(token);
            await _uow.CommitTransactionAsync(token);
        }
        catch (Exception ex)
        {
            await _uow.RoleBackTransactionAsync(token);
            throw;
        }
    }
}