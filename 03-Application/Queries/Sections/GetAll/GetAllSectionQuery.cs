using MediatR;
using UserManagement.Application.Dtos.SectionDtos;
using UserManagement.Domain.Common;

namespace UserManagement.Application.Queries.Sections.GetAll;
public record GetAllSectionQuery : IRequest<IList<IResponse>>;