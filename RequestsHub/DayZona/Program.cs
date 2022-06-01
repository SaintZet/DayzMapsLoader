using DayZona;
using Maps;

Livonia livonia = new Livonia();
//livonia.GetAllImages(TypeOfMap.Terrain, Direction.Horizontal, 7, @"D:/LivoniaMap");
MergePictures images = new MergePictures();
images.GetPicturesFromFolder();

//MergeImages.HorizontalMerge(@"E:\LivoniaMap\Horizontal 0");