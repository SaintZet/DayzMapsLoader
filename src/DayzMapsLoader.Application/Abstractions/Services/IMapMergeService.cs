using DayzMapsLoader.Shared.Enums;
using DayzMapsLoader.Shared.Wrappers;
using System.Drawing;

namespace DayzMapsLoader.Application.Abstractions.Services;

internal interface IMapMergeService
{
    public Bitmap Merge(MapParts source, ImageExtension extension);
}