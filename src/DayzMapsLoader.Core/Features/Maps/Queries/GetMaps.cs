﻿using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Domain.Entities;

using MediatR;

namespace DayzMapsLoader.Core.Features.Maps.Queries;

public record GetMapsQuery : IRequest<IEnumerable<Map>>;

internal class GetMapsHandler : IRequestHandler<GetMapsQuery, IEnumerable<Map>>
{
    private readonly IMapsRepository _mapsRepository;

    public GetMapsHandler(IMapsRepository mapsRepository)
        => _mapsRepository = mapsRepository;

    public async Task<IEnumerable<Map>> Handle(GetMapsQuery request, CancellationToken cancellationToken)
        => await _mapsRepository.GetAllMapsAsync().ConfigureAwait(false);
}