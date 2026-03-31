using evoNaplo.Services;
using System.Collections.Generic;

namespace evoNaplo.Services.Interface
{
    public interface IMentorService
    {
        List<Mentor> GetAllMentors();
        Mentor GetMentorById(string id);
        void AddMentor(Mentor mentor);
       
        void UpdateMentorFields(string id, Mentor updatedFields);

        
        void UpdateMentor(string id, Mentor updatedMentor);

        void DeleteMentor(string id);
    }
}