using DayzMapsLoader.Core.Contracts.Services;

using MediatR;

namespace DayzMapsLoader.Core.Features.MapArchive.Queries;

public record GetMapImageArchiveQuery(int ProviderId, int MapId, int TypeId, int Zoom) : IRequest<(byte[] data, string name)>;

internal class GetMapImageArchiveHandler : IRequestHandler<GetMapImageArchiveQuery, (byte[] data, string name)>
{
    private readonly IMapDownloadArchiveService _mapDownloader;

    public GetMapImageArchiveHandler(IMapDownloadArchiveService mapDownloader)
        => _mapDownloader = mapDownloader;

    public async Task<(byte[] data, string name)> Handle(GetMapImageArchiveQuery request, CancellationToken cancellationToken)
        => await _mapDownloader
            .DownloadMapImageArchiveAsync(request.ProviderId, request.MapId, request.TypeId, request.Zoom)
            .ConfigureAwait(false);
}