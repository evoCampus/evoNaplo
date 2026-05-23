import { Loader2 } from "lucide-react";
import { useParams } from "react-router";
import { useMemo, Suspense, useState, useTransition } from "react";
import { useApiClient } from "../../hooks/use-api-client";
import ErrorBoundary from "../../components/ErrorBoundary";
import { type ProjectDetailedData, type TeamWithMembers } from "../../types";
import ProjectDetailsContent from "../../components/mentor/ProjectDetailsContent";

export default function ProjectDetailsPage() {
  const { id } = useParams<{ id: string }>();
  const apiClient = useApiClient();
  const [isPending, startTransition] = useTransition();

  const initialPromise = useMemo(() => {
    if (!id) return Promise.reject(new Error("Project ID is required"));

    return (async (): Promise<ProjectDetailedData> => {
      const { data: project } = await apiClient.projects.apiProjectsIdGet(id);

      let teamsWithMembers: TeamWithMembers[] = [];
      if (project.teams && project.teams.length > 0) {
        const teamPromises = project.teams.map(async (teamId) => {
          const { data: teamData } = await apiClient.teams.apiTeamsIdGet(teamId);

          let memberNames: string[] = [];
          if (teamData.students && teamData.students.length > 0) {
            const studentPromises = teamData.students.map(sid =>
              apiClient.students.apiStudentsIdGet(sid).then(res => res.data.name || "Unknown Student")
            );
            memberNames = await Promise.all(studentPromises);
          }

          return { ...teamData, memberNames };
        });

        teamsWithMembers = await Promise.all(teamPromises);
      }

      return { project, teams: teamsWithMembers };
    })();
  }, [id, apiClient]);

  const [dataPromise, setDataPromise] = useState<Promise<ProjectDetailedData>>(initialPromise);

  const triggerRefresh = () => {
    if (!id) return;
    const promise = (async (): Promise<ProjectDetailedData> => {
      const { data: project } = await apiClient.projects.apiProjectsIdGet(id);

      let teamsWithMembers: TeamWithMembers[] = [];
      if (project.teams && project.teams.length > 0) {
        const teamPromises = project.teams.map(async (teamId) => {
          const { data: teamData } = await apiClient.teams.apiTeamsIdGet(teamId);

          let memberNames: string[] = [];
          if (teamData.students && teamData.students.length > 0) {
            const studentPromises = teamData.students.map(sid =>
              apiClient.students.apiStudentsIdGet(sid).then(res => res.data.name || "Unknown Student")
            );
            memberNames = await Promise.all(studentPromises);
          }

          return { ...teamData, memberNames };
        });

        teamsWithMembers = await Promise.all(teamPromises);
      }

      return { project, teams: teamsWithMembers };
    })();

    setDataPromise(promise);
    return promise;
  };

  const handleRefresh = () => {
    startTransition(async () => {
      await triggerRefresh();
    });
  };

  return (
    <ErrorBoundary onReset={handleRefresh}>
      <Suspense fallback={
        <div className="flex flex-col items-center justify-center min-h-100 gap-4">
          <Loader2 className="w-8 h-8 animate-spin text-primary" />
          <p className="text-muted-foreground font-medium">Loading project details...</p>
        </div>
      }>
        <div className={isPending ? "opacity-50 pointer-events-none transition-opacity" : "transition-opacity"}>
          <ProjectDetailsContent dataPromise={dataPromise} />
        </div>
      </Suspense>
    </ErrorBoundary>
  );
}
