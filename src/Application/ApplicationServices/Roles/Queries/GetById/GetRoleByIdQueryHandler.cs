namespace UserManagement.Application.ApplicationServices.Roles.Queries.GetById;

public sealed class GetRoleByIdQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetByIdRoleQueryReqeust, GetRoleQueryResponse>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<GetRoleQueryResponse> Handle(GetByIdRoleQueryReqeust request, CancellationToken cancellationToken)
    {
        var response = await _uow.Roles.GetRoleById(request.Id.ToString());
        if (response.Data is null)
        {
            throw new RoleNotFoundException();
        }

        return response.Data.Adapt<GetRoleQueryResponse>();
    }
}