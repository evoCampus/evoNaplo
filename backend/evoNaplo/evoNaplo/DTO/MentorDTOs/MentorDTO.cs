using evoNaplo.Models;
using System.Diagnostics.CodeAnalysis;

namespace evoNaplo.DTO.MentorDTOs;

    public class MentorDTO
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }

        [SetsRequiredMembers]
        public MentorDTO(Mentor mentor)
        {
            Id = mentor.Id;
            Name = mentor.Name;
            Email = mentor.Email;
            PhoneNumber = mentor.PhoneNumber;
        }
    }
