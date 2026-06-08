using evoNaplo.DTO;
using evoNaplo.Models;

namespace evoNaplo.Services;

public interface IMentorService
{
    Task<Mentor?> GetMentorModelById(string id);
    Task<IEnumerable<MentorDTO>> GetAllMentorsAsync();
    Task<MentorDTO> GetMentorByIdAsync(string id);
    Task<MentorDTO> AddMentorAsync(MentorDTO mentorToAdd);
    Task<MentorDTO> UpdateMentorAsync(string id, MentorDTO updatedMentor);
    Task<bool> DeleteMentorAsync(string id);
    
}
