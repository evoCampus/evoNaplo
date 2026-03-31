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

            existing.MentorName = updatedMentor.MentorName;
            existing.MentorEmail = updatedMentor.MentorEmail;
            existing.MentorPhoneNumber = updatedMentor.MentorPhoneNumber;
            existing.MentorAssignedTeam = updatedMentor.MentorAssignedTeam;
            existing.MentorAssignedProject = updatedMentor.MentorAssignedProject;
        }

        public void UpdateMentorFields(string id, Mentor updatedFields)
        {
            var existing = _mentors.FirstOrDefault(m => m.MentorId == id);
            if (existing is null || updatedFields is null) return;

            if (updatedFields.MentorName != null) existing.MentorName = updatedFields.MentorName;
            if (updatedFields.MentorEmail != null) existing.MentorEmail = updatedFields.MentorEmail;
            if (updatedFields.MentorPhoneNumber != null) existing.MentorPhoneNumber = updatedFields.MentorPhoneNumber;
            if (updatedFields.MentorAssignedTeam != null) existing.MentorAssignedTeam = updatedFields.MentorAssignedTeam;
            if (updatedFields.MentorAssignedProject != null) existing.MentorAssignedProject = updatedFields.MentorAssignedProject;
        }

        

        public void DeleteMentor(string id)
        {
            var existing = _mentors.FirstOrDefault(m => m.MentorId == id);
            if (existing is not null) _mentors.Remove(existing);
        }
    }
}
