using Baires.Application.People.Commands.CreatePerson;
using Baires.Application.People.Queries.GetClientPosition;
using Baires.Application.People.Queries.GetTopClients;
using Microsoft.AspNetCore.Mvc;

namespace Baires.Api.Controllers;

public class PeopleController : ApiControllerBase
{
    [HttpGet("topclients/{count}")]
    public async Task<ActionResult<IEnumerable<TopClientDto>>> GetTopClients([FromRoute] int count)
    {
        var query = new GetTopClientsQuery() { Count = count };
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("clientposition/{personId}")]
    public async Task<ActionResult<ClientPositionDto>> GetClientPosition([FromRoute] long personId)
    {
        var query = new GetClientPositionQuery() { PersonId = personId };
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreatePersonCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }
}
