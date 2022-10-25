﻿namespace DayzMapsLoader.MapProviders.Helpers;

internal static class MapProviderFactory
{
    public static IMapProvider Create(MapProviderName mapProviderName)
    {
        switch (mapProviderName)
        {
            case MapProviderName.xam:
                return new Xam();

            case MapProviderName.ginfo:
                return new Ginfo();

            default:
                throw new NotImplementedException();
        }
    }
}