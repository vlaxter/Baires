using FluentValidation;

namespace Baires.Application.People.Queries.GetTopClients;

public class GetTopClientsQueryValidator : AbstractValidator<GetTopClientsQuery>
{
    public GetTopClientsQueryValidator()
    {
        RuleFor(x => x.Count)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Count is a number at least greater than or equal to 1.");
    }
}
