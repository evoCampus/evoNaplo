namespace evoNaplo.DTO.ProjectDTOs
{
    public class UpdateProjectDTO
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public Dictionary<string, string>? ProjectLinks { get; set; }
        public string? TeamId { get; set; }
    }
}
