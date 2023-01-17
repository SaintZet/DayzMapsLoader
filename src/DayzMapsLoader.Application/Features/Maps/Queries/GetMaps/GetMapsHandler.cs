using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Domain.Entities;
using MediatR;

namespace DayzMapsLoader.Application.Features.Maps.Queries.GetMaps
{
    internal class GetMapsHandler : IRequestHandler<GetMapsQuery, IEnumerable<Map>>
    {
        private readonly IMapsRepository _mapsRepository;

        public GetMapsHandler(IMapsRepository mapsRepository)
        {
            _mapsRepository = mapsRepository;
        }

        public async Task<IEnumerable<Map>> Handle(GetMapsQuery request, CancellationToken cancellationToken)
            => await _mapsRepository.GetAllMapsAsync();
    }
}