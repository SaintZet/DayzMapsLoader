using DayzMapsLoader.Core.Models;

namespace DayzMapsLoader.Tests.xUnit.Core.Models;

public class MapPartTests
{
    [Fact]
    [Trait("Category", "Unit")]
    public void MapPart_Save_SavesDataToFile()
    {
        // Arrange
        const string testFilePath = "testFile.dat";
        byte[] testData = { 1, 2, 3, 4, 5 };
        var mapPart = new MapPart(1, 1, testData);

        // Act
        mapPart.Save(testFilePath);

        // Assert
        Assert.True(File.Exists(testFilePath));

        var savedData = File.ReadAllBytes(testFilePath);
        Assert.Equal(testData, savedData);

        // Cleanup
        File.Delete(testFilePath);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void MapPart_AsStream_ReturnsStreamWithData()
    {
        // Arrange
        byte[] testData = { 1, 2, 3, 4, 5 };
        var mapPart = new MapPart(1,1, testData);

        // Act
        var stream = mapPart.AsStream();

        // Assert
        Assert.NotNull(stream);

        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        var streamData = memoryStream.ToArray();

        Assert.Equal(testData, streamData);
    }
}