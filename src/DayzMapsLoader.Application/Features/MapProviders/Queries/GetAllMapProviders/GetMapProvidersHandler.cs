using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Domain.Entities;
using MediatR;

namespace DayzMapsLoader.Application.Features.MapProviders.Queries.GetMapProviders
{
    public class GetMapProvidersHandler : IRequestHandler<GetMapProvidersQuery, IEnumerable<MapProvider>>
    {
        private readonly IMapProvidersRepository _mapProvidersRepository;

        public GetMapProvidersHandler(IMapProvidersRepository mapProvidersRepository)
        {
            _mapProvidersRepository = mapProvidersRepository;
        }

        public async Task<IEnumerable<MapProvider>> Handle(GetMapProvidersQuery request, CancellationToken cancellationToken)
            => await _mapProvidersRepository.GetAllMapProvidersAsync();
    }
}