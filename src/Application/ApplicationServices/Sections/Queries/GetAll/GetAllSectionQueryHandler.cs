namespace UserManagement.Application.ApplicationServices.Sections.Queries.GetAll;

public class GetAllSectionQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetAllSectionQueryRequest, PaginationResult<IEnumerable<SectionDto>>>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<PaginationResult<IEnumerable<SectionDto>>> Handle(GetAllSectionQueryRequest request, CancellationToken token)
    {
       var responses =  await _uow.Sections.List(request.PageSize, request.PageNumber, token);
       var sectionCount = await _uow.Sections.Count();
       return new PaginationResult<IEnumerable<SectionDto>>(
           data : responses.Adapt<IEnumerable<SectionDto>>(),
           pageNumber : request.PageNumber,
           pageSize : request.PageSize,
           totalRecords : sectionCount
       );
    }
}