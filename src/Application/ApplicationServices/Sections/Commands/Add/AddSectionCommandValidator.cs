namespace UserManagement.Application.ApplicationServices.Sections.Commands.Add;

public class AddSectionCommandValidator : AbstractValidator<AddSectionCommandRequest>
{
    public AddSectionCommandValidator()
    {
        RuleFor(s => s.GroupId)
          .NotEmpty()
          .WithMessage("Group Id is required.");

        RuleFor(s => s.Name)
          .NotEmpty()
          .MaximumLength(50)
          .WithMessage("Name is required.");

        RuleFor(s => s.Url)
          .NotEmpty()
          .MaximumLength(200)
          .WithMessage("Url is required.");

        RuleFor(s => s.Description)
          .MaximumLength(200)
          .WithMessage("maximum length is 200.");
    }
}