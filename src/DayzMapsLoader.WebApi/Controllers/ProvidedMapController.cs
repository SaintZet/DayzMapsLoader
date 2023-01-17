using DayzMapsLoader.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;

namespace DayzMapsLoader.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidedMapController : ControllerBase
    {
        private readonly IMapDownloader _mapDownloader;

        public ProvidedMapController(IMapDownloader mapDownloader)
        {
            _mapDownloader = mapDownloader;
        }

        // GET: api/<ProvidedMapController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{providerId}/{mapID}/{typeId}/{zoom}")]
        public async Task<FileContentResult> Get(int providerId, int mapID, int typeId, int zoom)
        {
            var map = await _mapDownloader.DownloadMap(providerId, mapID, typeId, zoom);

            using (var compressedFileStream = new MemoryStream())
            {
                ////Create an archive and store the stream in memory.
                //using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
                //{
                //    //Create a zip entry for each attachment
                //    var zipEntry = zipArchive.CreateEntry(attachmentModel.Name);

                //    //Get the stream of the attachment
                //    using var originalFileStream = new MemoryStream(attachmentModel.Body);

                //    using var zipEntryStream = zipEntry.Open();
                //    //Copy the attachment stream to the zip entry stream
                //    originalFileStream.CopyTo(zipEntryStream);
                //}

                return new FileContentResult(compressedFileStream.ToArray(), "application/zip") { FileDownloadName = "Filename.zip" };
            }
        }

        private struct AttachmentModel
        {
            public string Name { get; set; }
            public byte[] Body { get; set; }
        }
    }
}