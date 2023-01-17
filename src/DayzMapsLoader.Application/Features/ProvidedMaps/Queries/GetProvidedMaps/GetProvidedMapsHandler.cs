using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Domain.Entities;
using MediatR;

namespace DayzMapsLoader.Application.Features.ProvidedMaps.Queries.GetProvidedMaps
{
    internal class GetProvidedMapsHandler : IRequestHandler<GetProvidedMapsQuery, IEnumerable<ProvidedMap>>
    {
        private readonly IProvidedMapsRepository _providedMapsRepository;

        public GetProvidedMapsHandler(IProvidedMapsRepository providedMapsRepository)
        {
            _providedMapsRepository = providedMapsRepository;
        }

        public async Task<IEnumerable<ProvidedMap>> Handle(GetProvidedMapsQuery request, CancellationToken cancellationToken)
        {
            return await _providedMapsRepository.GetAllProvidedMapsAsync();
        }
    }
}