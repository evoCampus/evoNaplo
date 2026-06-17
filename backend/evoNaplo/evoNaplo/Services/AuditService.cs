using System;
using System.Linq;
using evoNaplo.Data;
using evoNaplo.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<evoNaplo.DTO.PagedResult<AuditLog>> QueryAsync(evoNaplo.DTO.AuditQueryParams query)
    {
        var q = _context.AuditLogs.AsQueryable();

        if (!string.IsNullOrEmpty(query.UserId))
            q = q.Where(x => x.UserId == query.UserId);

        if (!string.IsNullOrEmpty(query.EventType))
            q = q.Where(x => x.EventType == query.EventType);

        if (!string.IsNullOrEmpty(query.Outcome))
            q = q.Where(x => x.Outcome == query.Outcome);

        if (query.From.HasValue)
            q = q.Where(x => x.Timestamp >= query.From.Value);

        if (query.To.HasValue)
            q = q.Where(x => x.Timestamp <= query.To.Value);

        if (!string.IsNullOrEmpty(query.Search))
        {
            var s = query.Search.ToLowerInvariant();
            q = q.Where(x => (x.Details ?? string.Empty).ToLower().Contains(s)
                             || (x.Resource ?? string.Empty).ToLower().Contains(s)
                             || (x.Action ?? string.Empty).ToLower().Contains(s));
        }
        q = (query.SortBy ?? "Timestamp") switch
        {
            "EventType" => query.Desc ? q.OrderByDescending(x => x.EventType) : q.OrderBy(x => x.EventType),
            "UserId" => query.Desc ? q.OrderByDescending(x => x.UserId) : q.OrderBy(x => x.UserId),
            _ => query.Desc ? q.OrderByDescending(x => x.Timestamp) : q.OrderBy(x => x.Timestamp),
        };
        var total = await q.CountAsync();
        var items = await q.Skip((Math.Max(1, query.Page) - 1) * Math.Max(1, query.PageSize))
                           .Take(Math.Max(1, query.PageSize))
                           .ToListAsync();

        return new evoNaplo.DTO.PagedResult<AuditLog>
        {
            Items = items,
            TotalCount = total,
            Page = query.Page,
            PageSize = query.PageSize
        };
    }
}
