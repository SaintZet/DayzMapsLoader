namespace DayzMapsLoader.Infrastructure.DTO;

public class ProviderDTO
{
    public int Name { get; set; }
    public IEnumerable<MapInfoDto>? Maps { get; set; }
}