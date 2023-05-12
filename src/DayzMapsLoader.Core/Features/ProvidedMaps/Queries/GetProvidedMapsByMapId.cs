using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Domain.Entities;

using MediatR;

namespace DayzMapsLoader.Core.Features.ProvidedMaps.Queries;

public record GetProvidedMapsByMapIdQuery(int ProviderId) : IRequest<IEnumerable<ProvidedMap>>;

internal class GetProvidedMapsByMapIdHandler : IRequestHandler<GetProvidedMapsByProviderIdQuery, IEnumerable<ProvidedMap>>
{
    private readonly IProvidedMapsRepository _providedMapsRepository;

    public GetProvidedMapsByMapIdHandler(IProvidedMapsRepository providedMapsRepository)
        => _providedMapsRepository = providedMapsRepository;

    public async Task<IEnumerable<ProvidedMap>> Handle(GetProvidedMapsByProviderIdQuery request, CancellationToken cancellationToken)
        => await _providedMapsRepository
            .GetAllProvidedMapsByMapIdAsync(request.ProviderId)
            .ConfigureAwait(false);
}