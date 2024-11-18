namespace UserManagement.Application.ApplicationServices.Account.Queries.GetById;

public sealed class GetUserByIdQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetUserByIdQueryRequest, GetUserQueryResponse>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<GetUserQueryResponse> Handle(GetUserByIdQueryRequest request, CancellationToken token)
    {
        var response = await _uow.Users.GetUserById(request.UserId.ToString());
        if (response.Data is null)
        {
            throw new UserNotFoundException();
        }

        return response.Data.Adapt<GetUserQueryResponse>();
    }
}