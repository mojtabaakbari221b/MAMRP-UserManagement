using MediatR;
using UserManagement.Application.Dtos.SectionDtos;

namespace UserManagement.Application.Queries.Sections;


public class GetSectionByIdQuery : IRequest<SectionDto>
{
    public int Id { get; set; }
}

