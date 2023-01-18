using DayzMapsLoader.Application.Features.MapProviders.Queries.GetMapProviders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace DayzMapsLoader.Presentation.WebApi.Controllers.MapProviders
{
    /// <summary>
    /// Map providers controller
    /// </summary>
    [ApiController]
    [Route("api/map-providers")]
    public class MapProvidersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MapProvidersController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// My best summary!
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-all")]
        public async Task<ActionResult> GetMapProviders()
        {
            var mapProviders = await _mediator.Send(new GetMapProvidersQuery());

            return Ok(mapProviders);
        }
    }
}