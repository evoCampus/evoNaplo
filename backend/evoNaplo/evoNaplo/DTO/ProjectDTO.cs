namespace evoNaplo.DTO;

public class ProjectDTO
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Dictionary<string, string>? ProjectLinks { get; set; }
    public List<string>? Mentors { get; set; }
    public List<string>? Students { get; set; }

}
