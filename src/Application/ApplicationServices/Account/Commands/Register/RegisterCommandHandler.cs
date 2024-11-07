using UserManagement.Domain.Services.DTOs;

namespace UserManagement.Application.ApplicationServices.Account.Commands.Register;



public sealed class RegisterCommandHandler(IUnitOfWork uow)
    : IRequestHandler<RegisterCommandRequest>
{
    private readonly IUnitOfWork _uow = uow;
    public async Task Handle(RegisterCommandRequest request, CancellationToken token)
    {
        var model = request.Adapt<RegisterDto>();
        await _uow.Users.Register(model);
    }
}