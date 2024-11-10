namespace UserManagement.Application.ApplicationServices.Roles.Queries.GetAll;

public sealed class GetRoleByIdQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetRoleByIdQueryReqeust, GetRoleQueryResponse>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<GetRoleQueryResponse> Handle(GetRoleByIdQueryReqeust request, CancellationToken cancellationToken)
    {
        var response = await _uow.Roles.GetRoleById(request.Id.ToString());
        if (response is null)
        {
            throw new RoleNotFoundException();
        }

        return response.Adapt<GetRoleQueryResponse>();
    }
}