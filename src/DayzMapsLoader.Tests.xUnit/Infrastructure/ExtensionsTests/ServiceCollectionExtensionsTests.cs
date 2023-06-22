using System.Reflection;

using DayzMapsLoader.Core.Contracts.Infrastructure.Repositories;
using DayzMapsLoader.Core.Contracts.Infrastructure.Services;
using DayzMapsLoader.Infrastructure.Contexts;
using DayzMapsLoader.Infrastructure.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace DayzMapsLoader.Tests.xUnit.Infrastructure.ExtensionsTests
{
     public class ServiceCollectionExtensionsTests
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void AddRepositories_ShouldAddExpectedServices()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddDbContext<DayzMapLoaderContext>();

            // Act
            services.AddRepositories();

            // Assert
            var serviceProvider = services.BuildServiceProvider();
    
            var mapProvidersRepository = serviceProvider.GetService<IMapProvidersRepository>();
            var mapsRepository = serviceProvider.GetService<IMapsRepository>();
            var providedMapsRepository = serviceProvider.GetService<IProvidedMapsRepository>();

            Assert.NotNull(mapProvidersRepository);
            Assert.NotNull(mapsRepository);
            Assert.NotNull(providedMapsRepository);
        }


        [Fact]
        [Trait("Category", "Unit")]
        public void AddServices_ShouldAddServicesToServiceCollection()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddServices();

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            var multipleThirdPartyApiService = serviceProvider.GetService<IMultipleThirdPartyApiService>();
            var fileService = serviceProvider.GetService<IFileService>();

            Assert.NotNull(multipleThirdPartyApiService);
            Assert.NotNull(fileService);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void AddDatabase_ShouldAddDbContextToServiceCollection()
        {
            // Arrange
            var services = new ServiceCollection();
            var dbConnection = ServiceCollectionExtensions.GetConnectionString();

            // Act
            services.AddDatabase(dbConnection);

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            var dbContext = serviceProvider.GetService<DayzMapLoaderContext>();

            Assert.NotNull(dbContext);
            Assert.IsType<DayzMapLoaderContext>(dbContext);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void EnsureDatabaseUpdated_ShouldNotApplyMigrations_WhenNoPendingMigrationsExist()
        {
            // Arrange
            var dbContextMock = new Mock<DbContext>();
            var historyRepositoryMock = new Mock<IHistoryRepository>();
            var migrationsAssemblyMock = new Mock<IMigrationsAssembly>();

            historyRepositoryMock.Setup(repo => repo.GetAppliedMigrations())
                .Returns(new List<HistoryRow>());

            migrationsAssemblyMock.SetupGet(assembly => assembly.Migrations)
                .Returns(new Dictionary<string, TypeInfo>());

            dbContextMock.As<IInfrastructure<IServiceProvider>>()
                .Setup(db => db.Instance.GetService(typeof(IHistoryRepository)))
                .Returns(historyRepositoryMock.Object);

            dbContextMock.As<IInfrastructure<IServiceProvider>>()
                .Setup(db => db.Instance.GetService(typeof(IMigrationsAssembly)))
                .Returns(migrationsAssemblyMock.Object);

            var databaseFacadeMock = new Mock<DatabaseFacade>(dbContextMock.Object);
            dbContextMock.SetupGet(db => db.Database).Returns(databaseFacadeMock.Object);

            // Act
            DayzMapsLoader.Infrastructure.Extensions.ServiceCollectionExtensions.EnsureDatabaseUpdated(dbContextMock.Object);

            // Assert
            databaseFacadeMock.Verify(db => db.EnsureDeleted(), Times.Never);
        }
    }
}