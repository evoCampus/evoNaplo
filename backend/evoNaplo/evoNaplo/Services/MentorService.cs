using System.Collections.Generic;
using System.Linq;
using evoNaplo.Services;
using evoNaplo.Models;

namespace evoNaplo.Services
{
    internal class MentorService : IMentorService
    {
        private static readonly List<Mentor> _mentors = new List<Mentor>();

        public IEnumerable<Mentor> GetAllMentors()
        {
            return _mentors;
        }

        public Mentor? GetMentorById(string id)
        {
            return _mentors.FirstOrDefault(m => m.Id == id);
        }

        public void AddMentor(Mentor mentor)
        {
            if (string.IsNullOrEmpty(mentor.Id)) mentor.Id = System.Guid.NewGuid().ToString();
            _mentors.Add(mentor);
        }

       
        public void UpdateMentor(string id, Mentor updatedMentor)
        {
            var existing = _mentors.FirstOrDefault(m => m.Id == id);
            if (existing is null || updatedMentor is null) return;

            if (updatedMentor.Name is not null) existing.Name = updatedMentor.Name;
            if (updatedMentor.Email is not null) existing.Email = updatedMentor.Email;
            if (updatedMentor.PhoneNumber is not null) existing.PhoneNumber = updatedMentor.PhoneNumber;
            if (updatedMentor.TeamCount != 0) existing.TeamCount = updatedMentor.TeamCount;
            if (updatedMentor.ProjecCount != 0) existing.ProjecCount = updatedMentor.ProjecCount;
            if (updatedMentor.StudentCount != 0) existing.StudentCount = updatedMentor.StudentCount;
            if (updatedMentor.Teams is not null) existing.Teams = updatedMentor.Teams;
            if (updatedMentor.Projects is not null) existing.Projects = updatedMentor.Projects;
        }

        public void DeleteMentor(string id)
        {
            var existing = _mentors.FirstOrDefault(m => m.Id == id);
            if (existing is not null) _mentors.Remove(existing);
        }
    }
}
