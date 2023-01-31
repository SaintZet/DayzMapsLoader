using DayzMapsLoader.Application.Abstractions.Services;
using MediatR;

namespace DayzMapsLoader.Application.Features.MapArchive.Queries;

public record GetMapImagePartsArchiveQuery(int ProviderId, int MapId, int TypeId, int Zoom) : IRequest<(byte[] data, string name)>;

internal class GetMapImagePartsArchiveHandler : IRequestHandler<GetMapImagePartsArchiveQuery, (byte[] data, string name)>
{
    private readonly IMapDownloadService _mapDownloader;

    public GetMapImagePartsArchiveHandler(IMapDownloadService mapDownloader)
    {
        _mapDownloader = mapDownloader;
    }

    public async Task<(byte[] data, string name)> Handle(GetMapImagePartsArchiveQuery request, CancellationToken cancellationToken)
    {
        return await _mapDownloader
            .DownloadMapImagePartsArchiveAsync(request.ProviderId, request.MapId, request.TypeId, request.Zoom)
            .ConfigureAwait(false);
    }
}