using MediatR;
using UserManagement.Domain.Repositories;

namespace UserManagement.Application.Commands.Sections.Update;

public class UpdateSectionCommandHandler(ISectionRepository _repository) : IRequestHandler<UpdateSectionCommand>
{
    public async Task Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
    {
        var section = await _repository.GetById(request.Id)
            ?? throw new InvalidOperationException();

        section.Name = request.Name;
        section.Description = request.Description;
        section.Url = request.Url;
        section.GroupId = request.GroupId;

        _repository.Update(section);
    }
}

