using DayzMapsLoader.Domain.Entities;
using MediatR;

namespace DayzMapsLoader.Application.Features.MapProviders.Queries.GetMapProviders;

public record GetMapProvidersQuery : IRequest<IEnumerable<MapProvider>>;