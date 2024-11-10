namespace UserManagement.Application.ApplicationServices.SectionGroups.Queries.GetAll;

public sealed class GetAllSectionGroupQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetAllSectionGroupQueryRequest, IEnumerable<GetAllSectionGroupQueryResponse>>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<IEnumerable<GetAllSectionGroupQueryResponse>> Handle(GetAllSectionGroupQueryRequest request,
        CancellationToken token)
    {
        var responses = await _uow.SectionGroups
            .ToList(request.PageNumber, request.PageSize, request.Type, token);

        return responses.Adapt<IEnumerable<GetAllSectionGroupQueryResponse>>();
    }
}