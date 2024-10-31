
using MediatR;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Repositories;

public class UpdateSectionCommandHandler(ISectionRepository _repository) : IRequestHandler<UpdateSectionCommand>
{
    public async Task Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
    {
        Section menu = _repository.GetById(request.Id);
        if (menu == null)
        {
            throw new InvalidOperationException();
        }

        Section newSection = new Section()
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            Url = request.Url,
            GroupId = request.GroupId,
        };

        _repository.Update(newSection);
    }
}

