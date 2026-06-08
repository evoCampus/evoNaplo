using evoNaplo.Models;

namespace evoNaplo.DAL.Interfaces
{
    public interface IMentorRepository
    {
        Task<IEnumerable<Mentor>> GetAllMentorsAsync();
        Task<Mentor?> GetMentorByIdAsync(string id);
        Task AddMentorAsync(Mentor mentor);
        Task UpdateMentorAsync(Mentor mentor);
        Task DeleteMentorAsync(string id);
    }
}
