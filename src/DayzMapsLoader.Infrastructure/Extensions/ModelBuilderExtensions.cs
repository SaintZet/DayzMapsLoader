using DayzMapsLoader.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace DayzMapsLoader.Infrastructure.Extensions;

internal static class ModelBuilderExtensions
{
    public static ModelBuilder InitializeData(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .InitializeProviders()
            .InitializeMaps()
            .InitializeMapTypes()
            .InitializeProvidedMaps();

        return modelBuilder;
    }

    private static ModelBuilder InitializeProviders(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MapProvider>().HasData(
            new MapProvider
            {
                Id = 1,
                Name = "Xam",
                Link = "https://xam.nu/",
                UrlQueryTemplate = "https://static.xam.nu/dayz/maps/{Map.NameForProvider}/{Map.Version}/{Map.MapTypeForProvider}/{Zoom}/{X}/{Y}.{Map.ImageExtension}"
            },
            new MapProvider
            {
                Id = 2,
                Name = "GInfo",
                Link = "https://ginfo.gg/",
                UrlQueryTemplate = "https://maps.izurvive.com/maps/{Map.NameForProvider}-{Map.MapTypeForProvider}/{Map.Version}/tiles/{Zoom}/{X}/{Y}.{Map.ImageExtension}"
            }
        );

        return modelBuilder;
    }

    private static ModelBuilder InitializeMaps(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Map>().HasData(
            new Map
            {
                Id = 1,
                Name = "Chernarus",
                Author = "Bohemia Interactive",
                Link = "https://www.bohemia.net",
                LastUpdate = new DateTime(2022, 10, 18),
            },
            new Map
            {
                Id = 2,
                Name = "Livonia",
                Author = "Bohemia Interactive",
                Link = "https://www.bohemia.net",
                LastUpdate = new DateTime(2022, 10, 18),
            },
            new Map
            {
                Id = 3,
                Name = "Namalsk",
                Author = "Sumrak",
                Link = "https://steamcommunity.com/workshop/filedetails/?id=2289456201",
                LastUpdate = new DateTime(2022, 01, 31),
            },
            new Map
            {
                Id = 4,
                Name = "Esseker",
                Author = "RonhillUltra",
                Link = "https://steamcommunity.com/sharedfiles/filedetails/?id=2462896799",
                LastUpdate = new DateTime(2021, 07, 20),
            },
            new Map
            {
                Id = 5,
                Name = "Takistan",
                Author = "CypeRevenge",
                Link = "https://steamcommunity.com/sharedfiles/filedetails/?id=2563233742",
                LastUpdate = new DateTime(2022, 10, 18),
            },
            new Map
            {
                Id = 6,
                Name = "Banov",
                Author = "KubeloLive",
                Link = "https://steamcommunity.com/sharedfiles/filedetails/?id=2415195639",
                LastUpdate = new DateTime(2022, 10, 18),
            }
        );

        return modelBuilder;
    }

    private static ModelBuilder InitializeMapTypes(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MapType>()
            .HasData(
            new MapType
            {
                Id = 1,
                Name = "topographic",
            },
            new MapType
            {
                Id = 2,
                Name = "satellite",
            }
        );

        return modelBuilder;
    }

