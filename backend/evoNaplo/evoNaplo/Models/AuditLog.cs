using System;

namespace evoNaplo.Models;

public class AuditLog
{
    public Guid Id { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string? UserId { get; set; }
    public string? Resource { get; set; }
    public string? Action { get; set; }
    public string? Outcome { get; set; }
    public string? Details { get; set; }
}
