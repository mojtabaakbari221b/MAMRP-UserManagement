namespace UserManagement.Application.ApplicationServices.MenuGroup.Queries.GetById;

public sealed class GetMenuGroupByIdQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetMenuGroupByIdQueryRequest, SectionGroupDto>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<SectionGroupDto> Handle(GetMenuGroupByIdQueryRequest request, CancellationToken token)
    {
        var response = await _uow.SectionGroups.GetById(request.Id, SectionType.Menu, token)
                       ?? throw new SectionGroupNotFoundException();
        
        return response.Adapt<SectionGroupDto>();
    }
}