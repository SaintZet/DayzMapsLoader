using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Domain.Entities;

using MediatR;

namespace DayzMapsLoader.Core.Features.ProvidedMaps.Queries;

public record GetProvidedMapsByProviderIdQuery(int ProviderId) : IRequest<IEnumerable<ProvidedMap>>;

internal class GetProvidedMapsByProviderIdHandler : IRequestHandler<GetProvidedMapsByProviderIdQuery, IEnumerable<ProvidedMap>>
{
    private readonly IProvidedMapsRepository _providedMapsRepository;

    public GetProvidedMapsByProviderIdHandler(IProvidedMapsRepository providedMapsRepository)
        => _providedMapsRepository = providedMapsRepository;

    public async Task<IEnumerable<ProvidedMap>> Handle(GetProvidedMapsByProviderIdQuery request, CancellationToken cancellationToken)
        => await _providedMapsRepository
            .GetAllProvidedMapsByProviderIdAsync(request.ProviderId)
            .ConfigureAwait(false);
}