using RequestsHub.Domain.Contracts;
using RequestsHub.Domain.DataTypes;
using RequestsHub.Domain.MapsProviders;

namespace RequestsHub.Domain.Services
{
    internal class QueryBuilder
    {
        private string queryTemplate;
        private string mapName;
        private string version;
        private string type;
        private string zoom;
        private string extension;

        public QueryBuilder(IMapProvider mapProvider, IMap currentMap, TypeMap typeMap, int zoom)
        {
            mapName = currentMap.MapNameForProvider;
            version = currentMap.Version;
            type = typeMap.ToString();
            extension = currentMap.MapExtension.ToString();
            this.zoom = zoom.ToString();

            queryTemplate = BuildQueryBase(mapProvider, currentMap, typeMap, zoom);
        }

        public string BuildQueryBase(IMapProvider mapProvider, IMap currentMap, TypeMap typeMap, int zoom)
        {
            switch (mapProvider.Name)
            {
                case MapProvider.xam:
                    return @"https://static.xam.nu/dayz/maps/{0}/{1}/{2}/{3}/{4}/{5}.{6}";

                    //case MapProvider.ginfo:
                    //    string ginfoTypeMap;
                    //    switch (typeMap)
                    //    {
                    //        case TypeMap.topographic:
                    //            ginfoTypeMap = "Top";
                    //            break;

                    // case TypeMap.satellite: ginfoTypeMap = "Sat"; break;

                    // case TypeMap.tourist: default: throw new
                    // NotImplementedException("RequestsHub.Domain.Services.QueryBuilder.GetQuery:
                    // Not support this type!"); } return @"https://maps.izurvive.com/maps/{currentMap.MapNameForProvider}-{ginfoTypeMap}/{currentMap.Version}/tiles/{zoom}/{i}/{j}.{currentMap.MapExtension}";
            }
            throw new NotImplementedException("RequestsHub.Domain.Services.QueryBuilder.GetQuery: Add new provider in this method!");
        }

        internal string GetQuery(int i, int j)
        {
            return string.Format(queryTemplate, mapName, version, type, zoom, i.ToString(), j.ToString(), extension);
        }
    }
}