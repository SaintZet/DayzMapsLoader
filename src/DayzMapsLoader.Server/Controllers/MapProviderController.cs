using DayzMapsLoader.Domain.Entities.MapProvider;
using DayzMapsLoader.Server.Operations.Handlers;
using DayzMapsLoader.Server.Operations.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DayzMapsLoader.Server.Controllers;

[Route("api/providers")]
[ApiController]
public class Controller : ControllerBase
{
    private readonly IMediator _mediator;

    public Controller(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IList<MapProvider>> Get()
    {
        return await _mediator.Send(new MapProviderQuery());
    }
}