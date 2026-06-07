import type { TeamDTO } from "../../api/api";
import { getDayName, formatTime } from "../../lib/date-utils";
import { GenericEntityList } from "./GenericEntityList";

export default function TeamsList({
  teamsPromise,
  onEdit,
  onDelete,
}: {
  teamsPromise: Promise<TeamDTO[]>;
  onEdit: (team: TeamDTO) => void;
  onDelete: (id: string) => void;
}) {
  return (
    <GenericEntityList
      dataPromise={teamsPromise}
      onEdit={onEdit}
      onDelete={onDelete}
      renderContent={(team) => (
        <div className="flex flex-col gap-1">
          <span className="text-lg font-medium text-foreground/90">
            Team - {getDayName(team.weeklyMeetingDay)} at {formatTime(team.weeklyMeetingTime)}
          </span>
          <span className="text-xs text-muted-foreground">
            {team.students?.length || 0} students • {team.mentors?.length || 0} mentors
          </span>
        </div>
      )}
      emptyMessage="No teams found."
    />
  );
}
