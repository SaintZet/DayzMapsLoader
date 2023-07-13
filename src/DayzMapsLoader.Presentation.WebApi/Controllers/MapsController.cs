using DayzMapsLoader.Core.Features.Maps.Queries;
using DayzMapsLoader.Core.Features.ProvidedMaps.Queries;
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
    /// <response code="200"> Returns a list of maps in json format. </response>
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
    /// Get maps by specific provider.
    /// </summary>
    /// <param name="providerId"> Provider ID. </param>
    [HttpGet]
    [Route("provider={providerId:int}&maps")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<Map>), 200)]
    public async Task<ActionResult<IEnumerable<Map>>> GetMapsByProviderId(int providerId)
    {
        var query = new GetMapsByProviderIdQuery(providerId);
        var maps = await _mediator.Send(query);

        return Ok(maps);
    }
}