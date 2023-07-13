using DayzMapsLoader.Core.Features.MapArchive.Queries;
using DayzMapsLoader.Presentation.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DayzMapsLoader.Tests.xUnit.Presentation.WebApi.Controllers;

    public class DownloadMapControllerTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetMapImageArchive_ReturnsFileContentResult()
        {
            // Arrange
            const string expectedName = "map_image_archive.zip";
            var mockMediator = new Mock<IMediator>();
            var expectedData = new byte[] { 1, 2, 3 };
            mockMediator.Setup(m => m.Send(It.IsAny<GetMapImageArchiveQuery>(), CancellationToken.None))
                .ReturnsAsync((expectedData, expectedName));
            var controller = new DownloadMapController(mockMediator.Object);

            // Act
            var result = await controller.GetMapImageArchive(1, 1, 1, 1);

            // Assert
            var fileResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal("application/zip", fileResult.ContentType);
            Assert.Equal(expectedData, fileResult.FileContents);
            Assert.Equal(expectedName, fileResult.FileDownloadName);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetMapImagePartsArchive_ReturnsFileContentResult()
        {
            // Arrange
            const string expectedName = "map_image_parts_archive.zip";
            var mockMediator = new Mock<IMediator>();
            var expectedData = new byte[] { 1, 2, 3 };
            mockMediator.Setup(m => m.Send(It.IsAny<GetMapImagePartsArchiveQuery>(), CancellationToken.None))
                .ReturnsAsync((expectedData, expectedName));
            var controller = new DownloadMapController(mockMediator.Object);

            // Act
            var result = await controller.GetMapImagePartsArchive(1, 1, 1, 1);

            // Assert
            var fileResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal("application/zip", fileResult.ContentType);
            Assert.Equal(expectedData, fileResult.FileContents);
            Assert.Equal(expectedName, fileResult.FileDownloadName);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task GetAllMapsImagesArchive_ReturnsFileContentResult()
        {
            // Arrange
            const string expectedName = "all_maps_images_archive.zip";
            var mockMediator = new Mock<IMediator>();
            var expectedData = new byte[] { 1, 2, 3 };
            mockMediator.Setup(m => m.Send(It.IsAny<GetAllMapsImagesArchiveQuery>(), CancellationToken.None))
                .ReturnsAsync((expectedData, expectedName));
            var controller = new DownloadMapController(mockMediator.Object);

            // Act
            var result = await controller.GetAllMapsImagesArchive(1, 1);

            // Assert
            var fileResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal("application/zip", fileResult.ContentType);
            Assert.Equal(expectedData, fileResult.FileContents);
            Assert.Equal(expectedName, fileResult.FileDownloadName);
        }
    }