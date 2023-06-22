using System.Drawing;
using DayzMapsLoader.Core.Helpers;

namespace DayzMapsLoader.Tests.xUnit.Core.Helpers;

public class ImageResizerTests
{
    [Fact]
    public void Resize_ReturnsResizedImage()
    {
        // Arrange
        const int width = 50;
        const int height = 50;
        var image = new Bitmap(100, 100);

        // Act
        var resizedImage = ImageResizer.Resize(image, width, height);

        // Assert
        Assert.Equal(width, resizedImage.Width);
        Assert.Equal(height, resizedImage.Height);
    }

    // Add more test methods to cover different scenarios or edge cases
}