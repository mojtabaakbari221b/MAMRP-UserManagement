namespace UserManagement.Application.ApplicationServices.Services.Queries.GetAll;

public class GetAllServiceQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetAllServiceQueryRequest, IEnumerable<ServiceDto>>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<IEnumerable<ServiceDto>> Handle(GetAllServiceQueryRequest request, CancellationToken token)
    {
       var response =  await _uow.Sections.GetAllServices(request.PageNumber, request.PageSize, token);
       return response.Adapt<IEnumerable<ServiceDto>>();
    }
}