using UrlShortener.Domain.Entities;

namespace UrlShortener.Domain.Repositories;

public interface IShortUrlWriter
{
    Task Add(ShortUrl shortUrl, CancellationToken cancellationToken);
    void Remove(ShortUrl shortUrl);
    Task IncrementAccessCount(int id, CancellationToken cancellationToken);
}