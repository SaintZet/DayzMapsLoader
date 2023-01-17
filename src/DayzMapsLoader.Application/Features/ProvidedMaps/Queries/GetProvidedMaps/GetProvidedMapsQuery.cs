using DayzMapsLoader.Domain.Entities;
using MediatR;

namespace DayzMapsLoader.Application.Features.ProvidedMaps.Queries.GetProvidedMaps;

public record GetProvidedMapsQuery : IRequest<IEnumerable<ProvidedMap>>;