import { Users, UserSquare2, Users2, FolderRoot, ArrowRight, Loader2 } from "lucide-react";
import { Button } from "@evonaplo/ui-library";
import type { StudentDTO, MentorDTO, TeamDTO, ProjectDTO } from "../../api";
import { getDayName, formatTime } from "../../lib/date-utils";

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
          {/* Students Group */}
          {filteredStudents.length > 0 && (
            <div className="space-y-3">
              <h3 className="text-sm font-semibold text-muted-foreground flex items-center gap-2 px-1">
                <Users className="h-4 w-4" /> Students ({filteredStudents.length})
              </h3>
              <div className="grid grid-cols-1 md:grid-cols-2 gap-3">
                {filteredStudents.map((student) => (
                  <div
                    key={student.id}
                    onClick={() => onSelectStudent(student)}
                    className="flex items-center justify-between p-4 bg-card rounded-xl border border-transparent hover:border-border/50 hover:shadow-sm transition-all cursor-pointer group"
                  >
                    <div className="flex flex-col gap-0.5">
                      <span className="font-medium text-foreground/90 group-hover:text-primary transition-colors">
                        {student.name || "Unknown"}
                      </span>
                      <span className="text-xs text-muted-foreground">{student.email}</span>
                    </div>
                    <ArrowRight className="h-4 w-4 text-muted-foreground opacity-0 group-hover:opacity-100 transition-opacity" />
                  </div>
                ))}
              </div>
            </div>
          )}

          {/* Mentors Group */}
          {filteredMentors.length > 0 && (
            <div className="space-y-3">
              <h3 className="text-sm font-semibold text-muted-foreground flex items-center gap-2 px-1">
                <UserSquare2 className="h-4 w-4" /> Mentors ({filteredMentors.length})
              </h3>
              <div className="grid grid-cols-1 md:grid-cols-2 gap-3">
                {filteredMentors.map((mentor) => (
                  <div
                    key={mentor.id}
                    onClick={() => onSelectMentor(mentor)}
                    className="flex items-center justify-between p-4 bg-card rounded-xl border border-transparent hover:border-border/50 hover:shadow-sm transition-all cursor-pointer group"
                  >
                    <div className="flex flex-col gap-0.5">
                      <span className="font-medium text-foreground/90 group-hover:text-primary transition-colors">
                        {mentor.name || "Unknown"}
                      </span>
                      <span className="text-xs text-muted-foreground">{mentor.email}</span>
                    </div>
                    <ArrowRight className="h-4 w-4 text-muted-foreground opacity-0 group-hover:opacity-100 transition-opacity" />
                  </div>
                ))}
              </div>
            </div>
          )}

          {/* Teams Group */}
          {filteredTeams.length > 0 && (
            <div className="space-y-3">
              <h3 className="text-sm font-semibold text-muted-foreground flex items-center gap-2 px-1">
                <Users2 className="h-4 w-4" /> Teams ({filteredTeams.length})
              </h3>
              <div className="grid grid-cols-1 md:grid-cols-2 gap-3">
                {filteredTeams.map((team) => (
                  <div
                    key={team.id}
                    onClick={() => onSelectTeam(team)}
                    className="flex items-center justify-between p-4 bg-card rounded-xl border border-transparent hover:border-border/50 hover:shadow-sm transition-all cursor-pointer group"
                  >
                    <div className="flex flex-col gap-0.5">
                      <span className="font-medium text-foreground/90 group-hover:text-primary transition-colors">
                        {getTeamName(team)}
                      </span>
                      <span className="text-xs text-muted-foreground">
                        {team.students?.length || 0} students • {team.mentors?.length || 0} mentors
                      </span>
                    </div>
                    <ArrowRight className="h-4 w-4 text-muted-foreground opacity-0 group-hover:opacity-100 transition-opacity" />
                  </div>
                ))}
              </div>
            </div>
          )}

          {/* Projects Group */}
          {filteredProjects.length > 0 && (
            <div className="space-y-3">
              <h3 className="text-sm font-semibold text-muted-foreground flex items-center gap-2 px-1">
                <FolderRoot className="h-4 w-4" /> Projects ({filteredProjects.length})
              </h3>
              <div className="grid grid-cols-1 md:grid-cols-2 gap-3">
                {filteredProjects.map((project) => (
                  <div
                    key={project.id}
                    onClick={() => onSelectProject(project)}
                    className="flex items-center justify-between p-4 bg-card rounded-xl border border-transparent hover:border-border/50 hover:shadow-sm transition-all cursor-pointer group"
                  >
                    <div className="flex flex-col gap-0.5">
                      <span className="font-medium text-foreground/90 group-hover:text-primary transition-colors">
                        {project.name || "Unknown"}
                      </span>
                      <span className="text-xs text-muted-foreground line-clamp-1">{project.description}</span>
                    </div>
                    <ArrowRight className="h-4 w-4 text-muted-foreground opacity-0 group-hover:opacity-100 transition-opacity" />
                  </div>
                ))}
              </div>
            </div>
          )}
        </div>
      ) : (
        <div className="text-center p-12 bg-card rounded-2xl border border-dashed border-border/60 text-muted-foreground">
          No matching records found. Try searching by name, email, or project description.
        </div>
      )}
    </div>
  );
}
