using evoNaplo.Data;
using evoNaplo.Models;

namespace evoNaplo.Services;

internal class AuditService : IAuditService
{
    private readonly AppDbContext _context;

    public AuditService(AppDbContext context)
    {
        _context = context;
    }

    public async Task LogAsync(AuditLog entry)
    {
        entry.Id = Guid.NewGuid();
        entry.Timestamp = DateTimeOffset.UtcNow;
        await _context.AuditLogs.AddAsync(entry);
        await _context.SaveChangesAsync();
    }
}
