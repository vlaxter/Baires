using FluentValidation;

namespace Baires.Application.People.Commands.CreatePerson;

public class CreatePersonCommandValidator :AbstractValidator<CreatePersonCommand>
{
    public CreatePersonCommandValidator()
    {
        RuleFor(x => x.PersonId)
            .NotEmpty()
            .GreaterThanOrEqualTo(1)
            .WithMessage("The PersonId field is required  and must be at least greater than or equal to 1.");

        RuleFor(x => x.FirstName)
            .MaximumLength(150)
            .NotEmpty()
            .WithMessage("The First Name field is required and must not exceed 150 characters.");

        RuleFor(x => x.LastName)
            .MaximumLength(150)
            .NotEmpty()
            .WithMessage("The Last Name field is required and must not exceed 150 characters.");

        RuleFor(x => x.CurrentRole)
            .MaximumLength(200)
            .NotEmpty()
            .WithMessage("The Current Role field is required and must not exceed 200 characters.");

        RuleFor(x => x.Country)
            .MaximumLength(150)
            .NotEmpty()
            .WithMessage("The Country field is required and must not exceed 200 characters.");

        RuleFor(x => x.Industry)
            .MaximumLength(200)
            .NotEmpty()
            .WithMessage("The Industry field is required and must not exceed 200 characters.");

        RuleFor(x => x.NumberOfRecommendations)
            .GreaterThanOrEqualTo(0)
            .WithMessage("NumberOfRecommendations at least greater than or equal to 0.");

        RuleFor(x => x.NumberOfConnections)
            .GreaterThanOrEqualTo(0)
            .WithMessage("NumberOfConnections at least greater than or equal to 0.");
    }
}
