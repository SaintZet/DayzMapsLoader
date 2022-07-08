using RequestsHub.Domain.Contracts;
using RequestsHub.Domain.DataTypes;
using RequestsHub.Domain.Services.ConsoleServices;
using System.Diagnostics;
using System.Drawing;
using System.Net;

namespace RequestsHub.Domain.Services
{
    public class ImageRetrieve
    {
        private readonly IMapProvider mapProvider;
        private readonly int zoom;
        private readonly TypeMap typeMap;
        private readonly PathsService pathsService;
        private IMap currentMap;

        internal ImageRetrieve(IMapProvider mapProvider, MapName nameMap, TypeMap typeMap, int zoom, string pathsToSave)
        {
            this.mapProvider = mapProvider;
            this.typeMap = typeMap;
            this.zoom = zoom;

            pathsService = InitializePathService(pathsToSave, nameMap);
            Console.WriteLine($"Directory to save: {pathsService.FolderMap}");
        }

        public void MergePartsMap()
        {
            var pathSource = $"{pathsService.GeneralPathToFolderWithFile}";
            Directory.CreateDirectory(pathSource);

            Directory.CreateDirectory(pathsService.GeneralPathToFolderWithFile);
            var pathSave = $@"{pathsService.GeneralPathToFolderWithFile}\{currentMap.MapName}.{currentMap.MapExtension}";
            new MergeImages().MergeAndSave(pathSource, pathSave);
        }

        public void GetAllMaps()
        {
            foreach (IMap map in mapProvider.Maps)
            {
                currentMap = Validate.CheckMapAtProvider(mapProvider, map.MapName);
                pathsService.FolderMap = currentMap.MapName.ToString();
                GetMap();
            }
        }

        public void GetAllMapsInParts()
        {
            foreach (var map in mapProvider.Maps)
            {
                currentMap = Validate.CheckMapAtProvider(mapProvider, map.MapName);
                pathsService.FolderMap = currentMap.MapName.ToString();
                GetMapInParts();
            }
        }

        public void GetMap()
        {
            //TODO: use linq;
            KeyValuePair<int, MapSize> keyValuePair = default;
            foreach (var item in currentMap.KeyValuePairsSize)
            {
                if (item.Key == zoom)
                {
                    keyValuePair = item;
                }
            }
            int axisY = keyValuePair.Value.Height;
            int axisX = keyValuePair.Value.Width;

            WebClient webClient = new();
            var queryBuilder = new QueryBuilder(mapProvider, currentMap, typeMap, zoom);
            string query;
            byte[,][] verticals = new byte[axisY, axisX][];

            Console.Write($"Download {currentMap.MapName}".PadRight(20));

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            using (ProgressBar progress = new())
            {
                for (int y = 0; y < axisY; y++)
                {
                    for (int x = 0; x < axisX; x++)
                    {
                        query = queryBuilder.GetQuery(x, y);
                        verticals[y, x] = webClient.DownloadData(query);

                        progress.Report((double)(y * axisX + x) / (axisX * axisY));
                    }
                }
            }

            stopWatch.Stop();
            Console.Write(" time: {0} ", stopWatch.Elapsed);

            Directory.CreateDirectory(pathsService.GeneralPathToFolderWithFile);
            var path = $@"{pathsService.GeneralPathToFolderWithFile}\{currentMap.MapName}.{currentMap.MapExtension}";

            new MergeImages().MergeAndSave(verticals, path);
        }

        public void GetMapInParts()
        {
            //TODO: use linq;
            KeyValuePair<int, MapSize> keyValuePair = default;
            foreach (var item in currentMap.KeyValuePairsSize)
            {
                if (item.Key == zoom)
                {
                    keyValuePair = item;
                }
            }
            int axisY = keyValuePair.Value.Height;
            int axisX = keyValuePair.Value.Width;

            WebClient webClient = new();
            QueryBuilder queryBuilder = new(mapProvider, currentMap, typeMap, zoom);
            string query;
            byte[] bytes;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            using (ProgressBar progress = new())
            {
                for (int y = 0; y < axisY; y++)
                {
                    string pathToFolder = pathsService.GeneralPathToFolderWithFile + @"\Horizontal " + y.ToString();
                    Directory.CreateDirectory(pathToFolder);

                    for (int x = 0; x < axisX; x++)
                    {
                        query = queryBuilder.GetQuery(x, y);
                        bytes = webClient.DownloadData(query);

                        string nameFile = $"({x}.{y}).{currentMap.MapExtension}";
                        string pathToFile = Path.Combine(pathToFolder, nameFile);

                        File.WriteAllBytes(pathToFile, bytes);
                        Console.WriteLine(pathToFile);
                    }
                }
            }

            stopWatch.Stop();
            Console.Write(" time: {0} ", stopWatch.Elapsed);
        }

        internal void MergePartsAllMaps()
        {
            foreach (IMap map in mapProvider.Maps)
            {
                currentMap = Validate.CheckMapAtProvider(mapProvider, map.MapName);
                pathsService.FolderMap = currentMap.MapName.ToString();
                MergePartsMap();
            }
        }

        private PathsService InitializePathService(string pathsToSave, MapName nameMap)
        {
            pathsToSave = Validate.PathToSave(pathsToSave);
            currentMap = Validate.CheckMapAtProvider(mapProvider, nameMap);

            Validate.CheckTypeAtMap(currentMap, typeMap);
            Validate.CheckZoomAtMap(currentMap, zoom);

            string providerName = Enum.GetName(typeof(MapProvider), mapProvider.Name);
            return new PathsService(pathsToSave, providerName, currentMap.MapName.ToString(), typeMap.ToString(), zoom.ToString());
        }

        private Image CastToImage(byte[] byteArrayIn)
        {
            using var ms = new MemoryStream(byteArrayIn);
            return Image.FromStream(ms);
        }
    }
}