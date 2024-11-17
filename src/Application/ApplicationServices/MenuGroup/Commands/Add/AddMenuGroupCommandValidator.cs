namespace UserManagement.Application.ApplicationServices.MenuGroup.Commands.Add;

public sealed class AddMenuGroupCommandValidator : AbstractValidator<AddMenuGrcoupCommandRequest>
{
    public AddMenuGroupCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().NotNull().WithMessage("نام سکشن گروپ نباید خالی باشد");
    }
}