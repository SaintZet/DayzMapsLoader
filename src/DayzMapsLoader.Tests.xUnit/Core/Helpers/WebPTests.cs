using System.Drawing.Imaging;
using DayzMapsLoader.Core.Helpers.WebpDecoder;
using DayzMapsLoader.Tests.xUnit.Core.TestData.Images;

namespace DayzMapsLoader.Tests.xUnit.Core.Helpers;

public class WebPTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void Decode_ValidWebP_ReturnsBitmap()
        {
            // Arrange
            var webP = new WebP();
            var rawWebP = GetValidWebPBytes();

            // Act
            var bitmap = webP.Decode(rawWebP);

            // Assert
            Assert.NotNull(bitmap);
            Assert.Equal(ImageFormat.MemoryBmp, bitmap.RawFormat);
            Assert.Equal(250, bitmap.Width);
            Assert.Equal(250, bitmap.Height);
            Assert.Equal(PixelFormat.Format24bppRgb, bitmap.PixelFormat);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void Decode_InvalidWebP_ThrowsException()
        {
            // Arrange
            var webP = new WebP();
            var rawWebP = GetInvalidWebPBytes();

            // Act and Assert
            Assert.Throws<Exception>(() => webP.Decode(rawWebP));
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void GetInfo_ValidWebP_ReturnsImageInfo()
        {
            // Arrange
            var webP = new WebP();
            var rawWebP = GetValidWebPBytes();

            // Act
            webP.GetInfo(rawWebP, out int width, out int height, out bool hasAlpha, out bool hasAnimation, out string format);

            // Assert
            Assert.Equal(250, width);
            Assert.Equal(250, height);
            Assert.False(hasAlpha);
            Assert.False(hasAnimation);
            Assert.Equal("lossy", format);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void GetInfo_InvalidWebP_ThrowsException()
        {
            // Arrange
            var webP = new WebP();
            var rawWebP = GetInvalidWebPBytes();

            // Act and Assert
            Assert.Throws<Exception>(() => webP.GetInfo(rawWebP, out _, out _, out _, out _, out _));
        }

        private byte[] GetValidWebPBytes() => 
            ImageDataProvider.GetByteArrayFromEmbeddedResource("webp", 0 , 0);

        private byte[] GetInvalidWebPBytes() => 
            new byte[]{ 0x52, 0x49, 0x46, 0x46, 0x00, 0x00, 0x00, 0x00};
    }