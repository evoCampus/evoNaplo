using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Microsoft.Extensions.Logging;
using evoNaplo.Models;

namespace evoNaplo.Data
{
    public static class SeedData
    {
        public static void Seed(AppDbContext context, bool includeInvalids, ILogger logger)
        {
            try
            {
                logger.LogInformation("Starting database seed. includeInvalids={IncludeInvalids}", includeInvalids);
                // Read configurable counts from environment variables
                int projectsCount = TryGetEnvInt("EVONAPLO_SEED_PROJECTS", 10);
                int mentorsCount = TryGetEnvInt("EVONAPLO_SEED_MENTORS", 10);
                int teamsCount = TryGetEnvInt("EVONAPLO_SEED_TEAMS", 20);
                int studentsCount = TryGetEnvInt("EVONAPLO_SEED_STUDENTS", 100);

                logger.LogInformation("Seeding counts: Projects={Projects}, Mentors={Mentors}, Teams={Teams}, Students={Students}", projectsCount, mentorsCount, teamsCount, studentsCount);

                context.Database.EnsureCreated();
                logger.LogInformation("Database ensured/created.");

                // Clear existing data in safe order
                logger.LogInformation("Clearing existing data...");
                try { context.ProjectLinks.RemoveRange(context.ProjectLinks.ToList()); } catch (Exception ex) { logger.LogWarning(ex, "Failed removing ProjectLinks"); }
                try { context.AttendanceSheets.RemoveRange(context.AttendanceSheets.ToList()); } catch (Exception ex) { logger.LogWarning(ex, "Failed removing AttendanceSheets"); }
                try { context.Students.RemoveRange(context.Students.ToList()); } catch (Exception ex) { logger.LogWarning(ex, "Failed removing Students"); }
                try { context.Teams.RemoveRange(context.Teams.ToList()); } catch (Exception ex) { logger.LogWarning(ex, "Failed removing Teams"); }
                try { context.Mentors.RemoveRange(context.Mentors.ToList()); } catch (Exception ex) { logger.LogWarning(ex, "Failed removing Mentors"); }
                try { context.Projects.RemoveRange(context.Projects.ToList()); } catch (Exception ex) { logger.LogWarning(ex, "Failed removing Projects"); }

                context.SaveChanges();
                logger.LogInformation("Existing data cleared.");

                var faker = new Faker();

                ICollection<Project> projects = new List<Project>();
                for (int i = 0; i < projectsCount; i++)
                {
                    projects.Add(new Project
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = faker.Company.CatchPhrase(),
                        ShortDescription = faker.Lorem.Sentence(),
                        ProjectLinks = new List<ProjectLink>(),
                        Teams = new List<Team>()
                    });
                }
                context.Projects.AddRange(projects);
                context.SaveChanges();
                logger.LogInformation("Inserted {Count} projects.", projects.Count);

                ICollection<ProjectLink> links = new List<ProjectLink>();
                foreach (var proj in projects)
                {
                    int per = faker.Random.Int(1, 3);
                    for (int i = 0; i < per; i++)
                    {
                        links.Add(new ProjectLink
                        {
                            Id = Guid.NewGuid().ToString(),
                            ProjectId = proj.Id,
                            LinkType = faker.PickRandom<LinkTypes>(),
                            Url = faker.Internet.Url()
                        });
                    }
                }
                context.ProjectLinks.AddRange(links);
                context.SaveChanges();
                logger.LogInformation("Inserted {Count} project links.", links.Count);

                ICollection<Mentor> mentors = new List<Mentor>();
                for (int i = 0; i < mentorsCount; i++)
                {
                    mentors.Add(new Mentor
                    {
                        Id = Guid.NewGuid().ToString(),
                        // IsActive = faker.Random.Bool(),
                        Name = faker.Name.FullName(),
                        Email = faker.Internet.Email(),
                        PhoneNumber = faker.Phone.PhoneNumber(),
                        Teams = new List<Team>(),
                        Projects = new List<Project>()
                    });
                }
                context.Mentors.AddRange(mentors);
                context.SaveChanges();
                logger.LogInformation("Inserted {Count} mentors.", mentors.Count);

                ICollection<Team> teams = new List<Team>();
                for (int i = 0; i < teamsCount; i++)
                {
                    var proj = faker.PickRandom(projects);
                    teams.Add(new Team
                    {
                        Id = Guid.NewGuid().ToString(),
                        ProjectId = proj.Id,
                        Project = proj,
                        AttendanceSheets = new List<AttendanceSheet>(),
                        Mentors = new List<Mentor>(),
                        Students = new List<Student>()
                    });
                }
                context.Teams.AddRange(teams);
                context.SaveChanges();
                logger.LogInformation("Inserted {Count} teams.", teams.Count);

                ICollection<Student> students = new List<Student>();
                for (int i = 0; i < studentsCount; i++)
                {
                    var team = faker.PickRandom(teams);
                    var schDur = faker.Date.Between(DateTime.Now.AddMonths(-12), DateTime.Now.AddMonths(12));
                    var workDur = faker.Date.Between(DateTime.Now.AddMonths(-12), DateTime.Now.AddMonths(12));
                    students.Add(new Student
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = faker.Name.FullName(),
                        Email = faker.Internet.Email(),
                        PhoneNumber = faker.Random.Bool(7) ? faker.Phone.PhoneNumber() : null,
                        UniversityName = faker.Company.CompanyName() + " University",
                        UniversityProgramme = faker.Commerce.Department(),
                        CurrentSemester = faker.Random.Int(1, 8),
                        IsFirstEvoCampusSemester = faker.Random.Bool(),
                        PersonalGoals = faker.Lorem.Sentence(),
                        HasAppliedForScholarship = faker.Random.Bool(),
                        HasActiveScholarship = faker.Random.Bool(),
                        ScholarshipDuration = schDur,
                        HasAppliedForInternship = faker.Random.Bool(),
                        IsCurrentlyIntern = faker.Random.Bool(),
                        IsWorkingStudent = faker.Random.Bool(),
                        WorkingStudentDuration = workDur,
                        WantsToStayWithCurrentTeam = faker.Random.Bool(),
                        TeamId = team.Id,
                        Team = team
                    });
                }
                context.Students.AddRange(students);
                context.SaveChanges();
                logger.LogInformation("Inserted {Count} students.", students.Count);

