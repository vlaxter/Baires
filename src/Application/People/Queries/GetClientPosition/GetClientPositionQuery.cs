using Baires.Application.Common.Exceptions;
using Baires.Application.Common.Interfaces;
using MediatR;

namespace Baires.Application.People.Queries.GetClientPosition;

public record GetClientPositionQuery : IRequest<ClientPositionDto>
{
    public long PersonId { get; set; }
}

public class GetClientPositionQueryHandler : IRequestHandler<GetClientPositionQuery, ClientPositionDto>
{
    private readonly IPeopleRepository _peopleRepository;

    public GetClientPositionQueryHandler(IPeopleRepository peopleRepository)
    {
        _peopleRepository = peopleRepository;
    }

    public async Task<ClientPositionDto> Handle(GetClientPositionQuery request, CancellationToken cancellationToken)
    {
        if (!await _peopleRepository.ExistsAsync(request.PersonId, cancellationToken))
        {
            throw new NotFoundException($"PersonId {request.PersonId} was not found");
        }

        var position = await _peopleRepository.GetClientPositionAsync(request.PersonId, cancellationToken);

        return new ClientPositionDto { Position = position };
    }
}
