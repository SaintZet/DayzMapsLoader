﻿using DayzMapsLoader.Domain.Contracts.Services;
using MediatR;

namespace DayzMapsLoader.Core.Features.MapArchive.Queries;

public record GetMapImagePartsArchiveQuery(int ProviderId, int MapId, int TypeId, int Zoom) : IRequest<(byte[] data, string name)>;

internal class GetMapImagePartsArchiveHandler : IRequestHandler<GetMapImagePartsArchiveQuery, (byte[] data, string name)>
{
    private readonly IMapDownloadArchiveService _mapDownloader;

    public GetMapImagePartsArchiveHandler(IMapDownloadArchiveService mapDownloader)
        => _mapDownloader = mapDownloader;

    public async Task<(byte[] data, string name)> Handle(GetMapImagePartsArchiveQuery request, CancellationToken cancellationToken)
        => await _mapDownloader
            .DownloadMapImagePartsArchiveAsync(request.ProviderId, request.MapId, request.TypeId, request.Zoom)
            .ConfigureAwait(false);
}