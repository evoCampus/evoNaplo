using System.Collections.Generic;
using System.Linq;
using evoNaplo.Services;

namespace evoNaplo.Services
{
    internal interface IMentorService
    {
        IEnumerable<Mentor> GetAllMentors();
        Mentor? GetMentorById(string id);
        void AddMentor(Mentor mentor);
       
        void UpdateMentor(string id, Mentor updatedMentor);

        void DeleteMentor(string id);
    }
}