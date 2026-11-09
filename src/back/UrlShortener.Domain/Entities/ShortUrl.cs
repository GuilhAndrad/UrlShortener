namespace UrlShortener.Domain.Entities;

public class ShortUrl
{
    public int Id { get; private set; }
    public string Url { get; set; } = string.Empty;
    public string ShortCode { get; set; } = string.Empty;
    public int AccessCount { get; private set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}