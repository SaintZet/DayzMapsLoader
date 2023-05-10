using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Domain.Entities;

using MediatR;

namespace DayzMapsLoader.Core.Features.ProvidedMaps.Queries;

public record GetProvidedMapsQuery : IRequest<IEnumerable<ProvidedMap>>;

internal class GetProvidedMapsHandler : IRequestHandler<GetProvidedMapsQuery, IEnumerable<ProvidedMap>>
{
    private readonly IProvidedMapsRepository _providedMapsRepository;

    public GetProvidedMapsHandler(IProvidedMapsRepository providedMapsRepository)
        => _providedMapsRepository = providedMapsRepository;

    public async Task<IEnumerable<ProvidedMap>> Handle(GetProvidedMapsQuery request, CancellationToken cancellationToken)
        => await _providedMapsRepository.GetAllProvidedMapsAsync().ConfigureAwait(false);
}