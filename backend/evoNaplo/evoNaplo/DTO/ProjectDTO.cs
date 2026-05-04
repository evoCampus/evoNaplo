namespace evoNaplo.DTO;

public class ProjectDTO
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required Dictionary<string, string> ProjectLinks { get; set; }
    public required IEnumerable<string> TeamIds { get; set; }

}
