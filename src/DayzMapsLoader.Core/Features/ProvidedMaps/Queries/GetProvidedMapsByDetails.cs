using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Domain.Entities;

using MediatR;

namespace DayzMapsLoader.Core.Features.ProvidedMaps.Queries;

public record GetProvidedMapsQueryByDetailsQuery(int ProviderId, int MapId, int TypeId) : IRequest<ProvidedMap>;

internal class GetProvidedMapsByDetailsHandler : IRequestHandler<GetProvidedMapsQueryByDetailsQuery, ProvidedMap>
{
    private readonly IProvidedMapsRepository _providedMapsRepository;

    public GetProvidedMapsByDetailsHandler(IProvidedMapsRepository providedMapsRepository)
    {
        _providedMapsRepository = providedMapsRepository;
    }

    public async Task<ProvidedMap> Handle(GetProvidedMapsQueryByDetailsQuery request, CancellationToken cancellationToken)
        => await _providedMapsRepository.GetProvidedMapAsync(request.ProviderId, request.MapId, request.TypeId).ConfigureAwait(false);
}