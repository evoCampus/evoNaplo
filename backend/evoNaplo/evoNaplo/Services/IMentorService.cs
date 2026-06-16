using evoNaplo.DTO.MentorDTOs;
using evoNaplo.Models;

namespace evoNaplo.Services;

public interface IMentorService
{
    Task<Mentor?> GetMentorModelById(string id);
    Task<IEnumerable<MentorDTO>> GetAllMentorsAsync();
    Task<MentorDetailsDTO> GetMentorByIdAsync(string id);
    Task<MentorDTO> AddMentorAsync(CreateMentorDTO mentorToAdd);
    Task<MentorDTO> UpdateMentorAsync(string id, UpdateMentorDTO updatedMentor);
    Task<bool> DeleteMentorAsync(string id);
    
}
