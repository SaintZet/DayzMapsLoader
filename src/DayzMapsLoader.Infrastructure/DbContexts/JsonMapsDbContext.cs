using DayzMapsLoader.Application.Abstractions;
using DayzMapsLoader.Application.MapProviders;
using DayzMapsLoader.Domain.Map;
using System.Reflection;

namespace DayzMapsLoader.Infrastructure.DbContexts
{
    public class JsonMapsDbContext : IMapsDbContext
    {
        public List<MapInfo> GetMap(MapProviderName providerName)
        {
            string resourceName = "DayzMapsLoader.Infrastructure\\MapStorage\\ginfo-14-11-2022.json";

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName)!)
            using (StreamReader reader = new(stream!))
            {
                string result = reader.ReadToEnd();
            }

            return new List<MapInfo>();
        }
    }
}