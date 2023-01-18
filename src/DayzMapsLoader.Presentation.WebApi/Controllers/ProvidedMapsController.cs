using DayzMapsLoader.Application.Features.ProvidedMaps.Queries;
using DayzMapsLoader.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DayzMapsLoader.Presentation.WebApi.Controllers.ProvidedMaps
{
    [Route("api/provided-maps")]
    [ApiController]
    public class ProvidedMapsController : BaseController
    {
        public ProvidedMapsController(IMediator mediator)
            : base(mediator) { }

        /// <summary>
        /// Retrieves a list of all available provided maps.
        /// </summary>
        /// <response code="200">Returns a list of provided maps in json format.</response>
        [HttpGet]
        [Route("get-all")]
        [Produces("application/json")]
        public async Task<ActionResult> GetAllProvidedMaps()
        {
            var providedMaps = await _mediator.Send(new GetProvidedMapsQuery());

            return Ok(providedMaps);
        }
    }
}