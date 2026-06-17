using System.Diagnostics.CodeAnalysis;
using evoNaplo.Models;

namespace evoNaplo.DTO.StudentDTOs;

public class StudentDTO
{
    public required string Id { get; set; }
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

    [SetsRequiredMembers]
    public StudentDTO(Student student)
    {
        Id = student.Id;
        Name = student.Name;
        Email = student.Email;
        PhoneNumber = student.PhoneNumber;
        UniversityProgramme = student.UniversityProgramme;
        CurrentSemester = student.CurrentSemester;
        IsInTheirFirstSemester = student.IsFirstEvoCampusSemester;
        PersonalGoals = student.PersonalGoals;
        HasAppliedForScholarship = student.HasAppliedForScholarship;
        HasScholarship = student.HasActiveScholarship;
        ScholarshipDuration = student.ScholarshipDuration;
        HasAppliedForInternship = student.HasAppliedForInternship;
        HasInternship = student.IsCurrentlyIntern;
        IsWorkingStudent = student.IsWorkingStudent;
        WorkExperienceInSemesters = student.WorkingStudentDuration;
        WantsToStayWithCurrentTeam = student.WantsToStayWithCurrentTeam;
        
    }

}
