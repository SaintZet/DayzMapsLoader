namespace DayzMapsLoader.Infrastructure.Entities
{
    public class DayzMap
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? LastVersion { get; set; }
        public string? Author { get; set; }
        public string? Link { get; set; }
    }
}