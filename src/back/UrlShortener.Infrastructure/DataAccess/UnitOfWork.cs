using System;
using System.Collections.Generic;
using System.Text;
using UrlShortener.Domain.Repositories;

namespace UrlShortener.Infrastructure.DataAccess;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly UrlShortenerDbContext _dbContext;

    public UnitOfWork(UrlShortenerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Commit(CancellationToken cancellationToken) =>
        await _dbContext.SaveChangesAsync(cancellationToken);
}
