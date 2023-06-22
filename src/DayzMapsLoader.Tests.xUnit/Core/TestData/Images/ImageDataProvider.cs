using System.Drawing;
using System.Reflection;
using DayzMapsLoader.Core.Builders;
using DayzMapsLoader.Core.Enums;
using DayzMapsLoader.Core.Helpers.WebpDecoder;

namespace DayzMapsLoader.Tests.xUnit.Core.TestData.Images
{
    public static class ImageDataProvider
    {
        
        private const string _generalPathToTestData = "DayzMapsLoader.Tests.xUnit.Core.TestData.Images";

        private const string _fullImageTemplatePath = $"{_generalPathToTestData}.{{0}}.Original.{{0}}";
        private const string _partTemplatePath = $"{_generalPathToTestData}.{{0}}.{{1}},{{2}}.{{0}}";

        public static Bitmap GetOriginalImage(ImageExtension extension)
        {
            var originalImagePath = string.Format(_fullImageTemplatePath, extension.ToString());

            if (extension == ImageExtension.webp)
            {
                var bytes = GetByteArrayFromEmbeddedResource(originalImagePath);

                return WebP.RawWebpToBitmap(bytes);
            }

            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(originalImagePath)!;

            return new Bitmap(stream);
        }
        
        public static byte[] GetByteArrayFromEmbeddedResource(string extension, int axesX, int axesY)
        {
            var fileName = string.Format(_partTemplatePath, extension, axesX, axesY);

            return GetByteArrayFromEmbeddedResource(fileName);
        }
        
        private static byte[] GetByteArrayFromEmbeddedResource(string fileName)
        {
            using var myStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName)!;

            var byteArray = new byte[myStream.Length];

            myStream.Read(byteArray, 0, byteArray.Length);

            return byteArray;
        }
    }
}