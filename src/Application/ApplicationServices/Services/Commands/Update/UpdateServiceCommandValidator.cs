namespace UserManagement.Application.ApplicationServices.Services.Commands.Update;

public sealed class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommandRequest>
{
    public UpdateServiceCommandValidator()
    {
        RuleFor(x => x.DisplayName)
            .NotEmpty().WithMessage("نام سرویس اجباری است");
        RuleFor(x => x.DisplayName)
            .MaximumLength(200).WithMessage("نام سرویس نباید بیشتر از 200 حرف باشد");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("توضیبحات سرویس اجباری است");
        RuleFor(x => x.Description)
            .MaximumLength(5000).WithMessage("توضیحات نباید بیشتر از 5000 حرف باشد");
        
        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("آدرس سرویس اجباری است");
    }
}