using evoNaplo.Models;

namespace evoNaplo.DTO.StudentDTOs
{
    public class CreateStudentDTO
    {
        // public string Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UniversityName { get; set; }
        public string? UniversityProgramme { get; set; }
        public required int CurrentSemester { get; set; }
        public required bool IsInTheirFirstSemester { get; set; }
        public string? PersonalGoals { get; set; }
        public required bool HasAppliedForScholarship { get; set; }
        public required bool HasScholarship { get; set; }
        public DateTime ScholarshipDuration { get; set; }
        public required bool HasAppliedForInternship { get; set; }
        public required bool HasInternship { get; set; }
        public required bool IsWorkingStudent { get; set; }
        public DateTime WorkExperienceInSemesters { get; set; }
        public required bool WantsToStayWithCurrentTeam { get; set; }
        public string? TeamId { get; set; }

        public Student ToStudent()
        {
            return new Student
            {
                Id = string.Empty,
                Name = this.Name,
                Email = this.Email,
                PhoneNumber = this.PhoneNumber,
                UniversityName = this.UniversityName,
                UniversityProgramme = this.UniversityProgramme,
                CurrentSemester = this.CurrentSemester,
                IsFirstEvoCampusSemester = this.IsInTheirFirstSemester,
                PersonalGoals = this.PersonalGoals,
                HasAppliedForScholarship = this.HasAppliedForScholarship,
                HasActiveScholarship = this.HasScholarship,
                ScholarshipDuration = this.ScholarshipDuration,
                HasAppliedForInternship = this.HasAppliedForInternship,
                IsCurrentlyIntern = this.HasInternship,
                IsWorkingStudent = this.IsWorkingStudent,
                WorkingStudentDuration = this.WorkExperienceInSemesters,
                WantsToStayWithCurrentTeam = this.WantsToStayWithCurrentTeam,
                TeamId = this.TeamId
            };
        }
    }
}
