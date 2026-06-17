import { useMemo, useState, useTransition } from "react";
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
      const { data: mentor } = await apiClient.mentors.getMentor(user.id);
      const teams: UITeam[] = [];

      if (mentor.projectIds && mentor.projectIds.length > 0) {
        for (const projectId of mentor.projectIds) {
          try {
            const { data: project } = await apiClient.projects.getProject(projectId);
            if (project.teamIds && project.teamIds.length > 0) {
              for (const teamId of project.teamIds) {
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
      const { data: mentor } = await apiClient.mentors.getMentor(user.id);
      const teams: UITeam[] = [];

      if (mentor.projectIds && mentor.projectIds.length > 0) {
        for (const projectId of mentor.projectIds) {
          try {
            const { data: project } = await apiClient.projects.getProject(projectId);
            if (project.teamIds && project.teamIds.length > 0) {
              for (const teamId of project.teamIds) {
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
      try {
        await triggerRefresh();
      } catch (error) {
        console.error("Failed to refresh home data:", error);
      }
    });
  };

  return (
    <ErrorBoundary onReset={handleRefresh}>
      <div className={isPending ? "opacity-50 pointer-events-none transition-opacity" : "transition-opacity"}>
        <MentorHomeContent dataPromise={dataPromise} />
      </div>
    </ErrorBoundary>
  );
}
