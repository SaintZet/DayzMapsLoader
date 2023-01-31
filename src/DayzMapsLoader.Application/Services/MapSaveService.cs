//using DayzMapsLoader.Application.Abstractions.Infrastructure;
//using DayzMapsLoader.Application.Abstractions.Services;
//using DayzMapsLoader.Application.Enums;
//using DayzMapsLoader.Application.Helpers;
//using DayzMapsLoader.Domain.Entities;
//using System;

//namespace DayzMapsLoader.Application.Services;

//public class MapSaver : BaseMapService, IMapSaver
//{
//    public MapSaver(IMapsDbContext mapsDbContext) : base(mapsDbContext)
//    {
//    }

//    public ImageExtension ImageExtansionForSave { get; set; } = ImageExtension.jpg;

//    public string SaveMap(string pathToSave, int providerId, int mapID, int typeId, int zoom)
//    {
//        ProvidedMap map = _mapsDbContext.GetProvidedMap(providerId, mapID, typeId);

//        var mapParts = _providerManager.GetMapParts(map, zoom);

//        Enum.TryParse(map.ImageExtension, true, out ImageExtension extension);

//        var image = _imageMerger.Merge(mapParts, extension);

//        string path = $@"{pathToSave}\{_providerManager}\{map.Map.Name}\{map.Type.Name}\{map.Version}\{zoom}";

//        return ImageSaver.SaveImageToHardDisk(image, path, ImageExtansionForSave);
//    }

//    public string SaveMapInParts(string pathToSave, int providerId, int mapID, int typeId, int zoom)
//    {
//        MapProvider provider = _mapsDbContext.GetMapProviderById(providerId);

//        ProvidedMap map = _mapsDbContext.GetProvidedMap(provider.Id, mapID, typeId);

//        var mapParts = _providerManager.GetMapParts(map, zoom);

//        string path = $@"{pathToSave}\{provider.Name}\{map.Map.Name}\{map.Type.Name}\{map.Version}\{zoom}";

//        return ImageSaver.SaveImageToHardDisk(mapParts, path, ImageExtansionForSave);
//    }

//    public IEnumerable<string> SaveAllMaps(string pathToSave, int providerId, int typeId, int zoom)
//    {
//        List<string> result = new();

//        MapProvider provider = _mapsDbContext.GetMapProviderById(providerId);

//        Parallel.ForEach(_mapsDbContext.GetProvidedMapsByProviderId(provider.Id), map =>
//        {
//            var path = SaveMap(pathToSave, provider.Id, map.Id, typeId, zoom);

//            result.Add(path);
//        });

//        return result;
//    }
//}