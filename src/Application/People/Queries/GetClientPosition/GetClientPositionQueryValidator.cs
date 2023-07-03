using FluentValidation;

namespace Baires.Application.People.Queries.GetClientPosition;
public class GetClientPositionQueryValidator : AbstractValidator<GetClientPositionQuery>
{
    public GetClientPositionQueryValidator()
    {
        RuleFor(x => x.PersonId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PersonId is a number at least greater than or equal to 1.");
    }
}
