namespace UserManagement.Application.ApplicationServices.Account.Commands.Update;

public sealed class UpdateUserCommandRequestValidator : AbstractValidator<UpdateUserCommandRequest>
{
    public UpdateUserCommandRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotNull().NotEmpty().WithMessage("نام کاربری اجباری است");
        
        RuleFor(x => x.UserName)
            .Matches("^[a-zA-Z]+$").WithMessage("نام کاربری فقط می تواند شامل حروف انگلیسی باشد.");
        
        RuleFor(x => x.FamilyName)
            .NotNull().NotEmpty().WithMessage("نام خانوادگی اجباری است");
    }
}