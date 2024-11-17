namespace UserManagement.Application.ApplicationServices.ServiceGroups.Commands.Update;

public sealed class UpdateServiceGroupCommandValidator : AbstractValidator<UpdateServiceGroupCommandRequest>
{
    public UpdateServiceGroupCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().NotNull().WithMessage("نام سکشن گروپ نباید خالی باشد");
    }
}