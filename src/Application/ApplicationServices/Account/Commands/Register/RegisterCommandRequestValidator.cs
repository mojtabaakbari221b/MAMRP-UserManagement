namespace UserManagement.Application.ApplicationServices.Account.Commands.Register;

public sealed class RegisterCommandRequestValidator : AbstractValidator<RegisterCommandRequest>
{
    private readonly IUnitOfWork _uow;

    public RegisterCommandRequestValidator(IUnitOfWork uow)
    {
        _uow = uow;
        RuleFor(x => x.UserName)
            .NotEmpty().NotNull().WithMessage("نام کاربری نمی‌تواند خالی باشد");

        RuleFor(x => x.UserName)
            .Matches("^[a-zA-Z]+$").WithMessage("نام کاربری فقط می تواند شامل حروف انگلیسی باشد.");

        RuleFor(x => x.UserName)
            .MustAsync(AlreadyExistUserName).WithMessage("این نام کاربری وجود دارد");

        RuleFor(x => x.FirstName)
            .NotNull().NotEmpty().WithMessage("نام اجباری است");

        RuleFor(x => x.FamilyName)
            .NotNull().NotEmpty().WithMessage("نام خانوادگی اجباری است");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("رمز عبور نمی‌تواند خالی باشد")
            .MinimumLength(8).WithMessage("رمز عبور باید حداقل ۸ کاراکتر باشد")
            .Matches("[A-Z]").WithMessage("رمز عبور باید حداقل یک حرف بزرگ داشته باشد")
            .Matches("[a-z]").WithMessage("رمز عبور باید حداقل یک حرف کوچک داشته باشد")
            .Matches("[0-9]").WithMessage("رمز عبور باید حداقل یک عدد داشته باشد")
            .Matches("[^a-zA-Z0-9]").WithMessage("رمز عبور باید حداقل یک کاراکتر خاص داشته باشد");
    }

    private async Task<bool> AlreadyExistUserName(string userName, CancellationToken token)
        => !await _uow.Users.AnyAsync(userName, token);
}