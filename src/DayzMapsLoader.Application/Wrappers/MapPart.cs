namespace DayzMapsLoader.Application.Wrappers;

internal record MapPart(byte[] Data)
{
    public void Save(string pathToFile) => File.WriteAllBytes(pathToFile, Data);
    public Stream AsStream() => new MemoryStream(Data);
}