namespace DayzMapsLoader.Map;

internal record MapPart(byte[] data)
{
    public void Save(string pathToFile) => File.WriteAllBytes(pathToFile, data);
    public Stream AsStream() => new MemoryStream(data);
}