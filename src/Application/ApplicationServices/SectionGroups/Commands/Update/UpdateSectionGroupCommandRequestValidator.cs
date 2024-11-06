namespace UserManagement.Application.ApplicationServices.SectionGroups.Commands.Update;

public sealed class UpdateSectionGroupCommandRequestValidator : AbstractValidator<UpdateSectionGroupCommandRequest>
{
    public UpdateSectionGroupCommandRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().NotNull().WithMessage("نام سکشن گروپ نباید خالی باشد");
    }
}