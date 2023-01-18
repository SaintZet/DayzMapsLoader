using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Domain.Entities;
using MediatR;

namespace DayzMapsLoader.Application.Features.ProvidedMaps.Queries;

public record GetProvidedMapsByMapIdQuery(int ProviderId) : IRequest<IEnumerable<ProvidedMap>>;

internal class GetProvidedMapsByMapIdHandler : IRequestHandler<GetProvidedMapsByProviderIdQuery, IEnumerable<ProvidedMap>>
{
    private readonly IProvidedMapsRepository _providedMapsRepository;

    public GetProvidedMapsByMapIdHandler(IProvidedMapsRepository providedMapsRepository)
    {
        _providedMapsRepository = providedMapsRepository;
    }

    public async Task<IEnumerable<ProvidedMap>> Handle(GetProvidedMapsByProviderIdQuery request, CancellationToken cancellationToken)
    {
        return await _providedMapsRepository.GetAllProvidedMapsByMapIdAsync(request.ProviderId).ConfigureAwait(false);
    }
}