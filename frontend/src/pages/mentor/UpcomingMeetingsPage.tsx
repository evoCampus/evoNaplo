import { useMemo, useState, useTransition, useCallback } from "react";
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

  const fetchAllData = useCallback(async (): Promise<UpcomingMeetingsData> => {
    if (!user) throw new Error("User not authenticated");

    const { data: mentor } = await apiClient.mentors.apiMentorsIdGet(user.id);

    const fetchStudentDetails = async (studentIds: string[]) => {
          const promises = studentIds.map(async (sid) => {
            const { data: student } = await apiClient.students.apiStudentsStudentIdGet(sid);

            return {
              id: student.id ?? sid,
              name: student.name || "Unknown Student"
            };
          });
          return Promise.all(promises);
        };

    const fetchTeamMeeting = async (projectId: string, projectName: string, teamId: string): Promise<UIMeeting | null> => {
      try {
        const { data: team } = await apiClient.teams.apiTeamsTeamIdGet(teamId);
        if (team.weeklyMeetingDay === undefined || !team.weeklyMeetingTime) return null;

        const studentDetails = await fetchStudentDetails(team.studentIds || []);
        const dates = getMeetingDates(team.weeklyMeetingDay);
        const savedRecord = (team.attendanceSheetIds || []).find((record) => record.split("|")[0] === dates.raw);
        const presentStudentIds = savedRecord
          ? (savedRecord.split("|")[1] || "").split(",").filter(Boolean)
          : studentDetails.map((s) => s.id);

        return {
          id: `${projectId}-${teamId}`,
          projectId,
          projectName,
          teamName: team.id || teamId,
          location: "Evosoft Miskolc",
          date: dates.formatted,
          rawDate: dates.raw,
          time: formatTime(team.weeklyMeetingTime),
          students: studentDetails.map(student => ({
            ...student,
            isPresent: presentStudentIds.includes(student.id)
          }))
        };
      } catch (e) {
        console.error(`Failed to fetch team ${teamId}`, e);
        return null;
      }
    };

    const fetchProjectMeetings = async (projectId: string): Promise<UIMeeting[]> => {
      try {
        const { data: project } = await apiClient.projects.apiProjectsProjectIdGet(projectId);
        if (!project.teamIds || project.teamIds.length === 0) return [];

        const teamMeetings = await Promise.all(
          project.teamIds.map(teamId => fetchTeamMeeting(projectId, project.name || "Unknown Project", teamId))
        );
        return teamMeetings.filter((m): m is UIMeeting => m !== null);
      } catch (e) {
        console.error(`Failed to fetch project ${projectId}`, e);
        return [];
      }
    };

    const meetingsByProject = await Promise.all(
      (mentor.projectIds || []).map(fetchProjectMeetings)
    );

    return {
      mentorName: mentor.name || "Mentor",
      meetings: meetingsByProject.flat()
    };
  }, [apiClient, user]);

  const initialPromise = useMemo(() => fetchAllData(), [fetchAllData]);
  const [meetingsPromise, setMeetingsPromise] = useState<Promise<UpcomingMeetingsData>>(initialPromise);

  const handleRefresh = () => {
    startTransition(async () => {
      try {
        setMeetingsPromise(fetchAllData());
      } catch (error) {
        console.error("Failed to refresh meetings:", error);
      }
    });
  };

  return (
    <ErrorBoundary onReset={handleRefresh}>
      <div className={isPending ? "opacity-50 pointer-events-none transition-opacity" : "transition-opacity"}>
        <UpcomingMeetingsContent dataPromise={meetingsPromise} onRefresh={handleRefresh} />
      </div>
    </ErrorBoundary>
  );
}
