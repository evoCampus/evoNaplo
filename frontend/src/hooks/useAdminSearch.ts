import { useMemo } from "react";
import type { StudentDTO, MentorDTO, TeamDTO, ProjectDTO } from "../api";

interface UseAdminSearchProps {
  searchQuery: string;
  students: StudentDTO[];
  mentors: MentorDTO[];
  teams: TeamDTO[];
  projects: ProjectDTO[];
}

const DAYS = ["monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday"];

export function useAdminSearch({
  searchQuery,
  students,
  mentors,
  teams,
  projects,
}: UseAdminSearchProps) {
  const query = useMemo(() => searchQuery.trim().toLowerCase(), [searchQuery]);

  const filteredStudents = useMemo(() => {
    if (!query) return [];
    return students.filter(
      (s) => s.name?.toLowerCase().includes(query) || s.email?.toLowerCase().includes(query)
    );
  }, [students, query]);

  const filteredMentors = useMemo(() => {
    if (!query) return [];
    return mentors.filter(
      (m) => m.name?.toLowerCase().includes(query) || m.email?.toLowerCase().includes(query)
    );
  }, [mentors, query]);

  const filteredTeams = useMemo(() => {
    if (!query) return [];
    return teams.filter((t) => {
      const dayStr =
        t.weeklyMeetingDay !== undefined && t.weeklyMeetingDay !== null
          ? DAYS[t.weeklyMeetingDay]
          : "unknown";
      const timeStr = (t.weeklyMeetingTime || "tbd").toLowerCase();
      return "team".includes(query) || dayStr.includes(query) || timeStr.includes(query);
    });
  }, [teams, query]);

  const filteredProjects = useMemo(() => {
    if (!query) return [];
    return projects.filter(
      (p) => p.name?.toLowerCase().includes(query) || p.description?.toLowerCase().includes(query)
    );
  }, [projects, query]);

  const totalResults = useMemo(
    () =>
      filteredStudents.length +
      filteredMentors.length +
      filteredTeams.length +
      filteredProjects.length,
    [filteredStudents, filteredMentors, filteredTeams, filteredProjects]
  );

  return {
    filteredStudents,
    filteredMentors,
    filteredTeams,
    filteredProjects,
    totalResults,
    isSearching: query.length > 0,
  };
}
