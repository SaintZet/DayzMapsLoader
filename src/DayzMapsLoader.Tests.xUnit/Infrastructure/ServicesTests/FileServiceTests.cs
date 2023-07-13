using DayzMapsLoader.Infrastructure.Services;

using Newtonsoft.Json;

namespace DayzMapsLoader.Tests.xUnit.Infrastructure.ServicesTests;

public class FileServiceTests
{
    private const string _folderPath = "TestFiles";

    [Fact]
    [Trait("Category", "Unit")]
    public void Read_ExistingFile_ReturnsDeserializedObject()
    {
        // Arrange
        const string fileName = "existing_file.json";
        var fileService = new FileService();
        var expectedContent = new TestObject { Value = "Test" };
        var filePath = Path.Combine(_folderPath, fileName);
        var json = JsonConvert.SerializeObject(expectedContent);
        File.WriteAllText(filePath, json);

        // Act
        var result = fileService.Read<TestObject>(_folderPath, fileName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedContent.Value, result.Value);

        // Cleanup
        File.Delete(filePath);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void Read_NonExistingFile_ReturnsDefault()
    {
        // Arrange
        const string fileName = "non_existing_file.json";
        var fileService = new FileService();

        // Act
        var result = fileService.Read<TestObject>(_folderPath, fileName);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void Save_CreatesFileWithSerializedContent()
    {
        // Arrange
        const string fileName = "new_file.json";
        var fileService = new FileService();
        var content = new TestObject { Value = "Test" };
        var filePath = Path.Combine(_folderPath, fileName);

        // Act
        fileService.Save(_folderPath, fileName, content);

        // Assert
        Assert.True(File.Exists(filePath));

        var json = File.ReadAllText(filePath);
        var result = JsonConvert.DeserializeObject<TestObject>(json);
        Assert.Equal(content.Value, result?.Value);

        // Cleanup
        File.Delete(filePath);
    }
    
    [Fact]
    [Trait("Category", "Unit")]
    public void Save_CreatesDirectory_WhenDirectoryDoesNotExist()
    {
        // Arrange
        const string folderPath = "non_existing_folder";
        const string fileName = "new_file.json";
        var fileService = new FileService();
        var content = new TestObject { Value = "Test" };
        var filePath = Path.Combine(folderPath, fileName);

        // Act
        fileService.Save(folderPath, fileName, content);

        // Assert
        Assert.True(Directory.Exists(folderPath));

        // Cleanup
        Directory.Delete(folderPath, true);
    }
    
    [Fact]
    [Trait("Category", "Integration")]
    public void Save_OverwritesExistingFile_WhenFileExists()
    {
        // Arrange
        const string fileName = "existing_file.json";
        var fileService = new FileService();
        var content = new TestObject { Value = "Test" };
        var filePath = Path.Combine(_folderPath, fileName);

        // Create existing file
        File.WriteAllText(filePath, "existing content");

        // Act
        fileService.Save(_folderPath, fileName, content);

        // Assert
        Assert.True(File.Exists(filePath));

        var json = File.ReadAllText(filePath);
        var result = JsonConvert.DeserializeObject<TestObject>(json);
        Assert.Equal(content.Value, result?.Value);

        // Cleanup
        File.Delete(filePath);
    }
    
    
    [Fact]
    [Trait("Category", "Unit")]
    public void Save_ThrowsException_WhenFolderPathIsNull()
    {
        // Arrange
        const string fileName = "new_file.json";
        var fileService = new FileService();
        var content = new TestObject { Value = "Test" };

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => fileService.Save(null, fileName, content));
    }
    
    [Fact]
    [Trait("Category", "Unit")]
    public void Save_ThrowsException_WhenFileNameIsNull()
    {
        // Arrange
        const string folderPath = "existing_folder";
        var fileService = new FileService();
        var content = new TestObject { Value = "Test" };

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => fileService.Save(folderPath, null, content));
    }
    
    [Fact]
    [Trait("Category", "Unit")]
    public void Save_ThrowsException_WhenContentIsNull()
    {
        // Arrange
        const string folderPath = "existing_folder";
        const string fileName = "new_file.json";
        var fileService = new FileService();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => fileService.Save<object>(folderPath, fileName, null));
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void Delete_ExistingFile_DeletesFile()
    {
        // Arrange
        const string fileName = "existing_file.txt";
        var fileService = new FileService();
        var filePath = Path.Combine(_folderPath, fileName);
        File.Create(filePath).Close();

        // Act
        fileService.Delete(_folderPath, fileName);

        // Assert
        Assert.False(File.Exists(filePath));
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void Delete_NonExistingFile_DoesNotThrowException()
    {
        // Arrange
        const string fileName = "non_existing_file.txt";
        var fileService = new FileService();

        // Act
        fileService.Delete(_folderPath, fileName);

        // Assert (no exception is thrown)
    }
}

public class TestObject
{
    public string? Value { get; init; }
}