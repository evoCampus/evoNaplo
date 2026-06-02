import { use, useState } from "react";
import { useApiClient } from "../../hooks/use-api-client";
import MeetingInfoDialog from "./MeetingInfoDialog";
import UpcomingMeetingCard from "./UpcomingMeetingCard";
import { type UpcomingMeetingsData, type UIMeeting } from "../../types";

export default function UpcomingMeetingsContent({
  dataPromise,
  onRefresh
}: {
  dataPromise: Promise<UpcomingMeetingsData>;
  onRefresh: () => void;
}) {
  const { mentorName, meetings } = use(dataPromise);
  const [selectedMeeting, setSelectedMeeting] = useState<UIMeeting | null>(null);
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const apiClient = useApiClient();

  const handleMeetingClick = (meeting: UIMeeting) => {
    setSelectedMeeting(meeting);
    setIsDialogOpen(true);
  };

  const handleAttendanceConfirm = async (presentStudentIds: string[]) => {
    if (!selectedMeeting) return;

    const teamId = selectedMeeting.id.split('-')[1];
    const meetingDate = selectedMeeting.rawDate;

    try {
      const { data: team } = await apiClient.teams.apiTeamsIdGet(teamId);
      const newEntry = [meetingDate, ...presentStudentIds];
      const updatedAttendance = [...(team.attendance || [])] as string[][];

      const existingIndex = updatedAttendance.findIndex(record => record[0] === meetingDate);

      if (existingIndex !== -1) {
        updatedAttendance[existingIndex] = newEntry;
      } else {
        updatedAttendance.push(newEntry);
      }

      await apiClient.teams.apiTeamsIdPut(teamId, {
        ...team,
        attendance: updatedAttendance
      });

      onRefresh();
    } catch (error) {
      console.error("Failed to update attendance in backend:", error);
      throw error;
    }
  };

  return (
    <div className="max-w-5xl mx-auto py-8">
      <h1 className="text-3xl font-bold mb-8 text-foreground">Welcome {mentorName}!</h1>

      <div className="space-y-6">
        <h2 className="text-xl font-semibold text-foreground">Upcoming meetings:</h2>

        <div className="grid gap-3">
          {meetings.length > 0 ? meetings.map((meeting) => (
            <UpcomingMeetingCard
              key={meeting.id}
              meeting={meeting}
              onClick={() => handleMeetingClick(meeting)}
            />
          )) : (
            <div className="p-8 text-center bg-card rounded-2xl border border-dashed border-border/60">
              <p className="text-muted-foreground">No upcoming meetings scheduled.</p>
            </div>
          )}
        </div>
      </div>

      <MeetingInfoDialog
        key={selectedMeeting ? `${selectedMeeting.id}-${isDialogOpen}` : 'no-meeting'}
        isOpen={isDialogOpen}
        onClose={() => setIsDialogOpen(false)}
        onConfirm={handleAttendanceConfirm}
        meeting={selectedMeeting}
      />
    </div>
  );
}
