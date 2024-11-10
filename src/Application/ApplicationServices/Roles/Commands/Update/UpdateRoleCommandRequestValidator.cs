namespace UserManagement.Application.ApplicationServices.Roles.Commands.Update;

public sealed class UpdateRoleCommandRequestValidator : AbstractValidator<UpdateRoleCommandRequest>
{
    public UpdateRoleCommandRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("نام نقش اجباری است");
        RuleFor(x => x.DisplayName).NotEmpty().NotNull().WithMessage("نام دسترسی نقش اجباری است");
    }
}