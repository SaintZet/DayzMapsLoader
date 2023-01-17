using DayzMapsLoader.Application.Abstractions.Services;
using DayzMapsLoader.Application.Features.MapProviders.Queries.GetMapProviders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DayzMapsLoader.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapProviderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MapProviderController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult> Get()
            => Ok(await _mediator.Send(new GetMapProvidersQuery()));
    }
}