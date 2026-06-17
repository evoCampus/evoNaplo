import { Users, UserSquare2, Users2, FolderRoot, Loader2 } from "lucide-react";
import { Button } from "@evonaplo/ui-library";
import type { StudentDTO, MentorDTO, TeamDTO, ProjectDTO } from "../../api";
import { getDayName, formatTime } from "../../lib/date-utils";
import { SearchResultGroup } from "./SearchResultGroup";

function getTeamName(team: TeamDTO) {
  const dayStr = getDayName(team.weeklyMeetingDay);
  const timeStr = formatTime(team.weeklyMeetingTime);
  return `Team - ${dayStr} at ${timeStr}`;
}

interface DashboardSearchResultsProps {
  isLoading: boolean;
  totalResults: number;
  filteredStudents: StudentDTO[];
  filteredMentors: MentorDTO[];
  filteredTeams: TeamDTO[];
  filteredProjects: ProjectDTO[];
  onSelectStudent: (student: StudentDTO) => void;
  onSelectMentor: (mentor: MentorDTO) => void;
  onSelectTeam: (team: TeamDTO) => void;
  onSelectProject: (project: ProjectDTO) => void;
  onClear: () => void;
}

export default function DashboardSearchResults({
  isLoading,
  totalResults,
  filteredStudents,
  filteredMentors,
  filteredTeams,
  filteredProjects,
  onSelectStudent,
  onSelectMentor,
  onSelectTeam,
  onSelectProject,
  onClear,
}: DashboardSearchResultsProps) {
  return (
    <div className="space-y-6 animate-in fade-in duration-200">
      <div className="flex items-center justify-between border-b border-border/40 pb-2">
        <h2 className="text-xl font-semibold tracking-tight text-foreground/90">
          Search Results ({totalResults})
        </h2>
        <Button
          variant="ghost"
          size="sm"
          onClick={onClear}
          className="text-sm font-medium text-muted-foreground hover:text-foreground cursor-pointer"
        >
          Clear search
        </Button>
      </div>

      {isLoading ? (
        <div className="flex flex-col items-center justify-center py-12 text-muted-foreground gap-2">
          <Loader2 className="h-8 w-8 animate-spin text-primary" />
          <span className="text-sm font-medium">Searching records...</span>
        </div>
      ) : totalResults > 0 ? (
        <div className="space-y-6">
          <SearchResultGroup
            icon={Users}
            label="Students"
            items={filteredStudents}
            onSelect={onSelectStudent}
            primaryText={(s) => s.name || "Unknown"}
            secondaryText={(s) => s.email ?? ""}
          />
          <SearchResultGroup
            icon={UserSquare2}
            label="Mentors"
            items={filteredMentors}
            onSelect={onSelectMentor}
            primaryText={(m) => m.name || "Unknown"}
            secondaryText={(m) => m.email ?? ""}
          />
          <SearchResultGroup
            icon={Users2}
            label="Teams"
            items={filteredTeams}
            onSelect={onSelectTeam}
            primaryText={(t) => getTeamName(t)}
            secondaryText={(t) => `${t.studentIds?.length || 0} students • ${t.mentorIds?.length || 0} mentors`}
          />
          <SearchResultGroup
            icon={FolderRoot}
            label="Projects"
            items={filteredProjects}
            onSelect={onSelectProject}
            primaryText={(p) => p.name || "Unknown"}
            secondaryText={(p) => p.description ?? ""}
          />
        </div>
      ) : (
        <div className="text-center p-12 bg-card rounded-2xl border border-dashed border-border/60 text-muted-foreground">
          No matching records found. Try searching by name, email, or project description.
        </div>
      )}
    </div>
  );
}
