using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.Authentication.Domain;
using VisualArchitect.Api.Authentication.Domain.Repositories;

namespace VisualArchitect.Api.Authentication.Infrastructure.Persistence;

internal sealed class OAuthIdentityWriteRepository(DbContext _context) : IOAuthIdentityWriteRepository
{
    public Task AddAsync(OAuthIdentity identity, CancellationToken cancellationToken)
    {
        _context.Add(identity);
        return Task.CompletedTask;
    }
}
