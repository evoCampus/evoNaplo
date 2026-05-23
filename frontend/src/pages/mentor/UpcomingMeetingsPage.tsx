import { Loader2 } from "lucide-react";
import { useMemo, Suspense, useState, useTransition } from "react";
import { useApiClient } from "../../hooks/use-api-client";
import ErrorBoundary from "../../components/ErrorBoundary";
import { type UpcomingMeetingsData, type UIMeeting } from "../../types";
import UpcomingMeetingsContent from "../../components/mentor/UpcomingMeetingsContent";
import { getMeetingDates, formatTime } from "../../lib/date-utils";
import { useUser } from "../../hooks/use-user";

export default function UpcomingMeetingsPage() {
  const apiClient = useApiClient();
  const { user } = useUser();
  const [isPending, startTransition] = useTransition();

  const initialPromise = useMemo(() => {
    return (async (): Promise<UpcomingMeetingsData> => {
      if (!user) throw new Error("User not authenticated");
      const { data: mentor } = await apiClient.mentors.apiMentorsIdGet(user.id);
      const meetings: UIMeeting[] = [];

      if (mentor.projects && mentor.projects.length > 0) {
        for (const projectId of mentor.projects) {
          try {
            const { data: project } = await apiClient.projects.apiProjectsIdGet(projectId);
            if (project.teams && project.teams.length > 0) {
              for (const teamId of project.teams) {
                try {
                  const { data: team } = await apiClient.teams.apiTeamsIdGet(teamId);
                  if (team.weeklyMeetingDay !== undefined && team.weeklyMeetingTime) {
                    let studentDetails: { id: string, name: string }[] = [];
                    if (team.students && team.students.length > 0) {
                      const studentPromises = team.students.map(sid =>
                        apiClient.students.apiStudentsIdGet(sid).then(res => ({
                          id: sid,
                          name: res.data.name || "Unknown Student"
                        }))
                      );
                      studentDetails = await Promise.all(studentPromises);
                    }

                    const dates = getMeetingDates(team.weeklyMeetingDay);
                    const savedRecord = (team.attendance || []).find(record => record[0] === dates.raw);
                    const presentStudentIds = savedRecord ? savedRecord.slice(1) : studentDetails.map(s => s.id);
                    const studentsWithAttendance = studentDetails.map(student => ({
                      ...student,
                      isPresent: presentStudentIds.includes(student.id)
                    }));

                    meetings.push({
                      id: `${projectId}-${teamId}`,
                      projectId: project.id || projectId,
                      projectName: project.name || "Unknown Project",
                      teamName: team.id || teamId,
                      location: "Evosoft Miskolc", 
                      date: dates.formatted,
                      rawDate: dates.raw,
                      time: formatTime(team.weeklyMeetingTime),
                      students: studentsWithAttendance
                    });
                  }
                } catch (e) {
                  console.error(`Failed to fetch team ${teamId}`, e);
                }
              }
            }
          } catch (e) {
            console.error(`Failed to fetch project ${projectId}`, e);
          }
        }
      }

      return {
        mentorName: mentor.name || "Mentor",
        meetings
      };
    })();
  }, [apiClient, user]);

  const [meetingsPromise, setMeetingsPromise] = useState<Promise<UpcomingMeetingsData>>(initialPromise);

  const triggerRefresh = () => {
    const promise = (async (): Promise<UpcomingMeetingsData> => {
      if (!user) throw new Error("User not authenticated");
      const { data: mentor } = await apiClient.mentors.apiMentorsIdGet(user.id);
      const meetings: UIMeeting[] = [];

      if (mentor.projects && mentor.projects.length > 0) {
        for (const projectId of mentor.projects) {
          try {
            const { data: project } = await apiClient.projects.apiProjectsIdGet(projectId);
            if (project.teams && project.teams.length > 0) {
              for (const teamId of project.teams) {
                try {
                  const { data: team } = await apiClient.teams.apiTeamsIdGet(teamId);
                  if (team.weeklyMeetingDay !== undefined && team.weeklyMeetingTime) {
                    let studentDetails: { id: string, name: string }[] = [];
                    if (team.students && team.students.length > 0) {
                      const studentPromises = team.students.map(sid =>
                        apiClient.students.apiStudentsIdGet(sid).then(res => ({
                          id: sid,
                          name: res.data.name || "Unknown Student"
                        }))
                      );
                      studentDetails = await Promise.all(studentPromises);
                    }

                    const dates = getMeetingDates(team.weeklyMeetingDay);
                    const savedRecord = (team.attendance || []).find(record => record[0] === dates.raw);
                    const presentStudentIds = savedRecord ? savedRecord.slice(1) : studentDetails.map(s => s.id);
                    const studentsWithAttendance = studentDetails.map(student => ({
                      ...student,
                      isPresent: presentStudentIds.includes(student.id)
                    }));

                    meetings.push({
                      id: `${projectId}-${teamId}`,
                      projectId: project.id || projectId,
                      projectName: project.name || "Unknown Project",
                      teamName: team.id || teamId,
                      location: "Evosoft Miskolc", 
                      date: dates.formatted,
                      rawDate: dates.raw,
                      time: formatTime(team.weeklyMeetingTime),
                      students: studentsWithAttendance
                    });
                  }
                } catch (e) {
                  console.error(`Failed to fetch team ${teamId}`, e);
                }
              }
            }
          } catch (e) {
            console.error(`Failed to fetch project ${projectId}`, e);
          }
        }
      }

      return {
        mentorName: mentor.name || "Mentor",
        meetings
      };
    })();

    setMeetingsPromise(promise);
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
        <div className="flex flex-col items-center justify-center min-h-[400px] gap-4">
          <Loader2 className="w-8 h-8 animate-spin text-primary" />
          <p className="text-muted-foreground font-medium">Loading your meetings...</p>
        </div>
      }>
        <div className={isPending ? "opacity-50 pointer-events-none transition-opacity" : "transition-opacity"}>
          <UpcomingMeetingsContent dataPromise={meetingsPromise} onRefresh={handleRefresh} />
        </div>
      </Suspense>
    </ErrorBoundary>
  );
}