    private static ModelBuilder InitializeProvidedMaps(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProvidedMap>()
            .HasData(

                new
                {
                    Id = 1,
                    MapProviderId = 1,
                    MapId = 1,
                    MapTypeId = 1,
                    NameForProvider = "chernarusplus",
                    MapTypeForProvider = "topographic",
                    MaxMapLevel = 7,
                    IsFirstQuadrant = false,
                    Version = "1.17-1",
                    ImageExtension = "jpg",
                },
                new
                {
                    Id = 2,
                    MapProviderId = 1,
                    MapId = 1,
                    MapTypeId = 2,
                    NameForProvider = "chernarusplus",
                    MapTypeForProvider = "satellite",
                    MaxMapLevel = 7,
                    IsFirstQuadrant = false,
                    Version = "1.17-1",
                    ImageExtension = "jpg",
                },
                new
                {
                    Id = 3,
                    MapProviderId = 1,
                    MapId = 2,
                    MapTypeId = 1,
                    NameForProvider = "livonia",
                    MapTypeForProvider = "topographic",
                    MaxMapLevel = 7,
                    IsFirstQuadrant = false,
                    Version = "1.17-1",
                    ImageExtension = "jpg",
                },
                new
                {
                    Id = 4,
                    MapProviderId = 1,
                    MapId = 2,
                    MapTypeId = 2,
                    NameForProvider = "livonia",
                    MapTypeForProvider = "satellite",
                    MaxMapLevel = 7,
                    IsFirstQuadrant = false,
                    Version = "1.17-1",
                    ImageExtension = "jpg",
                },
                new
                {
                    Id = 5,
                    MapProviderId = 1,
                    MapId = 3,
                    MapTypeId = 1,
                    NameForProvider = "namalsk",
                    MapTypeForProvider = "topographic",
                    MaxMapLevel = 7,
                    IsFirstQuadrant = false,
                    Version = "04.19",
                    ImageExtension = "jpg",
                },
                new
                {
                    Id = 6,
                    MapProviderId = 1,
                    MapId = 3,
                    MapTypeId = 2,
                    NameForProvider = "namalsk",
                    MapTypeForProvider = "satellite",
                    MaxMapLevel = 7,
                    IsFirstQuadrant = false,
                    Version = "04.19",
                    ImageExtension = "jpg",
                },
                new
                {
                    Id = 7,
                    MapProviderId = 1,
                    MapId = 4,
                    MapTypeId = 1,
                    NameForProvider = "esseker",
                    MapTypeForProvider = "topographic",
                    MaxMapLevel = 7,
                    IsFirstQuadrant = false,
                    Version = "0.58",
                    ImageExtension = "jpg",
                },
                new
                {
                    Id = 8,
                    MapProviderId = 1,
                    MapId = 4,
                    MapTypeId = 2,
                    NameForProvider = "esseker",
                    MapTypeForProvider = "satellite",
                    MaxMapLevel = 7,
                    IsFirstQuadrant = false,
                    Version = "0.58",
                    ImageExtension = "jpg",
                },
                new
                {
                    Id = 9,
                    MapProviderId = 1,
                    MapId = 5,
                    MapTypeId = 1,
                    NameForProvider = "takistanplus",
                    MapTypeForProvider = "topographic",
                    MaxMapLevel = 7,
                    IsFirstQuadrant = false,
                    Version = "1.041",
                    ImageExtension = "jpg",
                },
                new
                {
                    Id = 10,
                    MapProviderId = 1,
                    MapId = 5,
                    MapTypeId = 2,
                    NameForProvider = "takistanplus",
                    MapTypeForProvider = "satellite",
                    MaxMapLevel = 7,
                    IsFirstQuadrant = false,
                    Version = "1.041",
                    ImageExtension = "jpg",
                },
                new
                {
                    Id = 11,
                    MapProviderId = 1,
                    MapId = 6,
                    MapTypeId = 1,
                    NameForProvider = "banov",
                    MapTypeForProvider = "topographic",
                    MaxMapLevel = 7,
                    IsFirstQuadrant = false,
                    Version = "04.19",
                    ImageExtension = "jpg",
                },
                new
                {
                    Id = 12,
                    MapProviderId = 1,
                    MapId = 6,
                    MapTypeId = 2,
                    NameForProvider = "banov",
                    MapTypeForProvider = "satellite",
                    MaxMapLevel = 7,
                    IsFirstQuadrant = false,
                    Version = "04.04",
                    ImageExtension = "jpg",
                },

                new
                {
                    Id = 13,
                    MapProviderId = 2,
                    MapId = 1,
                    MapTypeId = 1,
                    NameForProvider = "ChernarusPlus",
                    MapTypeForProvider = "Top",
                    MaxMapLevel = 8,
                    IsFirstQuadrant = false,
                    Version = "1.0.0",
                    ImageExtension = "webp",
                },
                new
                {
                    Id = 14,
                    MapProviderId = 2,
                    MapId = 1,
                    MapTypeId = 2,
                    NameForProvider = "ChernarusPlus",
                    MapTypeForProvider = "Sat",
                    MaxMapLevel = 8,
                    IsFirstQuadrant = false,
                    Version = "1.0.0",
                    ImageExtension = "webp",
                },
                new
                {
                    Id = 15,
                    MapProviderId = 2,
                    MapId = 2,
                    MapTypeId = 1,
                    NameForProvider = "Livonia",
                    MapTypeForProvider = "Top",
                    MaxMapLevel = 8,
                    IsFirstQuadrant = false,
                    Version = "1.0.0",
                    ImageExtension = "webp",
                },
                new
                {
                    Id = 16,
                    MapProviderId = 2,
                    MapId = 2,
                    MapTypeId = 2,
                    NameForProvider = "Livonia",
                    MapTypeForProvider = "Sat",
                    MaxMapLevel = 8,
                    IsFirstQuadrant = false,
                    Version = "1.0.0",
                    ImageExtension = "webp",
                },
                new
                {
                    Id = 17,
                    MapProviderId = 2,
                    MapId = 3,
                    MapTypeId = 1,
                    NameForProvider = "Namalsk",
                    MapTypeForProvider = "Top",
                    MaxMapLevel = 7,
                    IsFirstQuadrant = true,
                    Version = "0.1.0",
                    ImageExtension = "jpg",
                },
                new
                {
                    Id = 18,
                    MapProviderId = 1,
                    MapId = 3,
                    MapTypeId = 2,
                    NameForProvider = "Namalsk",
                    MapTypeForProvider = "Sat",
                    MaxMapLevel = 7,
                    IsFirstQuadrant = true,
                    Version = "0.1.0",
                    ImageExtension = "jpg",
                },
                new
                {
                    Id = 19,
                    MapProviderId = 2,
                    MapId = 4,
                    MapTypeId = 1,
                    NameForProvider = "Esseker",
                    MapTypeForProvider = "Top",
                    MaxMapLevel = 7,
                    IsFirstQuadrant = true,
                    Version = "1.1.0",
                    ImageExtension = "jpg",
                },
                new
                {
                    Id = 20,
                    MapProviderId = 2,
                    MapId = 4,
                    MapTypeId = 2,
                    NameForProvider = "Esseker",
                    MapTypeForProvider = "Sat",
                    MaxMapLevel = 7,
                    IsFirstQuadrant = true,
                    Version = "1.1.0",
                    ImageExtension = "jpg",
                },
                new
                {
                    Id = 21,
                    MapProviderId = 2,
                    MapId = 5,
                    MapTypeId = 1,
                    NameForProvider = "TakistanPlus",
                    MapTypeForProvider = "Top",
                    MaxMapLevel = 7,
                    IsFirstQuadrant = true,
                    Version = "1.1.0",
                    ImageExtension = "jpg",
                },
                new
                {
                    Id = 22,
                    MapProviderId = 2,
                    MapId = 5,
                    MapTypeId = 2,
                    NameForProvider = "TakistanPlus",
                    MapTypeForProvider = "Sat",
                    MaxMapLevel = 7,
                    IsFirstQuadrant = true,
                    Version = "1.1.0",
                    ImageExtension = "jpg",
                },
                new
                {
                    Id = 23,
                    MapProviderId = 2,
                    MapId = 6,
                    MapTypeId = 1,
                    NameForProvider = "Banov",
                    MapTypeForProvider = "Top",
                    MaxMapLevel = 7,
                    IsFirstQuadrant = false,
                    Version = "1.1.0",
                    ImageExtension = "webp",
                },
                new
                {
                    Id = 24,
                    MapProviderId = 2,
                    MapId = 6,
                    MapTypeId = 2,
                    NameForProvider = "Banov",
                    MapTypeForProvider = "Sat",
                    MaxMapLevel = 7,
                    IsFirstQuadrant = false,
                    Version = "1.1.0",
                    ImageExtension = "webp",
                }

             );
        return modelBuilder;
    }
}