namespace UserManagement.Application.ApplicationServices.Roles.Queries.GetById;

public sealed class GetAllRoleQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetAllRoleQueryRequest, IEnumerable<GetRoleQueryResponse>>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<IEnumerable<GetRoleQueryResponse>> Handle(GetAllRoleQueryRequest request,
        CancellationToken token)
    {
        var responses = await _uow.Roles.GetAll(request.PageNumber, request.PageSize, token);
        return responses.Adapt<IEnumerable<GetRoleQueryResponse>>();
    }
}