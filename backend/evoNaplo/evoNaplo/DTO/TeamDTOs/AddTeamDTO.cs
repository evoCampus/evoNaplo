namespace evoNaplo.DTO.TeamDTOs
{
    public class AddTeamDTo
    {
        public string? ProjectId { get; set; }
        public List<string>? MentorIds { get; set; }
        public List<string>? StudentIds { get; set; }
        public List<string>? AttendanceSheetIds { get; set; }
    }
}
