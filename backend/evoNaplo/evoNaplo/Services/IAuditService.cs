using evoNaplo.Models;

namespace evoNaplo.Services;

public interface IAuditService
{
    Task LogAsync(AuditLog entry);
}
