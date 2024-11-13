namespace UserManagement.Application.ApplicationServices.Services.Queries.GetAll;

public class GetAllServiceQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetAllServiceQueryRequest, PaginationResult<IEnumerable<ServiceDto>>>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<PaginationResult<IEnumerable<ServiceDto>>> Handle(GetAllServiceQueryRequest request, CancellationToken token)
    {
       var response =  await _uow.Sections.GetAll(request.PageNumber, request.PageSize, SectionType.Service, token);
       return new PaginationResult<IEnumerable<ServiceDto>>
       (
            response.Adapt<IEnumerable<ServiceDto>>(),
           request.PageNumber,
           request.PageSize,
           1
       );
    }
}