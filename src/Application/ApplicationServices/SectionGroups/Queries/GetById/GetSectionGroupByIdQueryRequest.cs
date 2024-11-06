namespace UserManagement.Application.ApplicationServices.SectionGroups.Queries.GetById;

public sealed record GetSectionGroupByIdQueryRequest(long Id) : IRequest<GetSectionGroupByIdQueryResponse>;

public sealed record GetSectionGroupByIdQueryResponse(long Id, string Name, SectionType Type) : IResponse;

public sealed class GetSectionGroupByIdQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetSectionGroupByIdQueryRequest, GetSectionGroupByIdQueryResponse>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<GetSectionGroupByIdQueryResponse> Handle(GetSectionGroupByIdQueryRequest request, CancellationToken token)
    {
        var response = await _uow.SectionGroups.GetById(request.Id, token)
            ?? throw new SectionGroupNotFoundException();
        return response.Adapt<GetSectionGroupByIdQueryResponse>();
    }
}