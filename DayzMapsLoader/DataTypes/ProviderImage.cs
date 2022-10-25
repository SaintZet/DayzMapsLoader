namespace DayzMapsLoader.DataTypes;

internal record ProviderImage(byte[] data)
{
    public void Save(string pathToFile) => File.WriteAllBytes(pathToFile, data);
    public Stream AsStream() => new MemoryStream(data);
}
