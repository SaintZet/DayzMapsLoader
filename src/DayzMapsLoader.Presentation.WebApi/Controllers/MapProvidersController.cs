using DayzMapsLoader.Application.Features.MapProviders.Queries;
using DayzMapsLoader.Application.Features.ProvidedMaps.Queries;
using DayzMapsLoader.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DayzMapsLoader.Presentation.WebApi.Controllers.MapProviders;

[Route("api/map-providers")]
[ApiController]
public class MapProvidersController : BaseController
{
    public MapProvidersController(IMediator mediator)
        : base(mediator) { }

    /// <summary>
    /// Retrieves a list of all available map providers.
    /// </summary>
    /// <response code="200">Returns a list of map providers in json format.</response>
    [HttpGet]
    [Route("get-all")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<MapProvider>), 200)]
    public async Task<ActionResult> GetAllMapProviders()
    {
        var query = new GetMapProvidersQuery();
        var mapProviders = await _mediator.Send(query);

        return Ok(mapProviders);
    }

    /// <summary>
    /// Get all maps provided by specific provider.
    /// </summary>
    /// <param name="providerId">Provider ID.</param>
    /// /// <response code="200">Returns a list of provided maps in json format.</response>
    [HttpGet]
    [Route("{providerId}/provided-maps")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<ProvidedMap>), 200)]
    public async Task<ActionResult<IEnumerable<ProvidedMap>>> GetProvidedMapsByProviderId(int providerId)
    {
        var query = new GetProvidedMapsByProviderIdQuery(providerId);
        var maps = await _mediator.Send(query);

        return Ok(maps);
    }
}