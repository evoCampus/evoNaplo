using System.Security.Cryptography.X509Certificates;

namespace evoNaplo.Services.Models
{
    public class Student
    {
        
        public int StudID { get; set; }


        //Personal information Stud

        public string StudName { get; set; }
        public string StudEmail { get; set; }
        public string StudPhoneNumber { get; set; }        
        public string CurrentStudies { get; set; }

        //evoCampus information Stud
        
        public bool IsFirstSemester { get; set; } 
        public string PersonalGoals { get; set; }
        public bool AppliedForCampus { get; set; }
        public bool ActiveScholarship { get; set; }
        public int ScholarshipDuration { get; set; }
        public bool StayWithCurrentTeam { get; set; }
        public string StudAssignedTeam { get; set; }
        public string StudAssignedProject { get; set; }

        //Internship information Stud

        public bool InternshipApplied { get; set; }
        public bool IsAnIntern { get; set; }
        public bool doeswork { get; set; }
        public string WorkDuration { get; set; }


    }
}
