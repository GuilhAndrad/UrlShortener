using UrlShortener.Domain.Entities;

namespace UrlShortener.Domain.Repositories;

public interface IShortUrlReader
{
    Task<ShortUrl?> GetByShortCode(string shortCode, CancellationToken cancellationToken);
    Task<bool> ExistShortCode(string shortCode, CancellationToken cancellationToken);
}