                ICollection<AttendanceSheet> attendanceSheets = new List<AttendanceSheet>();
                foreach (var team in teams)
                {
                    var sheet = new AttendanceSheet
                    {
                        Id = Guid.NewGuid().ToString(),
                        WeeklyMeetingTime = DateTimeOffset.Now.AddDays(faker.Random.Int(-30, 30)),
                        LengthOfMeeting = TimeSpan.FromMinutes(faker.Random.Int(30, 120)),
                        DayOfWeek = (DayOfWeek)faker.Random.Int(0, 6),
                        PresentStudents = new List<Student>(),
                        TeamId = team.Id,
                        Team = team
                    };

                    ICollection<Student> teamStudents = students.Where(s => s.TeamId == team.Id).ToList();
                    int presentCount = Math.Min(teamStudents.Count, faker.Random.Int(0, Math.Max(1, teamStudents.Count / 3)));
                    for (int i = 0; i < presentCount; i++)
                    {
                        var st = faker.PickRandom(teamStudents);
                        if (!sheet.PresentStudents.Contains(st)) sheet.PresentStudents.Add(st);
                    }

                    attendanceSheets.Add(sheet);
                }
                context.AttendanceSheets.AddRange(attendanceSheets);
                context.SaveChanges();
                logger.LogInformation("Inserted {Count} attendance sheets.", attendanceSheets.Count);

                if (includeInvalids)
                {
                    logger.LogInformation("Injecting invalid test data...");
                    var someMentors = mentors.Take(Math.Max(1, mentors.Count / 10)).ToList();
                    foreach (var m in someMentors)
                    {
                        m.Email = CorruptEmail(m.Email);
                    }

                    var someStudents = students.Take(Math.Max(1, students.Count / 10)).ToList();
                    foreach (var s in someStudents)
                    {
                        s.Email = CorruptEmail(s.Email);
                        if (s.PhoneNumber != null)
                            s.PhoneNumber = CorruptPhone(s.PhoneNumber);
                    }
                    
                    var fkBadStudents = students.Skip(Math.Max(0, students.Count - Math.Max(1, students.Count / 20))).ToList();
                    foreach (var s in fkBadStudents)
                    {
                        s.TeamId = Guid.NewGuid().ToString(); 
                    }

                    var fkBadSheets = attendanceSheets.Take(Math.Max(1, attendanceSheets.Count / 20)).ToList();
                    foreach (var sh in fkBadSheets)
                    {
                        sh.TeamId = Guid.NewGuid().ToString();
                    }
                                       
                    var someSheets = attendanceSheets.Take(Math.Max(1, attendanceSheets.Count / 10)).ToList();
                    foreach (var sh in someSheets)
                    {
                        sh.WeeklyMeetingTime = DateTimeOffset.Now.AddYears(50);
                    }

                    context.SaveChanges();
                    logger.LogInformation("Invalid data injection complete.");
                }

                logger.LogInformation("Database seeding finished successfully.");
            }
            catch (Exception ex)
            {
                try { logger.LogError(ex, "An error occurred during database seeding."); } catch { }
                throw;
            }
        }
        private static string CorruptEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return "not-an-email";
            if (email.Contains("@"))
                return email.Replace("@", "") + "_bad";
            return "invalid_email";
        }
        private static string CorruptPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone)) return "abc123";
            return "PHONE-INVALID-" + phone.Substring(0, Math.Min(4, phone.Length));
        }
        private static int TryGetEnvInt(string name, int fallback)
        {
            var v = Environment.GetEnvironmentVariable(name);
            if (int.TryParse(v, out var r)) return r;
            return fallback;
        }
    }
}
