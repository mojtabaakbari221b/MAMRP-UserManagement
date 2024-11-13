namespace UserManagement.Application.ApplicationServices.Account.Queries.GetById;

public sealed class GetAllUserQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetAllUserQueryRequest, IEnumerable<GetUserQueryResponse>>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<IEnumerable<GetUserQueryResponse>> Handle(GetAllUserQueryRequest request, CancellationToken token)
    {
        var responses = await _uow.Users.GetAll(request.PageNumber, request.PageSize, token);
        return responses.Adapt<IEnumerable<GetUserQueryResponse>>();
    }
}