using DayzMapsLoader.Application.Features.MapProviders.Queries.GetMapProviders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DayzMapsLoader.Presentation.WebApi.Controllers.MapProviders
{
    [ApiController]
    [Route("api/map-providers")]
    public class MapProvidersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MapProvidersController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [Route("get-all")]
        public async Task<ActionResult> GetMapProviders()
        {
            var mapProviders = await _mediator.Send(new GetMapProvidersQuery());

            return Ok(mapProviders);
        }
    }
}