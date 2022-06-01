using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.Net;

namespace Maps
{
    public class Livonia
    {
        private const int maximumFirstPlane = 128;
        private const int maximumSecondPlane = 128;

        //TODO: Try to use [Range] attribute
        internal bool GetAllImages(TypeOfMap typeMap, Direction directrion, [Range(0, 7)] int zoom, string? pathToSaveFolder = null)
        {
            if (pathToSaveFolder is null)
            {
                var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
                pathToSaveFolder = Path.GetDirectoryName(path: location);
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            switch (typeMap)
            {
                case TypeOfMap.Terrain:
                    GetTerrainImages(directrion, zoom, pathToSaveFolder!);
                    break;

                case TypeOfMap.Satellite:
                    GetSatelliteImages(directrion, zoom, pathToSaveFolder!);
                    break;

                case TypeOfMap.Tourist:
                    GetTouristImages(directrion, zoom, pathToSaveFolder!);
                    break;
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            return true;
        }

        private void GetTerrainImages(Direction directrion, int zoom, string mainFolder)
        {
            byte[] bytes;
            string nameFile;
            string pathToFile;
            string pathToileFolder;
            string query;

            for (int i = 0; i < maximumFirstPlane; i++)
            {
                pathToileFolder = string.Format("{0}/{1} {2}", mainFolder, Enum.GetName(typeof(Direction), directrion), i);
                Directory.CreateDirectory(pathToileFolder);

                for (int j = 0; j < maximumSecondPlane; j++)
                {
                    object[] args = new object[] { zoom, j, i };

                    if (directrion == Direction.Vertical)
                        args = new object[] { zoom, i, j };

                    query = string.Format("https://static.xam.nu/dayz/maps/livonia/1.17-1/topographic/{0}/{1}/{2}.jpg", args);
                    
                    //TODO: replace WebClient on HttpClient
                    bytes = new WebClient().DownloadData(query);

                    nameFile = string.Format("LivoniaTerrain({0}.{1}.{2}).jpg", zoom, i, j);
                    pathToFile = Path.Combine(pathToileFolder, nameFile);
                    File.WriteAllBytes(pathToFile, bytes);
                    Console.WriteLine(nameFile);
                }
            }
        }

        private void GetTouristImages(Direction directrion, int zoom, string v)
        {
            throw new NotImplementedException();
        }

        private void GetSatelliteImages(Direction directrion, int zoom, string v)
        {
            throw new NotImplementedException();
        }
    }
}