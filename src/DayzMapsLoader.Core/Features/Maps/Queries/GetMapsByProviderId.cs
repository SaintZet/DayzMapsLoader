using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Domain.Entities;

using MediatR;

namespace DayzMapsLoader.Core.Features.Maps.Queries;

public record GetMapsByProviderId(int ProviderId) : IRequest<IEnumerable<Map>>;

internal class GetMapsByProviderIdHandler : IRequestHandler<GetMapsByProviderId, IEnumerable<Map>>
{
    private readonly IProvidedMapsRepository _providedMapsRepository;

    public GetMapsByProviderIdHandler(IProvidedMapsRepository providedMapsRepository)
        => _providedMapsRepository = providedMapsRepository;

    public async Task<IEnumerable<Map>> Handle(GetMapsByProviderId request, CancellationToken cancellationToken)
        => await _providedMapsRepository
            .GetMapsByProviderId(request.ProviderId);
}