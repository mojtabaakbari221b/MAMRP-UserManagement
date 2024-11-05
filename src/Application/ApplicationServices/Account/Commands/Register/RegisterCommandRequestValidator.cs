namespace UserManagement.Application.ApplicationServices.Account.Commands.Register;

public sealed class RegisterCommandRequestValidator : AbstractValidator<RegisterCommandRequest>
{
    public RegisterCommandRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().NotNull().WithMessage("نام کاربری نمی‌تواند خالی باشد");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("رمز عبور نمی‌تواند خالی باشد")
            .MinimumLength(8).WithMessage("رمز عبور باید حداقل ۸ کاراکتر باشد")
            .Matches("[A-Z]").WithMessage("رمز عبور باید حداقل یک حرف بزرگ داشته باشد")
            .Matches("[a-z]").WithMessage("رمز عبور باید حداقل یک حرف کوچک داشته باشد")
            .Matches("[0-9]").WithMessage("رمز عبور باید حداقل یک عدد داشته باشد")
            .Matches("[^a-zA-Z0-9]").WithMessage("رمز عبور باید حداقل یک کاراکتر خاص داشته باشد");
    }
}