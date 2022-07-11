using RequestsHub.Domain.Contracts;
using RequestsHub.Domain.DataTypes;
using RequestsHub.Domain.MapsProviders;

namespace RequestsHub.Domain.Services
{
    internal class QueryBuilder
    {
        private readonly string queryTemplate;
        private readonly string mapName;
        private readonly string version;
        private readonly string type;
        private readonly string zoom;
        private readonly string extension;
        private readonly IMapProvider mapProvider;

        public QueryBuilder(IMapProvider mapProvider, IMap currentMap, TypeMap typeMap, int zoom)
        {
            this.mapProvider = mapProvider;
            this.zoom = zoom.ToString();

            mapName = currentMap.MapNameForProvider;
            version = currentMap.Version;
            extension = currentMap.MapExtension.ToString();
            type = GetTypeMap(typeMap)!;
            queryTemplate = BuildQueryTemplate();
        }

        public string BuildQueryTemplate()
        {
            switch (mapProvider.Name)
            {
                case MapProvider.xam:
                    return @"https://static.xam.nu/dayz/maps/{0}/{1}/{2}/{3}/{4}/{5}.{6}";

                case MapProvider.ginfo:
                    return @"https://maps.izurvive.com/maps/{0}-{1}/{2}/tiles/{3}/{4}/{5}.{6}";
            }
            throw new NotImplementedException("RequestsHub.Domain.Services.QueryBuilder.BuildQueryBase: Add new provider in this method!");
        }

        internal string GetQuery(int i, int j)
        {
            switch (mapProvider.Name)
            {
                case MapProvider.xam:
                    return string.Format(queryTemplate, mapName, version, type, zoom, i.ToString(), j.ToString(), extension);

                case MapProvider.ginfo:
                    return string.Format(queryTemplate, mapName, type, version, zoom, i.ToString(), j.ToString(), extension);
            }
            throw new NotImplementedException("RequestsHub.Domain.Services.QueryBuilder.GetQuery: Add new provider in this method!");
        }

        private string? GetTypeMap(TypeMap typeMap)
        {
            switch (mapProvider.Name)
            {
                case MapProvider.xam:
                    return typeMap.ToString(); ;

                case MapProvider.ginfo:
                    switch (typeMap)
                    {
                        case TypeMap.topographic:
                            return "Top";

                        case TypeMap.satellite:
                            return "Sat";

                        case TypeMap.tourist:
                        default:
                            throw new NotImplementedException("RequestsHub.Domain.Services.QueryBuilder.GetTypeMap: Not support this type!");
                    }
            }
            throw new NotImplementedException("RequestsHub.Domain.Services.QueryBuilder.GetTypeMap: Add new provider in this method!");
        }
    }
}