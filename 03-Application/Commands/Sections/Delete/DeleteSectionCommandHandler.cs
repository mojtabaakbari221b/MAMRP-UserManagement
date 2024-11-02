using MediatR;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;




public class DeleteSectionCommandHandler(ISectionRepository _repository) : IRequestHandler<DeleteSectionCommand>
{
    public async Task Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
    {
        Section menu = await _repository.GetById(request.Id);
        if (menu != null)
        {
            _repository.Delete(menu);
        }

    }
}

