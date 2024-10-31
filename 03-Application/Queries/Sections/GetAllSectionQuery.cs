using MediatR;
using UserManagement.Application.Dtos.SectionDtos;

namespace UserManagement.Application.Queries.Sections;
public class GetAllSectionQuery : IRequest<IList<SectionDto>>
{
    public int MyProperty { get; init; }
}

