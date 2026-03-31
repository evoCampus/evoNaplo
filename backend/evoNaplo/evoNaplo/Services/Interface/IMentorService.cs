using evoNaplo.Services;
using System.Collections.Generic;

namespace evoNaplo.Services.Interface
{
    public interface IMentorService
    {
        List<Mentor> GetAllMentors();
        Mentor GetMentorById(string id);
        void AddMentor(Mentor mentor);
        void UpdateMentor(Mentor mentor);
        void UpdateMentorFields(string id, Mentor updatedFields);

        void UpdateMentorName(string id, string name);
        void UpdateMentorEmail(string id, string email);
        void UpdateMentorPhone(string id, string phone);
        void UpdateMentorAssignedTeam(string id, string team);
        void UpdateMentorAssignedProject(string id, string project);

        void DeleteMentor(string id);
    }
}