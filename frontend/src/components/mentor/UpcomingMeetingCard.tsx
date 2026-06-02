import { Users2, ChevronRight, Calendar } from "lucide-react";
import { type UIMeeting } from "../../types";

export default function UpcomingMeetingCard({ 
  meeting, 
  onClick 
}: { 
  meeting: UIMeeting; 
  onClick: () => void;
}) {
  return (
    <div
      onClick={onClick}
      className="relative flex items-center justify-between p-5 bg-card rounded-2xl cursor-pointer hover:shadow-md transition-all group border border-transparent hover:border-border/50 shadow-sm"
    >
      <div className="flex items-center gap-4">
        <div className="p-2 text-foreground/80 bg-muted/50 rounded-xl">
            <Users2 className="w-6 h-6 text-primary" />
        </div>
        <div>
          <span className="text-lg font-medium text-foreground/90 block">
            {meeting.teamName} - {meeting.location}
          </span>
          <div className="flex items-center gap-1.5 text-sm text-muted-foreground mt-0.5">
            <Calendar className="w-4 h-4" />
            <span>{meeting.date} at {meeting.time}</span>
          </div>
        </div>
      </div>
      <ChevronRight className="w-5 h-5 text-muted-foreground group-hover:text-primary transition-colors" />
    </div>
  );
}
