import { Trash2 } from "lucide-react";
import { use } from "react";
import type { TeamDTO } from "../../api/api";
import { getDayName, formatTime } from "../../lib/date-utils";

export default function TeamsList({
  teamsPromise,
  onEdit,
  onDelete,
}: {
  teamsPromise: Promise<TeamDTO[]>;
  onEdit: (team: TeamDTO) => void;
  onDelete: (id: string) => void;
}) {
  const teams = use(teamsPromise);

  return (
    <div className="grid gap-3">
      {teams.map((team) => (
        <div
          key={team.id}
          className="flex items-center justify-between p-5 bg-card rounded-2xl border border-transparent hover:border-border/50 transition-all group shadow-sm hover:shadow-md cursor-pointer"
          onClick={() => onEdit(team)}
        >
          <div className="flex flex-col gap-1">
            <span className="text-lg font-medium text-foreground/90">
              Team - {getDayName(team.weeklyMeetingDay)} at {formatTime(team.weeklyMeetingTime)}
            </span>
            <span className="text-xs text-muted-foreground">
              {team.students?.length || 0} students • {team.mentors?.length || 0} mentors
            </span>
          </div>
          <button
            onClick={(e) => {
              e.stopPropagation();
              onDelete(team.id!);
            }}
            className="text-muted-foreground hover:text-destructive transition-colors p-2 rounded-full hover:bg-destructive/10 hover:cursor-pointer"
          >
            <Trash2 className="w-5 h-5" />
          </button>
        </div>
      ))}
      {teams.length === 0 && (
         <div className="text-center p-8 text-muted-foreground">No teams found.</div>
      )}
    </div>
  );
}
