using evoNaplo.Services;
using System.Collections.Generic;

namespace evoNaplo.Services.Interface
{
    internal interface IMentorService
    {
        IEnumerable<Mentor> GetAllMentors();
        Mentor GetMentorById(string id);
        void AddMentor(Mentor mentor);
       
        void UpdateMentor(string id, Mentor updatedMentor);

        void DeleteMentor(string id);
    }
}