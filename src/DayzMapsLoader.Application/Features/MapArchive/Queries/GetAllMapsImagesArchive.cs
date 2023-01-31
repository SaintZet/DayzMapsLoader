using DayzMapsLoader.Application.Abstractions.Services;
using MediatR;

namespace DayzMapsLoader.Application.Features.MapArchive.Queries;

public record GetAllMapsImagesArchiveQuery(int ProviderId, int Zoom) : IRequest<(byte[] data, string name)>;

internal class GetAllMapsImagesArchiveHandler : IRequestHandler<GetAllMapsImagesArchiveQuery, (byte[] data, string name)>
{
    private readonly IMapDownloadArchiveService _mapDownloader;

    public GetAllMapsImagesArchiveHandler(IMapDownloadArchiveService mapDownloader)
    {
        _mapDownloader = mapDownloader;
    }

    public async Task<(byte[] data, string name)> Handle(GetAllMapsImagesArchiveQuery request, CancellationToken cancellationToken)
    {
        return await _mapDownloader.DownloadAllMapsImagesArchiveAsync(request.ProviderId, request.Zoom).ConfigureAwait(false);
    }
}