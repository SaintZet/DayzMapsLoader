using System.Diagnostics;
using System.Reflection;

using DayzMapsLoader.Presentation.Wpf.Contracts.Services;

namespace DayzMapsLoader.Presentation.Wpf.Services;

public class ApplicationInfoService : IApplicationInfoService
{
    public ApplicationInfoService()
    {
    }

    public Version GetVersion()
    {
        // Set the app version in DayzMapsLoader.Presentation.Wpf > Properties > Package > PackageVersion
        string assemblyLocation = Assembly.GetExecutingAssembly().Location;
        var version = FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion;
        return new Version(version);
    }
}
