using DayzMapsLoader.Application.Abstractions;
using DayzMapsLoader.Domain.Entities.MapProvider;
using DayzMapsLoader.Infrastructure.Constants;
using DayzMapsLoader.Infrastructure.Converters;
using DayzMapsLoader.Infrastructure.DTO;
using Newtonsoft.Json;
using System.Reflection;

namespace DayzMapsLoader.Infrastructure.DbContexts;

public class JsonMapsDbContext : IMapsDbContext
{
    public MapProvider GetMapProvider(MapProviderName providerName)
    {
        string result;

        using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(JsonContextConstants.LastVersion)!)
        using (StreamReader reader = new(stream))
        {
            result = reader.ReadToEnd();
        }

        var providersDTO = JsonConvert.DeserializeObject<List<ProviderDTO>>(result);

        var providerDTO = providersDTO!.Single(dto => dto.Name == (int)providerName);

        return ConvertDTO.ToProviderEntity(providerDTO);
    }
}