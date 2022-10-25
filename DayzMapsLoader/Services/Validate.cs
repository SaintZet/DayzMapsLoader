using DayzMapsLoader.Contracts;
using DayzMapsLoader.DataTypes;
using System.Reflection;

namespace DayzMapsLoader.Services;

internal static class Validate
{
    public static bool CheckTypeAtMap(IMap currentMap, MapType typeMap)
    {
        foreach (var type in currentMap.TypesMap)
        {
            if (type == typeMap)
            {
                return true;
            }
        }
        //TODO:Write error message.
        throw new ArgumentException("");
    }

    public static bool CheckMapAtProvider(IMapProvider mapProvider, MapName nameMap)
    {
        foreach (var map in mapProvider.Maps)
        {
            if (map.Name == nameMap)
            {
                return true;
            }
        }
        //TODO:Write error message.
        throw new ArgumentException("");
    }

    internal static bool CheckZoomAtMap(IMap currentMap, int zoom)
    {
        foreach (var item in currentMap.ZoomLevelRatioToSize)
        {
            if (item.Key == zoom)
            {
                return true;
            }
        }
        //TODO:Write error message.
        throw new ArgumentException("");
    }

    //TODO: write comment.
    internal static string PathToSave(string pathsToSave)
    {
        if (Directory.Exists(pathsToSave))
        {
            return pathsToSave;
        }
        return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            ?? throw new ArgumentNullException("Can't get a path to save.");
    }
}