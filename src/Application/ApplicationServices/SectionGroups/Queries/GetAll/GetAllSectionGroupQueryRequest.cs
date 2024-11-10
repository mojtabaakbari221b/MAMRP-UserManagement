namespace UserManagement.Application.ApplicationServices.SectionGroups.Queries.GetAll;

public record GetAllSectionGroupQueryRequest(int PageNumber, int PageSize, SectionType Type)
    : IRequest<IEnumerable<GetAllSectionGroupQueryResponse>>;

public sealed record GetAllSectionGroupQueryResponse(long Id, string Name) : IResponse;

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