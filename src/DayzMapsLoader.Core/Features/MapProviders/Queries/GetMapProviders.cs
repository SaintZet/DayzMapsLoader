using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Domain.Entities;

using MediatR;

namespace DayzMapsLoader.Core.Features.MapProviders.Queries;

public record GetMapProvidersQuery : IRequest<IEnumerable<MapProvider>>;

public class GetMapProvidersHandler : IRequestHandler<GetMapProvidersQuery, IEnumerable<MapProvider>>
{
    private readonly IMapProvidersRepository _mapProvidersRepository;

    public GetMapProvidersHandler(IMapProvidersRepository mapProvidersRepository)
        => _mapProvidersRepository = mapProvidersRepository;

    public async Task<IEnumerable<MapProvider>> Handle(GetMapProvidersQuery request, CancellationToken cancellationToken)
        => await _mapProvidersRepository.GetAllMapProvidersAsync().ConfigureAwait(false);
}