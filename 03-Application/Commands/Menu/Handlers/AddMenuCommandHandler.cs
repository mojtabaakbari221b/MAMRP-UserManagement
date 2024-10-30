using MediatR;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.Application.Commands.Handlers;

public class AddMenuCommandHandler(IMenuRepository _repository) : IRequestHandler<AddMenuCommand, int>
{
    public async Task<int> Handle(AddMenuCommand command, CancellationToken cancellationToken)
    {
        Menu menu = new Menu(){
            Name = command.Name,
            Url = command.Url,
            Description = command.Description,
            GroupId = command.GroupId,
        };
        _repository.Add(menu);
        return 10;
    }
}