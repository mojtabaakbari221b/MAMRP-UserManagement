using FluentValidation;
using MediatR;
using UserManagement.Domain.Entities;


public class AddMenuCommand() : IRequest<int> {
    public long GroupId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Description { get; set; }
}

public class AddMenuCommandValidator : AbstractValidator<AddMenuCommand>
{
    public AddMenuCommandValidator()
    {
        RuleFor(menu => menu.GroupId)
          .NotEmpty()
          .WithMessage("Group Id is required.");

        RuleFor(user => user.Name)
          .NotEmpty()
          .MaximumLength(50)
          .WithMessage("Name is required.");

        RuleFor(user => user.Url)
          .NotEmpty()
          .MaximumLength(200)
          .WithMessage("Url is required.");

        RuleFor(user => user.Description)
          .MaximumLength(200)
          .WithMessage("maximum length is 200.");
    }
}