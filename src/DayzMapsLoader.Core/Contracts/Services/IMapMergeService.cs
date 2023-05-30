using System.Drawing;

using DayzMapsLoader.Core.Enums;
using DayzMapsLoader.Core.Models;

namespace DayzMapsLoader.Core.Contracts.Services;

internal interface IMapMergeService
{
    public Bitmap Merge(MapParts source, ImageExtension extension);
}