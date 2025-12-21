using Microsoft.EntityFrameworkCore;
using VisualArchitect.Api.Authentication.Domain;
using VisualArchitect.Api.Authentication.Domain.Repositories;

namespace VisualArchitect.Api.Authentication.Infrastructure.Persistence;

internal sealed class IdentityWriteRepository(DbContext _context) : IIdentityWriteRepository
{
    public Task AddAsync(Identity identity, CancellationToken cancellationToken)
    {
        _context.Add(identity);
        return Task.CompletedTask;
    }
}