namespace UserManagement.Application.ApplicationServices.Roles.Commands.Add;

public sealed record AddRoleCommandRequest(string RoleName, string DisplayName) : IRequest<AddRoleCommandResponse>;

public sealed class AddRoleCommandValidator : AbstractValidator<AddRoleCommandRequest>
{
    public AddRoleCommandValidator()
    {
        RuleFor(x => x.RoleName).NotEmpty().NotNull().WithMessage("نام نقش اجباری است");
        RuleFor(x => x.DisplayName).NotEmpty().NotNull().WithMessage("نام دسترسی نقش اجباری است");
    }
}

public sealed record AddRoleCommandResponse(string RoleName, string DisplayName);

