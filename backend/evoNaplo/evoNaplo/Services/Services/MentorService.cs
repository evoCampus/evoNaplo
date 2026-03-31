using evoNaplo.Services;
using System.Collections.Generic;
using System.Linq;



namespace evoNaplo.Services.Services
{
    public class MentorService : Interface.IMentorService
    {
        private static readonly List<Mentor> _mentors = new List<Mentor>();

        public List<Mentor> GetAllMentors()
        {
            return _mentors;
        }

        public Mentor GetMentorById(string id)
        {
            return _mentors.FirstOrDefault(m => m.MentorId == id);
        }

        public void AddMentor(Mentor mentor)
        {
            if (string.IsNullOrEmpty(mentor.MentorId)) mentor.MentorId = System.Guid.NewGuid().ToString();
            _mentors.Add(mentor);
        }

       
        public void UpdateMentor(string id, Mentor updatedMentor)
        {
            var existing = _mentors.FirstOrDefault(m => m.MentorId == id);
            if (existing is null || updatedMentor is null) return;

            if (updatedMentor.MentorName is not null) existing.MentorName = updatedMentor.MentorName;
            if (updatedMentor.MentorEmail is not null) existing.MentorEmail = updatedMentor.MentorEmail;
            if (updatedMentor.MentorPhoneNumber is not null) existing.MentorPhoneNumber = updatedMentor.MentorPhoneNumber;
            if (updatedMentor.MentorAssignedTeam is not null) existing.MentorAssignedTeam = updatedMentor.MentorAssignedTeam;
            if (updatedMentor.MentorAssignedProject is not null) existing.MentorAssignedProject = updatedMentor.MentorAssignedProject;
        }

        

        public void DeleteMentor(string id)
        {
            var existing = _mentors.FirstOrDefault(m => m.MentorId == id);
            if (existing is not null) _mentors.Remove(existing);
        }
    }
}
