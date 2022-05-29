using DayZona;
using Maps;

Livonia livonia = new Livonia();
livonia.GetAllImages(TypeOfMap.Terrain, Direction.Horizontal, 7, @"E:/LivoniaMap");

//MergeImages.HorizontalMerge(@"E:\LivoniaMap\Horizontal 0");