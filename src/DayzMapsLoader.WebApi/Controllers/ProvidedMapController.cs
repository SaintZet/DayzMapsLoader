using DayzMapsLoader.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace DayzMapsLoader.WebApi.Controllers
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
        public async Task<ActionResult<byte[]>> Get(int providerId, int mapID, int typeId, int zoom)
        {
            return await _mapDownloader.DownloadMap(providerId, mapID, typeId, zoom);
        }
    }
}