namespace evoNaplo.DTO.MentorDTOs
{
    public class UpdateMentorDTO
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public string? TeamId { get; set; }
        public string? ProjectId { get; set; }
    }
}
