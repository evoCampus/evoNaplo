import { Loader2 } from "lucide-react";
import { useMemo, Suspense, useState, useTransition } from "react";
import { useApiClient } from "../../hooks/use-api-client";
import { useUser } from "../../hooks/use-user";
import ErrorBoundary from "../../components/ErrorBoundary";
import { type MentorHomeData, type UITeam } from "../../types";
import MentorHomeContent from "../../components/mentor/MentorHomeContent";

export default function MentorHomePage() {
  const apiClient = useApiClient();
  const { user } = useUser();
  const [isPending, startTransition] = useTransition();

  const initialPromise = useMemo(() => {
    return (async (): Promise<MentorHomeData> => {
      if (!user) throw new Error("User not authenticated");
      const { data: mentor } = await apiClient.mentors.apiMentorsIdGet(user.id);
      const teams: UITeam[] = [];

      if (mentor.projects && mentor.projects.length > 0) {
        for (const projectId of mentor.projects) {
          try {
            const { data: project } = await apiClient.projects.apiProjectsIdGet(projectId);
            if (project.teams && project.teams.length > 0) {
              for (const teamId of project.teams) {
                teams.push({
                  id: teamId,
                  name: teamId,
                  projectId: project.id || projectId
                });
              }
            }
          } catch (e) {
            console.error(`Failed to fetch project ${projectId}`, e);
          }
        }
      }

      return {
        mentorName: mentor.name || "Mentor",
        teams
      };
    })();
  }, [apiClient, user]);

  const [dataPromise, setDataPromise] = useState<Promise<MentorHomeData>>(initialPromise);

  const triggerRefresh = () => {
    const promise = (async (): Promise<MentorHomeData> => {
      if (!user) throw new Error("User not authenticated");
      const { data: mentor } = await apiClient.mentors.apiMentorsIdGet(user.id);
      const teams: UITeam[] = [];

      if (mentor.projects && mentor.projects.length > 0) {
        for (const projectId of mentor.projects) {
          try {
            const { data: project } = await apiClient.projects.apiProjectsIdGet(projectId);
            if (project.teams && project.teams.length > 0) {
              for (const teamId of project.teams) {
                teams.push({
                  id: teamId,
                  name: teamId,
                  projectId: project.id || projectId
                });
              }
            }
          } catch (e) {
            console.error(`Failed to fetch project ${projectId}`, e);
          }
        }
      }

      return {
        mentorName: mentor.name || "Mentor",
        teams
      };
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
          <p className="text-muted-foreground font-medium">Loading your dashboard...</p>
        </div>
      }>
        <div className={isPending ? "opacity-50 pointer-events-none transition-opacity" : "transition-opacity"}>
          <MentorHomeContent dataPromise={dataPromise} />
        </div>
      </Suspense>
    </ErrorBoundary>
  );
}
