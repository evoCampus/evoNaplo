using System;

namespace evoNaplo.DTO;

public class AuditQueryParams
{
    public string? UserId { get; set; }
    public string? EventType { get; set; }
    public string? Outcome { get; set; }
    public DateTimeOffset? From { get; set; }
    public DateTimeOffset? To { get; set; }
    public string? Search { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50;
    public string SortBy { get; set; } = "Timestamp";
    public bool Desc { get; set; } = true;
}
