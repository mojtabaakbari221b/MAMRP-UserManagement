namespace UserManagement.Application.ApplicationServices.Services.Queries.GetById;

public class GetServiceByIdQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetSectionByIdQueryRequest, ServiceDto>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<ServiceDto> Handle(GetSectionByIdQueryRequest request, CancellationToken token)
    {
        var response = await _uow.Sections.GetById(request.Id, token)
                       ?? throw new ServiceNotFoundException();

        return response.Adapt<ServiceDto>();
    }
}