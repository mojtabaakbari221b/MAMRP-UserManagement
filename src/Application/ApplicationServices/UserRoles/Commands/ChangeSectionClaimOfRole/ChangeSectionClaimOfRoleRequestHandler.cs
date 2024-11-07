using UserManagement.Domain.Services.DTOs;

namespace UserManagement.Application.ApplicationServices.UserRoles.Commands.ChangeSectionClaimOfRole;

public class ChangeSectionClaimOfRoleRequestHandler(IUnitOfWork uow) : IRequestHandler<ChangeSectionClaimOfRoleRequest>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task Handle(ChangeSectionClaimOfRoleRequest request, CancellationToken token)
    {
        await _uow.BeginTransactionAsync(token);
        try
        {
            await _uow.Roles.RemoveSectionClaimOfRoleAsync(request.RoleId);
            await _uow.Roles.AddSectionIdsToRoleClaimAsync(request.RoleId, request.SelectionIds);
            await _uow.CommitTransactionAsync(token);
        }
        catch
        {
            await _uow.RoleBackTransactionAsync(token);
        }
    }
}