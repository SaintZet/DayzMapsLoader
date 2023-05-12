using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Domain.Entities;

using MediatR;

namespace DayzMapsLoader.Core.Features.ProvidedMaps.Queries;

public record GetProvidedMapsByMapIdQuery(int MapId) : IRequest<IEnumerable<ProvidedMap>>;

internal class GetProvidedMapsByMapIdHandler : IRequestHandler<GetProvidedMapsByMapIdQuery, IEnumerable<ProvidedMap>>
{
    private readonly IProvidedMapsRepository _providedMapsRepository;

    public GetProvidedMapsByMapIdHandler(IProvidedMapsRepository providedMapsRepository)
        => _providedMapsRepository = providedMapsRepository;

    public async Task<IEnumerable<ProvidedMap>> Handle(GetProvidedMapsByMapIdQuery request, CancellationToken cancellationToken)
        => await _providedMapsRepository
            .GetAllProvidedMapsByMapIdAsync(request.MapId)
            .ConfigureAwait(false);
}