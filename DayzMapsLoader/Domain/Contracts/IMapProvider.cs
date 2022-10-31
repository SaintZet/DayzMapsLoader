using FluentValidation;
using FluentValidation.Results;
using RequestsHub.Domain.MapsProviders;

namespace RequestsHub.Domain.Contracts;

public interface IMapProvider
{
    abstract ISet<IMap> Maps { get; }
    abstract MapProvider Name { get; }

    string ToString();
}

public class MapProviderValidation : AbstractValidator<IMapProvider>
{
    public MapProviderValidation(IMap map)
    {
        RuleFor(provider => provider.Maps)
            .Must(maps => maps.Contains(map))
            .WithMessage($"Map {map.Name} doesn't exists in MapProvider");
    }
}