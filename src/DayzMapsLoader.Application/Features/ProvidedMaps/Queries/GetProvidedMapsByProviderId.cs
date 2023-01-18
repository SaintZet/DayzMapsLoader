using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Domain.Entities;
using MediatR;

namespace DayzMapsLoader.Application.Features.ProvidedMaps.Queries;

public record GetProvidedMapsByProviderIdQuery(int ProviderId) : IRequest<IEnumerable<ProvidedMap>>;

internal class GetProvidedMapsByProviderIdHandler : IRequestHandler<GetProvidedMapsByProviderIdQuery, IEnumerable<ProvidedMap>>
{
    private readonly IProvidedMapsRepository _providedMapsRepository;

    public GetProvidedMapsByProviderIdHandler(IProvidedMapsRepository providedMapsRepository)
    {
        _providedMapsRepository = providedMapsRepository;
    }

    public async Task<IEnumerable<ProvidedMap>> Handle(GetProvidedMapsByProviderIdQuery request, CancellationToken cancellationToken)
    {
        return await _providedMapsRepository.GetAllProvidedMapsByProviderIdAsync(request.ProviderId).ConfigureAwait(false);
    }
}