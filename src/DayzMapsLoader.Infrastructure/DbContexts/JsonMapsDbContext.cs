using DayzMapsLoader.Application.Abstractions.Infrastructure;
using DayzMapsLoader.Domain.Entities.MapProvider;
using DayzMapsLoader.Infrastructure.Constants;
using DayzMapsLoader.Infrastructure.Converters;
using DayzMapsLoader.Infrastructure.DTO;
using Newtonsoft.Json;
using System.Reflection;

namespace DayzMapsLoader.Infrastructure.DbContexts;

public class JsonMapsDbContext : IMapsDbContext
{
    private readonly string readerResult;
    private List<ProviderDTO> providersDto;

    public JsonMapsDbContext()
    {
        using (Stream stream =
               Assembly.GetExecutingAssembly().GetManifestResourceStream(JsonContextConstants.LastVersion)!)
        using (StreamReader reader = new(stream))
        {
            readerResult = reader.ReadToEnd();
        }

        providersDto = JsonConvert.DeserializeObject<List<ProviderDTO>>(readerResult);
    }

    public MapProvider GetMapProvider(MapProviderName providerName)
    {
        var providerDTO = providersDto!.Single(dto => dto.Name == (int)providerName);

        return ConvertDTO.ToProviderEntity(providerDTO);
    }

    public IList<MapProvider> GetMapProviders()
        => providersDto.Select(providerDto => ConvertDTO.ToProviderEntity(providerDto)).ToList();
}