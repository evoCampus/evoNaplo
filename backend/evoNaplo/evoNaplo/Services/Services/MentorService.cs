using evoNaplo.Services.Models;
using System.Collections.Generic;
using System.Linq;



namespace evoNaplo.Services.Services
{
    public class MentorService : Interface.IMentorService
    {
        private static readonly List<Mentor> _mentors = new List<Mentor>();
        private static int _nextId = 1;

        public List<Mentor> GetAllMentors()
        {
            return _mentors;
        }

        public Mentor GetMentorById(int id)
        {
            return _mentors.FirstOrDefault(m => m.MentorId == id);
        }

        public void AddMentor(Mentor mentor)
        {
            mentor.MentorId = _nextId++;
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

        public void UpdateMentorFields(int id, Mentor updatedFields)
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
        public void UpdateMentorName(int id, string name)
        {
            var existing = _mentors.FirstOrDefault(m => m.MentorId == id);
            if (existing == null) return;
            existing.MentorName = name;
        }

        public void UpdateMentorEmail(int id, string email)
        {
            var existing = _mentors.FirstOrDefault(m => m.MentorId == id);
            if (existing == null) return;
            existing.MentorEmail = email;
        }

        public void UpdateMentorPhone(int id, string phone)
        {
            var existing = _mentors.FirstOrDefault(m => m.MentorId == id);
            if (existing == null) return;
            existing.MentorPhoneNumber = phone;
        }

        public void UpdateMentorAssignedTeam(int id, string team)
        {
            var existing = _mentors.FirstOrDefault(m => m.MentorId == id);
            if (existing == null) return;
            existing.MentorAssignedTeam = team;
        }

        public void UpdateMentorAssignedProject(int id, string project)
        {
            var existing = _mentors.FirstOrDefault(m => m.MentorId == id);
            if (existing == null) return;
            existing.MentorAssignedProject = project;
        }

        public void DeleteMentor(int id)
        {
            var existing = _mentors.FirstOrDefault(m => m.MentorId == id);
            if (existing != null) _mentors.Remove(existing);
        }
    }
}
