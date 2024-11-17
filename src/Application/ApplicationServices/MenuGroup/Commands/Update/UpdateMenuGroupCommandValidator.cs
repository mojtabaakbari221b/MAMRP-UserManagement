namespace UserManagement.Application.ApplicationServices.MenuGroup.Commands.Update;

public sealed class UpdateMenuGroupCommandValidator : AbstractValidator<UpdateMenuGroupCommandRequest>
{
    public UpdateMenuGroupCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().NotNull().WithMessage("نام سکشن گروپ نباید خالی باشد");
    }
}