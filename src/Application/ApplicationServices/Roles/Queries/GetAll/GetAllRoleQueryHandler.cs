namespace UserManagement.Application.ApplicationServices.Roles.Queries.GetAll;

public sealed class GetAllRoleQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetAllRoleQueryReqeust, GetRoleQueryResponse>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<GetRoleQueryResponse> Handle(GetAllRoleQueryReqeust request, CancellationToken cancellationToken)
    {
        var response = await _uow.Roles.GetRoleById(request.Id.ToString());
        if (response is null)
        {
            throw new RoleNotFoundException();
        }

        return response.Adapt<GetRoleQueryResponse>();
    }
}