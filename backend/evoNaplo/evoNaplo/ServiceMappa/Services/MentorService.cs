using evoNaplo.ServiceMappa.TesztDTO;
using System.Collections.Generic;
using System.Linq;



namespace evoNaplo.ServiceMappa.Services
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

        public void UpdateMentor(Mentor updatedMentor)
        {
            var existing = _mentors.FirstOrDefault(m => m.MentorId == updatedMentor.MentorId);
            if (existing == null) return;

            existing.MentorName = updatedMentor.MentorName;
            existing.MentorEmail = updatedMentor.MentorEmail;
            existing.MentorPhoneNumber = updatedMentor.MentorPhoneNumber;
            existing.MentorAssignedTeam = updatedMentor.MentorAssignedTeam;
            existing.MentorAssignedProject = updatedMentor.MentorAssignedProject;
        }

        public void UpdateMentorFields(string id, Mentor updatedFields)
        {
            var existing = _mentors.FirstOrDefault(m => m.MentorId == id);
            if (existing == null || updatedFields == null) return;

            if (updatedFields.MentorName != null) existing.MentorName = updatedFields.MentorName;
            if (updatedFields.MentorEmail != null) existing.MentorEmail = updatedFields.MentorEmail;
            if (updatedFields.MentorPhoneNumber != null) existing.MentorPhoneNumber = updatedFields.MentorPhoneNumber;
            if (updatedFields.MentorAssignedTeam != null) existing.MentorAssignedTeam = updatedFields.MentorAssignedTeam;
            if (updatedFields.MentorAssignedProject != null) existing.MentorAssignedProject = updatedFields.MentorAssignedProject;
        }

        // Single-field updates
        public void UpdateMentorName(string id, string name)
        {
            var existing = _mentors.FirstOrDefault(m => m.MentorId == id);
            if (existing == null) return;
            existing.MentorName = name;
        }

        public void UpdateMentorEmail(string id, string email)
        {
            var existing = _mentors.FirstOrDefault(m => m.MentorId == id);
            if (existing == null) return;
            existing.MentorEmail = email;
        }

        public void UpdateMentorPhone(string id, string phone)
        {
            var existing = _mentors.FirstOrDefault(m => m.MentorId == id);
            if (existing == null) return;
            existing.MentorPhoneNumber = phone;
        }

        public void UpdateMentorAssignedTeam(string id, string team)
        {
            var existing = _mentors.FirstOrDefault(m => m.MentorId == id);
            if (existing == null) return;
            existing.MentorAssignedTeam = team;
        }

        public void UpdateMentorAssignedProject(string id, string project)
        {
            var existing = _mentors.FirstOrDefault(m => m.MentorId == id);
            if (existing == null) return;
            existing.MentorAssignedProject = project;
        }

        public void DeleteMentor(string id)
        {
            var existing = _mentors.FirstOrDefault(m => m.MentorId == id);
            if (existing != null) _mentors.Remove(existing);
        }
    }
}
