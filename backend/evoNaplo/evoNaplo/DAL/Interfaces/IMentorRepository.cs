using evoNaplo.Models;

namespace evoNaplo.DAL.Interfaces
{
    public interface IMentorRepository
    {
        Task<IEnumerable<Mentor>> GetAllMentorsAsync();
        Task<Mentor?> GetMentorByIdAsync(string id);
        Task<Mentor> AddMentorAsync(Mentor mentor);
        Task<Mentor> UpdateMentorAsync(Mentor mentor);
        Task<bool> DeleteMentorAsync(string id);
        Task<Mentor?> GetMentorsWithDetails(string id);
    }
}
