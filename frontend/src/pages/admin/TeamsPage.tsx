import { useState, useTransition, useMemo, useEffect } from "react";
import { useLocation, useNavigate } from "react-router";
import { Button } from "@evonaplo/ui-library";
import { Plus } from "lucide-react";
import { useApiClient } from "../../hooks/use-api-client";
import type { TeamDTO } from "../../api";
import { TeamDialog } from "../../components/admin/TeamDialog";
import TeamsList from "../../components/admin/TeamsList";
import GenericConfirmDialog from "src/components/GenericConfirmDialog";
import ErrorBoundary from "../../components/ErrorBoundary";
import { AdminFilter, type FilterField } from "../../components/admin/AdminFilter";
import { DAYS_OF_WEEK } from "../../lib/date-utils";

const teamFilterFields: FilterField<TeamDTO>[] = [
  {
    key: "weeklyMeetingDay",
    label: "Meeting Day",
    type: "select",
    options: DAYS_OF_WEEK.map((name, index) => ({ label: name, value: index })),
  },
  { key: "weeklyMeetingTime", label: "Meeting Time", type: "text" },
];

const DEFAULT_TEAM: TeamDTO = {
  id: null,
  weeklyMeetingDay: 0,
  weeklyMeetingTime: "12:00",
  mentors: [],
  students: [],
  attendance: [],
};

