using evoNaplo.Models;

namespace evoNaplo.Services;

public interface IAuditService
{
    Task LogAsync(AuditLog entry);
    Task<evoNaplo.DTO.PagedResult<AuditLog>> QueryAsync(evoNaplo.DTO.AuditQueryParams query);
}
