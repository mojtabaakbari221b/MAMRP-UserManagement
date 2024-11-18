namespace UserManagement.Application.ApplicationServices.Account.Commands.Register;



public sealed class RegisterCommandHandler(IUnitOfWork uow)
    : IRequestHandler<RegisterCommandRequest>
{
    private readonly IUnitOfWork _uow = uow;
    public async Task Handle(RegisterCommandRequest request, CancellationToken token)
    {
        var model = request.Adapt<RegisterDto>();
        var result = await _uow.Users.Register(model);
        if (!result.IsSuccess)
        {
            throw new UserNotRegisteredException(result.Errors);
        }
    }
}