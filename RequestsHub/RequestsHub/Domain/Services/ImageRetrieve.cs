using RequestsHub.Domain.Contracts;
using RequestsHub.Domain.DataTypes;
using System.Net;

namespace RequestsHub.Domain.Services
{
    public class ImageRetrieve
    {
        private IMapProvider mapProvider;
        private PathsToSave pathsToSave;
        private int zoom;
        private string nameMap;
        private string typeMap;

        internal ImageRetrieve(IMapProvider mapProvider, ArgsFromConsole args)
        {
            this.mapProvider = mapProvider;

            nameMap = Enum.GetName(typeof(NameMap), args.NameMap).ToLower();
            typeMap = Enum.GetName(typeof(TypeMap), args.TypeMap).ToLower();
            zoom = args.Zoom;

            //TODO: Builder for paths class.
        }

        public void GetAllMaps() //TypeMap typeMap, int zoom, string pathToSaveImages
        {
            throw new NotImplementedException();
        }

        public void GetAllMapsInParts() //NameMap nameMap, TypeMap typeMap, int zoom, string pathToSaveImages
        {
            throw new NotImplementedException();
        }

        public void GetMap() //NameMap nameMap, TypeMap typeMap, int zoom, string directorySaveImage
        {
            throw new NotImplementedException();
            //var map = ValidateParameters(nameMap, typeMap, zoom);
            //if (map == null)
            //{
            //    throw new ArgumentException("No valid map.");
            //}

            //pathsToSave = Builder.InitializePaths(map, typeMap, zoom, directorySaveImage);
        }

        public void GetPartsMap() //NameMap nameMap, TypeMap typeMap, int zoom, string pathToSaveImages
        {
            throw new NotImplementedException();
        }

        //private IMap? ValidateParameters(NameMap nameMap, TypeMap typeMap, int zoom)
        //{
        //    foreach (IMap map in Maps)
        //    {
        //        if (map.Name == nameMap)
        //        {
        //            foreach (TypeMap type in map.TypesMap)
        //            {
        //                if (type == typeMap)
        //                {
        //                    foreach (KeyValuePair<int, MapSize> size in map.KeyValuePairsSize)
        //                    {
        //                        if (zoom == size.Key)
        //                        {
        //                            return map;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return null;
        //}

        //private string BildQuery(MapsProvider nameOfService)
        //{
        //    switch (nameOfService)
        //    {
        //        //if we have to much code in this place - make Interface and implemented class for services
        //        case MapsProvider.xam:
        //            return $"https://static.xam.nu/dayz/maps/{nameMap}/1.17-1/{typeMap}";

        //        case MapsProvider.ginfo:
        //        default:
        //            break;
        //    }
        //    return "";
        //}

        //internal bool GetImages(string pathToSaveFolder, Direction directrion, int zoom)
        //{
        //    if (pathToSaveFolder is null)
        //    {
        //        pathToSaveFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        //    }
        //    pathToSaveFolder += $"/{nameOfService}/{nameMap}/{typeMap}";

        // byte[] bytes; string nameFile; string pathToFile; string pathToileFolder; string query;

        // for (int i = 0; i < maximumFirstPlane; i++) { pathToileFolder =
        // $"{pathToSaveFolder}/{Enum.GetName(typeof(Direction), directrion)} {i}";

        // Directory.CreateDirectory(pathToileFolder);

        // for (int j = 0; j < maximumSecondPlane; j++) { object[] args = new object[] { mainQuery,
        // zoom, j, i };

        // if (directrion == Direction.vertical) args = new object[] { mainQuery, zoom, i, j };

        // query = string.Format("{0}/{1}/{2}/{3}.jpg", args); //TODO: replace WebClient on
        // HttpClient bytes = new WebClient().DownloadData(query);

        //            nameFile = $"({zoom}.{i}.{j}).jpg";
        //            pathToFile = Path.Combine(pathToileFolder, nameFile);
        //            File.WriteAllBytes(pathToFile, bytes);
        //            Console.WriteLine(nameFile);
        //        }
        //    }
        //    return true;
        //}
    }
}