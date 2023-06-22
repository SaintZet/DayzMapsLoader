using DayzMapsLoader.Domain.Entities;
using DayzMapsLoader.Infrastructure.Contexts;
using DayzMapsLoader.Infrastructure.Extensions;

using Microsoft.Extensions.DependencyInjection;

namespace DayzMapsLoader.Tests.xUnit.Infrastructure.ExtensionsTests;

public class IncludeDetailsExtensionsTests
{
    [Fact]
    [Trait("Category", "Integration")]
    public void IncludeDetails_IncludeIsTrue_IncludesAllNavigationProperties()
    {
        // Arrange
        var queryable = GetQueryable();
            
        // Act
        var result = (queryable.IncludeDetails() ?? throw new InvalidOperationException()).ToList();
            
        // Assert
        Assert.NotNull(result);
            
        foreach (var providedMap in result)
        {
            Assert.NotNull(providedMap.MapProvider);
            Assert.NotNull(providedMap.Map);
            Assert.NotNull(providedMap.MapType);
        }
    }
        
    [Fact]
    [Trait("Category", "Unit")]
    public void IncludeDetails_IncludeIsFalse_DoesNotIncludeNavigationProperties()
    {
        // Arrange
        var queryable = GetQueryable();
            
        // Act
        var result = (queryable.IncludeDetails(include: false) ?? throw new InvalidOperationException()).ToList();
            
        // Assert
        Assert.NotNull(result);
            
        foreach (var providedMap in result)
        {
            Assert.Null(providedMap.MapProvider);
            Assert.Null(providedMap.Map);
            Assert.Null(providedMap.MapType);
        }
    }
        
    private static IQueryable<ProvidedMap>? GetQueryable()
    {
        var serviceProvider = new ServiceCollection().BuildCollection();
        
        var dbContext = serviceProvider.GetService<DayzMapLoaderContext>();
        
        var queryable = dbContext?.Set<ProvidedMap>().AsQueryable();
            
        return queryable;
    }
}