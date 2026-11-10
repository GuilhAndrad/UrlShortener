using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Repositories;

namespace UrlShortener.Infrastructure.DataAccess.Repositories;

internal sealed class ShortUrlRepository : IShortUrlReader, IShortUrlWriter
{
    private readonly UrlShortenerDbContext _dbContext;

    public ShortUrlRepository(UrlShortenerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(ShortUrl shortUrl, CancellationToken cancellationToken) =>
        await _dbContext.ShortUrls.AddAsync(shortUrl, cancellationToken);

    public void Remove(ShortUrl shortUrl) => _dbContext.ShortUrls.Remove(shortUrl);

    public async Task IncrementAccessCount(int id, CancellationToken cancellationToken) =>
        await _dbContext.Database.ExecuteSqlInterpolatedAsync($"UPDATE ShortUrls SET AccessCount = AccessCount + 1 WHERE Id = {id}",cancellationToken);

    public async Task<ShortUrl?> GetByShortCode(string shortCode, CancellationToken cancellationToken) =>
        await _dbContext.ShortUrls.SingleOrDefaultAsync(shortUrl => shortUrl.ShortCode == shortCode, cancellationToken);

    public async Task<bool> ExistShortCode(string shortCode, CancellationToken cancellationToken) =>
        await _dbContext.ShortUrls.AnyAsync(shortUrl => shortUrl.ShortCode == shortCode, cancellationToken);
}