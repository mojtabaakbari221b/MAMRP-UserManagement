namespace UserManagement.Application.ApplicationServices.Roles.Queries.GetById;

public sealed class GetAllRoleQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetAllRoleQueryRequest, IEnumerable<GetRoleQueryResponse>>
{
    private readonly IUnitOfWork _uow = uow;
    // TODO : rename it
    public async Task<IEnumerable<GetRoleQueryResponse>> Handle(GetAllRoleQueryRequest request,
        CancellationToken token)
    {
        var responses = await _uow.Roles.GetAll(request.Pagination, request.Filtering, token);
        return responses.Adapt<IEnumerable<GetRoleQueryResponse>>();
    }
}