using evoNaplo.Services.Models;
using System.Collections.Generic;

namespace evoNaplo.Services.Interface
{
    public interface IMentorService
    {
        List<Mentor> GetAllMentors();
        Mentor GetMentorById(int id);
        void AddMentor(Mentor mentor);
        void UpdateMentor(Mentor mentor);
        void UpdateMentorFields(int id, Mentor updatedFields);

        void UpdateMentorName(int id, string name);
        void UpdateMentorEmail(int id, string email);
        void UpdateMentorPhone(int id, string phone);
        void UpdateMentorAssignedTeam(int id, string team);
        void UpdateMentorAssignedProject(int id, string project);

        void DeleteMentor(int id);
    }
}