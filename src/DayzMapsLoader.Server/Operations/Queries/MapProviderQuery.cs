using DayzMapsLoader.Domain.Entities.MapProvider;
using MediatR;

namespace DayzMapsLoader.Server.Operations.Queries;

public sealed record MapProviderQuery : IRequest<IList<MapProvider>>
{
}