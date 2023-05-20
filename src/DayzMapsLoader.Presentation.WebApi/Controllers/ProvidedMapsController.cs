using DayzMapsLoader.Core.Features.ProvidedMaps.Queries;
using DayzMapsLoader.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DayzMapsLoader.Presentation.WebApi.Controllers.ProvidedMaps;

[Route("api/provided-maps")]
[ApiController]
public class ProvidedMapsController : BaseController
{
    public ProvidedMapsController(IMediator mediator)
        : base(mediator) { }

    /// <summary>
    /// Retrieves a list of all available provided maps.
    /// </summary>
    /// <response code="200"> Returns a list of provided maps in json format. </response>
    [HttpGet]
    [Route("get-all")]
    [Produces("application/json")]
    public async Task<ActionResult> GetAllProvidedMaps()
    {
        var providedMaps = await _mediator.Send(new GetProvidedMapsQuery());

        return Ok(providedMaps);
    }
    
    /// <summary>
    /// Get all maps provided by specific map.
    /// </summary>
    /// <param name="mapId"> Map ID. </param>
    [HttpGet]
    [Route("{mapId}/get-by-map")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<ProvidedMap>), 200)]
    public async Task<ActionResult<IEnumerable<ProvidedMap>>> GetProvidedMapsByMapId(int mapId)
    {
        var query = new GetProvidedMapsByMapIdQuery(mapId);
        var maps = await _mediator.Send(query);

        return Ok(maps);
    } 
    
    /// <summary>
    /// Get all maps provided by specific provider.
    /// </summary>
    /// <param name="providerId"> Provider ID. </param>
    /// ///
    /// <response code="200"> Returns a list of provided maps in json format. </response>
    [HttpGet]
    [Route("{providerId}/get-by-provider")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<ProvidedMap>), 200)]
    public async Task<ActionResult<IEnumerable<ProvidedMap>>> GetProvidedMapsByProviderId(int providerId)
    {
        var query = new GetProvidedMapsByProviderIdQuery(providerId);
        var maps = await _mediator.Send(query);

        return Ok(maps);
    }
}