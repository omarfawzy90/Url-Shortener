namespace UrlShortner.models
{
    public class UrlMapping
    {
        public int Id { get; set; }
        public string ShortId { get; set; } = string.Empty;
        public string OriginalUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
