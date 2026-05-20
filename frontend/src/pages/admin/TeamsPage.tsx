import { useState, Suspense, useTransition, useMemo, useEffect } from "react";
import { useLocation, useNavigate } from "react-router";
import { Button } from "@evonaplo/ui-library";
import { Plus, SlidersHorizontal } from "lucide-react";
import { useApiClient } from "../../hooks/use-api-client";
import type { TeamDTO } from "../../api";
import { TeamDialog } from "../../components/admin/TeamDialog";
import TeamsList from "../../components/admin/TeamsList";
import GenericConfirmDialog from "src/components/GenericConfirmDialog";
import ErrorBoundary from "../../components/ErrorBoundary";

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

  const initialPromise = useMemo(() => {
    return (async () => {
      const res = await apiClient.teams.apiTeamsGet();
      return res.data;
    })();
  }, [apiClient]);

  const [teamsPromise, setTeamsPromise] = useState<Promise<TeamDTO[]>>(initialPromise);

  const shouldOpenAdd = location.state?.openAdd === true;

  const [selectedTeam, setSelectedTeam] = useState<TeamDTO | null>(shouldOpenAdd ? DEFAULT_TEAM : null);
  const [isDialogOpen, setIsDialogOpen] = useState(shouldOpenAdd);
  const [isCreating, setIsCreating] = useState(shouldOpenAdd);

  const [teamToDelete, setTeamToDelete] = useState<string | null>(null);
  const [deleteError, setDeleteError] = useState<string | null>(null);

  const triggerRefresh = () => {
    const promise = apiClient.teams.apiTeamsGet().then(res => res.data);
    setTeamsPromise(promise);
    return promise;
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
    if (location.state?.openAdd) {
      navigate(location.pathname, { replace: true, state: {} });
    }
  }, [location.state?.openAdd, location.pathname, navigate]);

  const handleSave = async (team: TeamDTO, selectedProjectIds: string[]) => {
    try {
      let savedTeamId = team.id;
      if (isCreating) {
        const res = await apiClient.teams.apiTeamsPost(team);
        savedTeamId = res.data.id;
      } else {
        await apiClient.teams.apiTeamsIdPut(team.id!, team);
      }

      if (savedTeamId) {
        const projectsRes = await apiClient.projects.apiProjectsGet();
        const allProjects = projectsRes.data;

        await Promise.all(
          allProjects.map(async (project) => {
            const isSelected = selectedProjectIds.includes(project.id!);
            const hasTeam = project.teams?.includes(savedTeamId!) ?? false;

            if (isSelected && !hasTeam) {
              const updatedTeams = [...(project.teams || []), savedTeamId!];
              await apiClient.projects.apiProjectsIdPut(project.id!, {
                ...project,
                teams: updatedTeams,
              });
            } else if (!isSelected && hasTeam) {
              const updatedTeams = (project.teams || []).filter((id) => id !== savedTeamId);
              await apiClient.projects.apiProjectsIdPut(project.id!, {
                ...project,
                teams: updatedTeams,
              });
            }
          })
        );
      }

      setIsDialogOpen(false);
      startTransition(async () => {
        await triggerRefresh();
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
        <h1 className="text-3xl font-bold tracking-tight">Teams</h1>
        <div className="flex items-center gap-6">
          <button className="flex items-center gap-2 text-muted-foreground hover:text-foreground transition-colors">
            <span className="text-sm font-medium">Filters</span>
            <SlidersHorizontal className="w-4 h-4" />
          </button>
          <Button onClick={handleAdd} className="bg-primary hover:bg-primary/90 cursor-pointer text-primary-foreground px-5 h-10 rounded-lg">
            <Plus className="w-4 h-4 mr-1" />
            Add
          </Button>
        </div>
      </div>

      <ErrorBoundary onReset={triggerRefresh}>
        <Suspense fallback={<div className="text-center p-8 text-muted-foreground">Loading teams...</div>}>
          <div className={isPending ? "opacity-50 pointer-events-none transition-opacity" : "transition-opacity"}>
            <TeamsList teamsPromise={teamsPromise} onEdit={handleEdit} onDelete={handleDeleteRequest} />
          </div>
        </Suspense>
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
