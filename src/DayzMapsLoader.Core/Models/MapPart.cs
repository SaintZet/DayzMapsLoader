namespace DayzMapsLoader.Core.Models;

public record MapPart(int X, int Y, byte[] Data)
{
    public void Save(string pathToFile) => File.WriteAllBytes(pathToFile, Data);
    public Stream AsStream() => new MemoryStream(Data);
}