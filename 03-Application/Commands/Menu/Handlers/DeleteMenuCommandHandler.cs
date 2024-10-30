using MediatR;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Repositories;




public class DeleteMenuCommandHandler(IMenuRepository _repository) : IRequestHandler<DeleteMenuCommand>
{
    public async Task Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine("im in handler ..");
        Menu menu = _repository.GetById(request.Id);
        if (menu != null)
        {
            _repository.Delete(menu);
        }

    }
}

