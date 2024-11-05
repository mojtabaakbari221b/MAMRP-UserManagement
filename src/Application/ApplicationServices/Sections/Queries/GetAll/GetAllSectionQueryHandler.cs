namespace UserManagement.Application.ApplicationServices.Sections.Queries.GetAll;

public class GetAllSectionQueryHandler(IUnitOfWork uow)
    : IRequestHandler<GetAllSectionQueryRequest, IEnumerable<SectionDto>>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<IEnumerable<SectionDto>> Handle(GetAllSectionQueryRequest request, CancellationToken token)
    {
       var response =  await _uow.Sections.List(token);
       return response.Adapt<IEnumerable<SectionDto>>();
    }
}