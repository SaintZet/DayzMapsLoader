using DayzMapsLoader.Domain.Entities;
using MediatR;

namespace DayzMapsLoader.Application.Features.Maps.Queries.GetMaps;

public record GetMapsQuery : IRequest<IEnumerable<Map>>;