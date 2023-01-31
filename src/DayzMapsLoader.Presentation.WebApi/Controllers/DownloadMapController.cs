using DayzMapsLoader.Application.Features.MapArchive.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DayzMapsLoader.Presentation.WebApi.Controllers;

[Route("api/download-map")]
[ApiController]
public class DownloadMapController : BaseController
{
    public DownloadMapController(IMediator mediator)
        : base(mediator) { }

    [HttpGet("providers/{providerId}/maps/{mapId}/types/{typeId}/zoom/{zoom}/image-archive")]
    public async Task<FileContentResult> GetMapImageArchive(int providerId, int mapId, int typeId, int zoom)
    {
        var query = new GetMapImageArchiveQuery(providerId, mapId, typeId, zoom);
        var (data, name) = await _mediator.Send(query);

        return new FileContentResult(data, "application/zip") { FileDownloadName = name };
    }

    [HttpGet("providers/{providerId}/maps/{mapId}/types/{typeId}/zoom/{zoom}/image-parts-archive")]
    public async Task<FileContentResult> GetMapImagePartsArchive(int providerId, int mapId, int typeId, int zoom)
    {
        var query = new GetMapImagePartsArchiveQuery(providerId, mapId, typeId, zoom);
        var (data, name) = await _mediator.Send(query);

        return new FileContentResult(data, "application/zip") { FileDownloadName = name };
    }

    [HttpGet("providers/{providerId}/zoom/{zoom}/image-archive")]
    public async Task<FileContentResult> GetAllMapsImagesArchive(int providerId, int zoom)
    {
        var query = new GetAllMapsImagesArchiveQuery(providerId, zoom);
        var (data, name) = await _mediator.Send(query);

        return new FileContentResult(data, "application/zip") { FileDownloadName = name };
    }
}