using MediatR;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Repositories;




public class DeleteSectionCommandHandler(ISectionRepository _repository) : IRequestHandler<DeleteSectionCommand>
{
    public async Task Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine("im in handler ..");
        Section menu = _repository.GetById(request.Id);
        if (menu != null)
        {
            _repository.Delete(menu);
        }

    }
}

