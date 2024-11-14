using Share.QueryFilterings;

namespace UserManagement.Application.ApplicationServices.Services.Queries.GetAll;

public class GetAllServiceQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetAllServiceQueryRequest, PaginationResult<IEnumerable<ServiceDto>>>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<PaginationResult<IEnumerable<ServiceDto>>> Handle(GetAllServiceQueryRequest request, CancellationToken token)
    {
       var listDto =  await _uow.Sections.GetAll(request.Pagination, request.Filtering, request.Ordering, SectionType.Service, token);
       return new PaginationResult<IEnumerable<ServiceDto>>
       (
            data: listDto.Responses.Adapt<IEnumerable<ServiceDto>>(),
           pageNumber: request.Pagination.PageNumber,
           pageSize: request.Pagination.PageSize,
           totalRecords: listDto.Count
       );
    }
}