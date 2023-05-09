using DayzMapsLoader.Presentation.Wpf.Core.Models;

namespace DayzMapsLoader.Presentation.Wpf.Core.Contracts.Services;

public interface ISampleDataService
{
    Task<IEnumerable<SampleOrder>> GetListDetailsDataAsync();
}
