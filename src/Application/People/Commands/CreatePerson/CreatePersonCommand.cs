using Baires.Application.Common.Exceptions;
using Baires.Application.Common.Interfaces;
using Baires.Domain.Entities;
using Baires.Domain.Events;
using MediatR;

namespace Baires.Application.People.Commands.CreatePerson;

public record CreatePersonCommand : IRequest<int>
{
    public long PersonId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CurrentRole { get; set; }
    public string? Country { get; set; }
    public string? Industry { get; set; }
    public int NumberOfRecommendations { get; set; }
    public int NumberOfConnections { get; set; }
}

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, int>
{
    private readonly IPeopleRepository _peopleRepository;
    private readonly IMLModel<Person> _personMLModel;

    public CreatePersonCommandHandler(IPeopleRepository peopleRepository, IMLModel<Person> personMLModel)
    {
        _peopleRepository = peopleRepository;
        _personMLModel = personMLModel;
    }

    public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        if (await _peopleRepository.ExistsAsync(request.PersonId, cancellationToken))
        {
            throw new BadRequestException($"PersonId {request.PersonId} already exists.");
        }

        var entity = new Person
        {
            PersonId = request.PersonId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            CurrentRole = request.CurrentRole,
            Country = request.Country,
            Industry = request.Industry,
            NumberOfRecommendations = request.NumberOfRecommendations,
            NumberOfConnections = request.NumberOfConnections,
        };

        entity.PriorityIndex = _personMLModel.PredictIndex(entity);

        entity.AddDomainEvent(new PersonCreatedEvent(entity));

        await _peopleRepository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
