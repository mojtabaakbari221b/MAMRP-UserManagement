namespace UserManagement.Application.ApplicationServices.SectionGroups.Commands.Add;

public sealed class AddSectionGroupValidator : AbstractValidator<AddSectionGroupCommandRequest>
{
    public AddSectionGroupValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().NotNull().WithMessage("نام سکشن گروپ نباید خالی باشد");
    }
}