export default function TeamsPage() {
  const apiClient = useApiClient();
  const location = useLocation();
  const navigate = useNavigate();
  const [isPending, startTransition] = useTransition();

  const [allTeams, setAllTeams] = useState<TeamDTO[] | null>(null);
  const [filters, setFilters] = useState<Partial<Record<keyof TeamDTO, unknown>>>({});

  const initialPromise = useMemo(() => {
    return apiClient.teams.apiTeamsGet().then((res) => res.data);
  }, [apiClient]);

  useEffect(() => {
    let cancelled = false;
    initialPromise
      .then(data => { if (!cancelled) setAllTeams(data); })
      .catch(err => { if (!cancelled) console.error("Failed to load teams:", err); });
    return () => { cancelled = true; };
  }, [initialPromise]);

  const teamsPromise = useMemo(() => {
    if (allTeams === null) return initialPromise;

    const filtered = allTeams.filter((t) => {
      return Object.entries(filters).every(([key, value]) => {
        if (value === undefined || value === null || value === "") return true;
        const itemValue = t[key as keyof TeamDTO];
        
        // Handle numeric fields (like weeklyMeetingDay)
        if (typeof itemValue === "number") {
          return itemValue === Number(value);
        }

        if (typeof value === "string") {
          return itemValue?.toString().toLowerCase().includes(value.toLowerCase());
        }
        return itemValue === value;
      });
    });
    return Promise.resolve(filtered);
  }, [allTeams, filters, initialPromise]);

  const shouldOpenAdd = location.state?.openAdd === true;
  const editItem = location.state?.editItem as TeamDTO | undefined;

  const [selectedTeam, setSelectedTeam] = useState<TeamDTO | null>(
    editItem ? editItem : shouldOpenAdd ? DEFAULT_TEAM : null
  );
  const [isDialogOpen, setIsDialogOpen] = useState(shouldOpenAdd || !!editItem);
  const [isCreating, setIsCreating] = useState(shouldOpenAdd && !editItem);

  const [teamToDelete, setTeamToDelete] = useState<string | null>(null);
  const [deleteError, setDeleteError] = useState<string | null>(null);

  const triggerRefresh = async () => {
    try {
      const res = await apiClient.teams.apiTeamsGet();
      setAllTeams(res.data);
      return res.data;
    } catch (error) {
      console.error("Failed to refresh teams:", error);
      throw error;
    }
  };

  const handleEdit = (team: TeamDTO) => {
    setSelectedTeam(team);
    setIsCreating(false);
    setIsDialogOpen(true);
  };

  const handleAdd = () => {
    setSelectedTeam(DEFAULT_TEAM);
    setIsCreating(true);
    setIsDialogOpen(true);
  };

  useEffect(() => {
    if (location.state?.openAdd || location.state?.editItem) {
      navigate(location.pathname, { replace: true, state: {} });
    }
  }, [location.state?.openAdd, location.state?.editItem, location.pathname, navigate]);

  const handleSave = async (team: TeamDTO, selectedProjectIds: string[]) => {
    try {
      let savedTeamId = team.id;
      if (isCreating) {
        const res = await apiClient.teams.apiTeamsPost(team);
        savedTeamId = res.data.id;
      } else {
        if (!team.id) throw new Error("Team ID is missing");
        await apiClient.teams.apiTeamsIdPut(team.id, team);
      }

      if (savedTeamId) {
        const projectsRes = await apiClient.projects.apiProjectsGet();
        const allProjects = projectsRes.data;

        await Promise.all(
          allProjects.map(async (project) => {
            if (!project.id) return;
            const isSelected = selectedProjectIds.includes(project.id);
            const hasTeam = project.teams?.includes(savedTeamId!) ?? false;

            if (isSelected && !hasTeam) {
              const updatedTeams = [...(project.teams || []), savedTeamId!];
              await apiClient.projects.apiProjectsIdPut(project.id, {
                ...project,
                teams: updatedTeams,
              });
            } else if (!isSelected && hasTeam) {
              const updatedTeams = (project.teams || []).filter((id) => id !== savedTeamId);
              await apiClient.projects.apiProjectsIdPut(project.id, {
                ...project,
                teams: updatedTeams,
              });
            }
          })
        );
      }

      setIsDialogOpen(false);
      startTransition(async () => {
        try {
          await triggerRefresh();
        } catch (error) {
          console.error("Failed to refresh after save:", error);
        }
      });
    } catch (error) {
      console.error("Unsuccessful save:", error);
      throw error;
    }
  };

  const handleDeleteRequest = (id: string) => {
    setDeleteError(null);
    setTeamToDelete(id);
  };

  const confirmDelete = () => {
    if (!teamToDelete) return;
    setDeleteError(null);

    startTransition(async () => {
      try {
        await apiClient.teams.apiTeamsIdDelete(teamToDelete);
        await triggerRefresh();
        setTeamToDelete(null);
      } catch (error) {
        console.error("Unsuccessful delete:", error);
        setDeleteError("Failed to delete team. Please try again.");
      }
    });
  };

  return (
    <div className="max-w-6xl w-full mx-auto py-4">
      <div className="flex items-center justify-between mb-8">
        <h1 className="text-3xl font-bold tracking-tight text-foreground">Teams</h1>
        <div className="flex items-center gap-6">
          <AdminFilter
            fields={teamFilterFields}
            currentFilters={filters}
            onFilterChange={setFilters}
          />
          <Button
            onClick={handleAdd}
            className="bg-primary hover:bg-primary/90 cursor-pointer text-primary-foreground px-5 h-10 rounded-lg shadow-md transition-all active:scale-95"
          >
            <Plus className="w-4 h-4 mr-1" />
            Add
          </Button>
        </div>
      </div>

      <ErrorBoundary onReset={triggerRefresh}>
        <div
          className={
            isPending
              ? "opacity-50 pointer-events-none transition-opacity duration-200"
              : "transition-opacity duration-200"
          }
        >
          <TeamsList teamsPromise={teamsPromise} onEdit={handleEdit} onDelete={handleDeleteRequest} />
        </div>
      </ErrorBoundary>

      <TeamDialog
        item={selectedTeam}
        isOpen={isDialogOpen}
        onClose={() => setIsDialogOpen(false)}
        onSave={handleSave}
        isCreating={isCreating}
      />

      <GenericConfirmDialog
        isOpen={!!teamToDelete}
        onClose={() => setTeamToDelete(null)}
        onConfirm={confirmDelete}
        title="Delete Team"
        description="Are you sure you want to delete this team? This action cannot be undone."
        confirmText="Delete"
        isPending={isPending}
        variant="destructive"
        error={deleteError}
      />
    </div>
  );
}
