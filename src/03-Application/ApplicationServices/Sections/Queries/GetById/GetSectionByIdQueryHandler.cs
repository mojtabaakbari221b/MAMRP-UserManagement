namespace UserManagement.Application.ApplicationServices.Sections.Queries.GetById;

public class GetSectionByIdQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetSectionByIdQueryRequest, SectionDto>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<SectionDto> Handle(GetSectionByIdQueryRequest request, CancellationToken token)
    {
        var response = await _uow.Sections.GetById(request.Id, token)
                       ?? throw new SectionNotFoundException();

        return response.Adapt<SectionDto>();
    }
}