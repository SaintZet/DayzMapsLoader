using DayzMapsLoader.Application.Abstractions.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Imaging;
using System.IO.Compression;

namespace DayzMapsLoader.Presentation.WebApi.Controllers
{
    [Route("api/download-map")]
    [ApiController]
    public class DownloadMapController : BaseController
    {
        private readonly IMapDownloader _mapDownloader;

        public DownloadMapController(IMediator mediator, IMapDownloader mapDownloader) : base(mediator)
        {
            _mapDownloader = mapDownloader;
        }

        [HttpGet("{providerId}/{mapID}/{typeId}/{zoom}")]
        public async Task<FileContentResult> Get(int providerId, int mapID, int typeId, int zoom)
        {
            var map = await _mapDownloader.DownloadMap(providerId, mapID, typeId, zoom);

            using (var compressedFileStream = new MemoryStream())
            {
                //Create an archive and store the stream in memory.
                using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
                {
                    //Create a zip entry for each attachment
                    var zipEntry = zipArchive.CreateEntry("TestName");

                    //Get the stream of the attachment
                    using var originalFileStream = new MemoryStream();
                    map.Save(originalFileStream, ImageFormat.Png);

                    using var zipEntryStream = zipEntry.Open();
                    //Copy the attachment stream to the zip entry stream
                    originalFileStream.CopyTo(zipEntryStream);
                }

                return new FileContentResult(compressedFileStream.ToArray(), "application/zip") { FileDownloadName = "Filename.zip" };
            }
        }
    }
}