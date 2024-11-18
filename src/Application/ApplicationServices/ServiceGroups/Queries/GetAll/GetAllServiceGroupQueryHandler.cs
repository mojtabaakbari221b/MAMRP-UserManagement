namespace UserManagement.Application.ApplicationServices.ServiceGroups.Queries.GetAll;

public sealed class GetAllServiceGroupQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetAllServiceGroupQueryRequest, PaginationResult<IEnumerable<SectionGroupDto>>>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<PaginationResult<IEnumerable<SectionGroupDto>>> Handle(GetAllServiceGroupQueryRequest request,
        CancellationToken token)
    {
        var results = await _uow.SectionGroups
            .GetAll(request.Pagination, request.Filtring, request.Ordering, SectionType.Service, token);

        return new PaginationResult<IEnumerable<SectionGroupDto>>
        (
            data: results.Responses.Adapt<IEnumerable<SectionGroupDto>>(),
            pageNumber: request.Pagination.PageNumber,
            pageSize: request.Pagination.PageSize,
            totalRecords: results.Count
        );    }
}