
using MediatR;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Repositories;

public class UpdateMenuCommandHandler(IMenuRepository _repository) : IRequestHandler<UpdateMenuCommand>
{
    public async Task Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
    {
        Menu menu = _repository.GetById(request.Id);
        if (menu == null)
        {
            throw new InvalidOperationException();
        }

        Menu newMenu = new Menu()
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            Url = request.Url,
            GroupId = request.GroupId,
        };

        _repository.Update(newMenu);
    }
}

