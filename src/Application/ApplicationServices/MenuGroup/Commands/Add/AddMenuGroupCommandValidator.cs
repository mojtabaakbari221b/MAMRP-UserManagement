namespace UserManagement.Application.ApplicationServices.MenuGroup.Commands.Add;

public sealed class AddMenuGroupCommandValidator : AbstractValidator<AddMenuGrcoupCommandRequest>
{
    private readonly IUnitOfWork _uow;

    public AddMenuGroupCommandValidator(IUnitOfWork uow)
    {
        _uow = uow;
        RuleFor(r => r.Name)
            .NotEmpty().NotNull().WithMessage("نام گروه منو اجباری است")
            .MustAsync(MenuGroupAlredyExist).WithMessage("این نام گروه منو از قبل وجود دارد");
    }

    private async Task<bool> MenuGroupAlredyExist(string name, CancellationToken token)
        => !await _uow.SectionGroups.AnyAsync(name, SectionType.Menu, token);
}