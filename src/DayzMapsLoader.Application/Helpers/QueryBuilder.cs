using DayzMapsLoader.Domain.Entities;

namespace DayzMapsLoader.Application.Helpers;

internal class QueryBuilder
{
    private readonly string _queryTemplate;

    public QueryBuilder(ProvidedMap map, int zoom)
    {
        _queryTemplate = BuildTemplateQuery(map, zoom);
    }

    public string GetQuery(int i, int j)
    {
        return string.Format(_queryTemplate, i.ToString(), j.ToString());
    }

    private static string BuildTemplateQuery(ProvidedMap map, int zoom)
    {
        return map.MapProvider.Id switch
        {
            1 => $"https://static.xam.nu/dayz/maps/{map.NameForProvider}/{map.Version}/{map.MapTypeForProvider}/{zoom}/{{0}}/{{1}}.{map.ImageExtension}",
            2 => $"https://maps.izurvive.com/maps/{map.NameForProvider}-{map.MapTypeForProvider}/{map.Version}/tiles/{zoom}/{{0}}/{{1}}.{map.ImageExtension}",
            _ => throw new NotImplementedException("Add new provider in this method!"),
        };
    }
}