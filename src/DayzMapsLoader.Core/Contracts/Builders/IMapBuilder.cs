using System.Drawing;
using DayzMapsLoader.Core.Enums;
using DayzMapsLoader.Core.Models;

namespace DayzMapsLoader.Core.Contracts.Builders;

internal interface IMapBuilder
{
	void Append(MapPart part, ImageExtension extension);
    Bitmap Build();
}