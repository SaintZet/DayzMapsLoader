using System.Drawing;
using DayzMapsLoader.Core.Enums;
using DayzMapsLoader.Core.Models;

namespace DayzMapsLoader.Core.Contracts.Builders;

internal interface IMapBuilder
{
    public Bitmap Build(MapParts source, ImageExtension extension);
}