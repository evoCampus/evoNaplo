using evoNaplo.Models;
using evoNaplo.DTO;

namespace evoNaplo.Services
{
    internal interface IMentorService
    {
        public Mentor? GetMentorModelById(string id);
        IEnumerable<MentorDTO> GetAllMentors();
        MentorDTO? GetMentorById(string id);
        void AddMentor(MentorDTO mentor);
        void UpdateMentor(string id, MentorDTO updatedMentor);
        void DeleteMentor(string id);

    }
}
