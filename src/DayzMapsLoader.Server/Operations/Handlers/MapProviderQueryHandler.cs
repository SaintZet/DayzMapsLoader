using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Domain.Entities.MapProvider;
using DayzMapsLoader.Server.Operations.Queries;
using MediatR;

namespace DayzMapsLoader.Server.Operations.Handlers;

public class MapProviderQueryHandler : IRequestHandler<MapProviderQuery, IList<MapProvider>>
{

    private readonly IMapsDbContext _dbContext;
    
    public MapProviderQueryHandler(IMapsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<MapProvider>> Handle(MapProviderQuery request, CancellationToken cancellationToken)
    {
        return _dbContext.GetMapProviders();
    }
}