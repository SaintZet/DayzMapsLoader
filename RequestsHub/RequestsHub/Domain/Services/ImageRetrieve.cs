using RequestsHub.Domain.Contracts;
using RequestsHub.Domain.DataTypes;
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

            pathsToSave = Validate.PathToSave(pathsToSave);
            currentMap = Validate.CheckMapAtProvider(mapProvider, nameMap);
            Validate.CheckTypeAtMap(currentMap, typeMap);
            Validate.CheckZoomAtMap(currentMap, zoom);
            string providerName = Enum.GetName(typeof(MapProvider), mapProvider.Name);
            pathsService = new(pathsToSave, providerName, currentMap.MapName.ToString(), typeMap.ToString(), zoom.ToString());
        }

        public void MergePartsMap()
        {
            var pathSource = $"{pathsService.GeneralFolderToSave}";
            Directory.CreateDirectory(pathSource);

            Directory.CreateDirectory(pathsService.GeneralFolderToSave);
            var pathSave = $@"{pathsService.GeneralFolderToSave}\{currentMap.MapName}.{currentMap.MapExtension}";
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
                foreach (MapName nameMap in Enum.GetValues(typeof(MapName)))
                {
                    currentMap = Validate.CheckMapAtProvider(mapProvider, nameMap);
                    pathsService.FolderMap = currentMap.MapName.ToString();
                    GetPartsMap();
                }
            }
        }

        public void GetMap()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            byte[] bytes;
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

            Image[][] verticals = new Image[axisY][];
            Image[] horizontal;
            for (int y = 0; y < axisY; y++)
            {
                horizontal = new Image[axisX];
                for (int x = 0; x < axisX; x++)
                {
                    query = queryBuilder.GetQuery(x, y);
                    bytes = webClient.DownloadData(query);

                    horizontal[x] = CastToImage(bytes);
                }
                verticals[y] = horizontal;
            }

            Directory.CreateDirectory(pathsService.GeneralFolderToSave);
            var path = $@"{pathsService.GeneralFolderToSave}\{currentMap.MapName}.{currentMap.MapExtension}";

            new MergeImages().MergeAndSave(verticals, path);

            stopWatch.Stop();
            Console.WriteLine("Work time: {0}", stopWatch.Elapsed);
        }

        public void GetPartsMap()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            byte[] bytes;

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

            for (int y = 0; y < axisY; y++)
            {
                string pathToFolder = pathsService.GeneralFolderToSave + @"\Horizontal " + y.ToString();
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

            stopWatch.Stop();
            Console.WriteLine("Work time: {0}", stopWatch.Elapsed);
        }

        private Image CastToImage(byte[] byteArrayIn)
        {
            using var ms = new MemoryStream(byteArrayIn);
            return Image.FromStream(ms);
        }
    }
}