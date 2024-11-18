namespace UserManagement.Application.ApplicationServices.ServiceGroups.Queries.GetById;

public sealed class GetServiceGroupByIdQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetServiceGroupByIdQueryRequest, SectionGroupDto>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<SectionGroupDto> Handle(GetServiceGroupByIdQueryRequest request,
        CancellationToken token)
    {
        var response = await _uow.SectionGroups.GetById(request.Id, SectionType.Service, token)
                       ?? throw new ServiceGroupNotFoundException();

        return response.Adapt<SectionGroupDto>();
    }
}