using MediatR;
using UserManagement.Application.Dtos.SectionDtos;
using UserManagement.Domain.Common;

namespace UserManagement.Application.Queries.Sections.GetById;


public record GetSectionByIdQuery(long Id) : IRequest<IResponse>;
