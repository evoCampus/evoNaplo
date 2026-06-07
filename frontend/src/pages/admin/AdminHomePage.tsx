import React, { useState, useEffect } from "react";
import {
  Search, Download, Upload, Users, UserSquare2,
  Users2, FolderRoot, CalendarDays, X
} from "lucide-react";
import { Button, Input } from "@evonaplo/ui-library";
import { useNavigate } from "react-router";
import { useApiClient } from "../../hooks/use-api-client";
import type { StudentDTO, MentorDTO, TeamDTO, ProjectDTO } from "../../api";
import DashboardSearchResults from "../../components/admin/DashboardSearchResults";
import DashboardQuickActions from "../../components/admin/DashboardQuickActions";
import { useAdminSearch } from "../../hooks/use-admin-search";

export default function AdminHomePage() {
  const navigate = useNavigate();
  const apiClient = useApiClient();

  const [searchQuery, setSearchQuery] = useState("");
  const [students, setStudents] = useState<StudentDTO[]>([]);
  const [mentors, setMentors] = useState<MentorDTO[]>([]);
  const [teams, setTeams] = useState<TeamDTO[]>([]);
  const [projects, setProjects] = useState<ProjectDTO[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    let active = true;
    const loadData = async () => {
      try {
        setIsLoading(true);
        const [studentsRes, mentorsRes, teamsRes, projectsRes] = await Promise.all([
          apiClient.students.apiStudentsGet(),
          apiClient.mentors.apiMentorsGet(),
          apiClient.teams.apiTeamsGet(),
          apiClient.projects.apiProjectsGet(),
        ]);
        if (active) {
          setStudents(studentsRes.data || []);
          setMentors(mentorsRes.data || []);
          setTeams(teamsRes.data || []);
          setProjects(projectsRes.data || []);
        }
      } catch (error) {
        console.error("Failed to load search data:", error);
      } finally {
        if (active) {
          setIsLoading(false);
        }
      }
    };
    loadData();
    return () => {
      active = false;
    };
  }, [apiClient]);

  const actions = [
    {
      title: "Add Student",
      description: "Register a new student to the system",
      icon: Users,
      onClick: () => navigate("/admin/students", { state: { openAdd: true } }),
      keywords: ["student", "add", "new", "register"],
      isAdd: true,
    },
    {
      title: "Add Mentor",
      description: "Add a new mentor for guidance",
      icon: UserSquare2,
      onClick: () => navigate("/admin/mentors", { state: { openAdd: true } }),
      keywords: ["mentor", "add", "new", "register"],
      isAdd: true,
    },
    {
      title: "Create Team",
      description: "Form a new team of students and mentors",
      icon: Users2,
      onClick: () => navigate("/admin/teams", { state: { openAdd: true } }),
      keywords: ["team", "create", "new", "group"],
      isAdd: true,
    },
    {
      title: "Add Project",
      description: "Define a new project for teams to work on",
      icon: FolderRoot,
      onClick: () => navigate("/admin/projects", { state: { openAdd: true } }),
      keywords: ["project", "add", "new", "assignment"],
      isAdd: true,
    },
    {
      title: "Manage Semesters",
      description: "Configure available semesters and current semester",
      icon: CalendarDays,
      onClick: () => navigate("/admin/semesters"),
      keywords: ["semester", "manage", "configure", "time"],
      isAdd: false,
    },
  ];

  const filteredActions = actions.filter((action) => {
    const query = searchQuery.toLowerCase();
    return (
      action.title.toLowerCase().includes(query) ||
      action.description.toLowerCase().includes(query) ||
      action.keywords.some((keyword) => keyword.includes(query))
    );
  });

  // Global search filtering
  const {
    filteredStudents,
    filteredMentors,
    filteredTeams,
    filteredProjects,
    totalResults,
    isSearching,
  } = useAdminSearch({
    searchQuery,
    students,
    mentors,
    teams,
    projects,
  });

  return (
    <div className="max-w-6xl w-full mx-auto py-4 space-y-10">
      {/* Header */}
      <div className="flex flex-col gap-1.5 mb-2">
        <h1 className="text-3xl font-bold tracking-tight">Admin Dashboard</h1>
        <p className="text-muted-foreground text-sm">
          Quickly access and manage students, mentors, teams, and semesters.
        </p>
      </div>

      {/* Search Bar */}
      <div className="relative w-full max-w-xl">
        <Search className="absolute left-3 top-1/2 -translate-y-1/2 h-4 w-4 text-muted-foreground" />
        <Input
          placeholder="Search students, mentors, teams, semesters..."
          value={searchQuery}
          onChange={(e: React.ChangeEvent<HTMLInputElement>) => setSearchQuery(e.target.value)}
          className="pl-10 pr-10 h-12 bg-card border border-border/80 hover:border-border rounded-xl text-base shadow-sm focus-visible:border-primary focus-visible:ring-3 focus-visible:ring-primary/20 transition-all placeholder:text-muted-foreground/70"
        />
        {searchQuery && (
          <button
            onClick={() => setSearchQuery("")}
            className="absolute right-3 top-1/2 -translate-y-1/2 text-muted-foreground hover:text-foreground p-1 rounded-full hover:bg-muted transition-colors cursor-pointer"
          >
            <X className="h-4 w-4" />
          </button>
        )}
      </div>

      {/* Search Results State */}
      {isSearching ? (
        <DashboardSearchResults
          isLoading={isLoading}
          totalResults={totalResults}
          filteredStudents={filteredStudents}
          filteredMentors={filteredMentors}
          filteredTeams={filteredTeams}
          filteredProjects={filteredProjects}
          onSelectStudent={(student) => navigate("/admin/students", { state: { editItem: student } })}
          onSelectMentor={(mentor) => navigate("/admin/mentors", { state: { editItem: mentor } })}
          onSelectTeam={(team) => navigate("/admin/teams", { state: { editItem: team } })}
          onSelectProject={(project) => navigate("/admin/projects", { state: { editItem: project } })}
          onClear={() => setSearchQuery("")}
        />
      ) : (
        /* Default Dashboard State */
        <>
          <DashboardQuickActions filteredActions={filteredActions} />

          {/* Data Import/Export */}
          <div className="bg-card rounded-2xl border border-border/20 p-6 shadow-sm space-y-4">
            <div className="space-y-1">
              <h2 className="text-xl font-semibold tracking-tight text-foreground/95">Data Management</h2>
              <p className="text-sm text-muted-foreground">Import or export system records using Excel spreadsheets.</p>
            </div>
            <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
              <Button
                variant="outline"
                className="h-12 text-base font-medium border-border/50 hover:bg-muted/80 hover:text-foreground cursor-pointer rounded-xl flex items-center justify-center gap-2"
              >
                <Download className="h-5 w-5" /> Import from XLSX
              </Button>
              <Button
                variant="outline"
                className="h-12 text-base font-medium border-border/50 hover:bg-muted/80 hover:text-foreground cursor-pointer rounded-xl flex items-center justify-center gap-2"
              >
                <Upload className="h-5 w-5" /> Export to XLSX
              </Button>
            </div>
          </div>
        </>
      )}
    </div>
  );
}
