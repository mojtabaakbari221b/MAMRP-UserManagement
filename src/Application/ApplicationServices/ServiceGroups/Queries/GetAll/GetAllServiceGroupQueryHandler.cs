namespace UserManagement.Application.ApplicationServices.ServiceGroups.Queries.GetAll;

public sealed class GetAllServiceGroupQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetAllServiceGroupQueryRequest, IEnumerable<SectionGroupDto>>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<IEnumerable<SectionGroupDto>> Handle(GetAllServiceGroupQueryRequest request,
        CancellationToken token)
    {
        var responses = await _uow.SectionGroups
            .ToList(request.PageNumber, request.PageSize, request.Type, token);

        return responses.Adapt<IEnumerable<SectionGroupDto>>();
    }
}