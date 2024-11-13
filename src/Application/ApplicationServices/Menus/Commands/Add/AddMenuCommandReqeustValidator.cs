namespace UserManagement.Application.ApplicationServices.Menus.Commands.Add;

public sealed class AddMenuCommandReqeustValidator : AbstractValidator<AddMenuCommandReqeust>
{
    public AddMenuCommandReqeustValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("نام منو اجباری است")
            .MaximumLength(100).WithMessage("نام منو نباید بیشتر از 100 حرف باشد");

        RuleFor(x => x.DisplayName)
            .NotEmpty().WithMessage("نام فارسی منو اجباری است")
            .MaximumLength(200).WithMessage("نام فارسی منو نباید بیشتر از 200 حرف باشد");

        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("آدرس نباید خالی باشد");

        RuleFor(x => x.GroupId)
            .Must(x => x > 0).WithMessage("شناسه ی گروه اشتباه است و 0 نباید باشد");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("توضیحات منو اجباری است")
            .MaximumLength(100).WithMessage("توضیحات منو نباید بیشتر از 5000 حرف باشد");
    }
}