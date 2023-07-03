using Baires.Application.Common.Interfaces;
using MediatR;

namespace Baires.Application.People.Queries.GetTopClients;

public record GetTopClientsQuery : IRequest<IEnumerable<TopClientDto>>
{
    public int Count { get; init; }
}

public class GetNHighestPotentialClientsQueryHandler : IRequestHandler<GetTopClientsQuery, IEnumerable<TopClientDto>>
{
    private readonly IPeopleRepository _peopleRepository;

    public GetNHighestPotentialClientsQueryHandler(IPeopleRepository peopleRepository)
    {
        _peopleRepository = peopleRepository;
    }

    public async Task<IEnumerable<TopClientDto>> Handle(GetTopClientsQuery request, CancellationToken cancellationToken)
    {
        var topClients = await _peopleRepository.GetTopClientsAsync(request.Count, cancellationToken);

        return topClients.Select(x => new TopClientDto() { PersonId = x.PersonId});
    }
}
