namespace evoNaplo.DTO;

public class ProjectDTO
{
    public required string Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Dictionary<string, string>? ProjectLinks { get; set; }
    public IEnumerable<string>? Teams { get; set; }

}
