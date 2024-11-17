namespace UserManagement.Application.ApplicationServices.MenuGroup.Queries.GetAll;

public sealed class GetAllMenuGroupQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetAllMenuGroupQueryRequest, IEnumerable<SectionGroupDto>>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<IEnumerable<SectionGroupDto>> Handle(GetAllMenuGroupQueryRequest request,
        CancellationToken token)
    {
        var responses = await _uow.SectionGroups
            .ToList(request.PageNumber, request.PageSize, request.Type, token);

        return responses.Adapt<IEnumerable<SectionGroupDto>>();
    }
}