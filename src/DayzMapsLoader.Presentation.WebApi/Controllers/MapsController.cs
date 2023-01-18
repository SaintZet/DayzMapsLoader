using DayzMapsLoader.Application.Features.Maps.Queries;
using DayzMapsLoader.Application.Features.ProvidedMaps.Queries;
using DayzMapsLoader.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DayzMapsLoader.Presentation.WebApi.Controllers.Maps;

[Route("api/maps")]
[ApiController]
public class MapsController : BaseController
{
    public MapsController(IMediator mediator)
        : base(mediator) { }

    /// <summary>
    /// Retrieves a list of all available maps.
    /// </summary>
    /// <response code="200">Returns a list of maps in json format.</response>
    [HttpGet]
    [Route("get-all")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<Map>), 200)]
    public async Task<ActionResult> GetAllMaps()
    {
        var maps = await _mediator.Send(new GetMapsQuery());

        return Ok(maps);
    }

    /// <summary>
    /// Get all maps provided by specific map.
    /// </summary>
    /// <param name="mapId">Map ID.</param>
    [HttpGet]
    [Route("{mapId}/provided-maps")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<ProvidedMap>), 200)]
    public async Task<ActionResult<IEnumerable<ProvidedMap>>> GetProvidedMapsByMapId(int mapId)
    {
        var query = new GetProvidedMapsByMapIdQuery(mapId);
        var maps = await _mediator.Send(query);

        return Ok(maps);
    }
}