using DayzMapsLoader.Core.Features.MapArchive.Queries;

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DayzMapsLoader.Presentation.WebApi.Controllers;

[Route("api/download-map")]
[ApiController]
public class DownloadMapController : BaseController
{
    public DownloadMapController(IMediator mediator)
        : base(mediator) { }

    /// <summary>
    /// Get map image archive for specific provider, map, type and zoom level.
    /// </summary>
    /// <param name="providerId"> Provider ID. </param>
    /// <param name="mapId"> Map ID. </param>
    /// <param name="typeId"> Type ID. </param>
    /// <param name="zoom"> Zoom level. </param>
    /// <response code="200"> Returns a zip archive file with the requested images. </response>
    [HttpGet]
    [Route("providers/{providerId}/maps/{mapId}/types/{typeId}/zoom/{zoom}/image-archive")]
    [ProducesResponseType(typeof(FileContentResult), 200)]
    public async Task<FileContentResult> GetMapImageArchive(int providerId, int mapId, int typeId, int zoom)
    {
        var query = new GetMapImageArchiveQuery(providerId, mapId, typeId, zoom);
        var (data, name) = await _mediator.Send(query);

        return new FileContentResult(data, "application/zip") { FileDownloadName = name };
    }

    /// <summary>
    /// Get map image parts archive for specific provider, map, type and zoom level.
    /// </summary>
    /// <param name="providerId"> Provider ID. </param>
    /// <param name="mapId"> Map ID. </param>
    /// <param name="typeId"> Type ID. </param>
    /// <param name="zoom"> Zoom level. </param>
    /// <response code="200"> Returns a zip archive file with the requested images. </response>
    [HttpGet]
    [Route("providers/{providerId}/maps/{mapId}/types/{typeId}/zoom/{zoom}/image-parts-archive")]
    [ProducesResponseType(typeof(FileContentResult), 200)]
    public async Task<FileContentResult> GetMapImagePartsArchive(int providerId, int mapId, int typeId, int zoom)
    {
        var query = new GetMapImagePartsArchiveQuery(providerId, mapId, typeId, zoom);
        var (data, name) = await _mediator.Send(query);

        return new FileContentResult(data, "application/zip") { FileDownloadName = name };
    }

    /// <summary>
    /// Get all map images archive for specific provider and zoom level.
    /// </summary>
    /// <param name="providerId"> Provider ID. </param>
    /// <param name="zoom"> Zoom level. </param>
    /// <response code="200"> Returns a zip archive file with all images for the provider. </response>
    [HttpGet]
    [Route("providers/{providerId}/zoom/{zoom}/image-archive")]
    [ProducesResponseType(typeof(FileContentResult), 200)]
    public async Task<FileContentResult> GetAllMapsImagesArchive(int providerId, int zoom)
    {
        var query = new GetAllMapsImagesArchiveQuery(providerId, zoom);
        var (data, name) = await _mediator.Send(query);

        return new FileContentResult(data, "application/zip") { FileDownloadName = name };
    }
}