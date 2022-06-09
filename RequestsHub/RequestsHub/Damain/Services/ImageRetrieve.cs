using RequestsHub.Domain.DataTypes;
using System.Net;

namespace RequestsHub.Domain.Services
{
    public class ImageRetrieve
    {
        private const int maximumFirstPlane = 128;
        private const int maximumSecondPlane = 128;

        private string mainQuery;
        private string nameMap;
        private string nameOfService;
        private string typeMap;

        public ImageRetrieve(MapsProvider nameOfService, NameMap nameMap, TypeMap typeMap)
        {
            this.nameMap = Enum.GetName(typeof(NameMap), nameMap).ToLower();
            this.nameOfService = Enum.GetName(typeof(MapsProvider), nameOfService).ToLower();
            this.typeMap = Enum.GetName(typeof(TypeMap), typeMap).ToLower();

            mainQuery = BildQuery(nameOfService);
        }

        private string BildQuery(MapsProvider nameOfService)
        {
            switch (nameOfService)
            {
                //if we have to much code in this place - make Interface and implemented class for services
                case MapsProvider.dayzona:
                    return $"https://static.xam.nu/dayz/maps/{nameMap}/1.17-1/{typeMap}";

                case MapsProvider.ginfo:
                default:
                    break;
            }
            return "";
        }

        internal bool GetImages(string pathToSaveFolder, Direction directrion, int zoom)
        {
            if (pathToSaveFolder is null)
            {
                pathToSaveFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
            pathToSaveFolder += $"/{nameOfService}/{nameMap}/{typeMap}";

            byte[] bytes;
            string nameFile;
            string pathToFile;
            string pathToileFolder;
            string query;

            for (int i = 0; i < maximumFirstPlane; i++)
            {
                pathToileFolder = $"{pathToSaveFolder}/{Enum.GetName(typeof(Direction), directrion)} {i}";

                Directory.CreateDirectory(pathToileFolder);

                for (int j = 0; j < maximumSecondPlane; j++)
                {
                    object[] args = new object[] { mainQuery, zoom, j, i };

                    if (directrion == Direction.vertical)
                        args = new object[] { mainQuery, zoom, i, j };

                    query = string.Format("{0}/{1}/{2}/{3}.jpg", args);
                    //TODO: replace WebClient on HttpClient
                    bytes = new WebClient().DownloadData(query);

                    nameFile = $"({zoom}.{i}.{j}).jpg";
                    pathToFile = Path.Combine(pathToileFolder, nameFile);
                    File.WriteAllBytes(pathToFile, bytes);
                    Console.WriteLine(nameFile);
                }
            }
            return true;
        }
    }
}