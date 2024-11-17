namespace UserManagement.Application.ApplicationServices.ServiceGroups.Commands.Add;

public sealed class AddServiceGroupCommandValidator : AbstractValidator<AddServiceGroupCommandRequest>
{
    public AddServiceGroupCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().NotNull().WithMessage("نام سکشن گروپ نباید خالی باشد");
    }
}