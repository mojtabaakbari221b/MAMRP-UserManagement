﻿namespace UserManagement.Application.ApplicationServices.Sections.Commands.Add;

public class AddMenuCommandValidator : AbstractValidator<AddSectionCommandRequest>
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