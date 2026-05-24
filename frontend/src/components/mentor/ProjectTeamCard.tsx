import { Users2, Clock, Calendar, User, MapPin } from "lucide-react";
import { type TeamWithMembers } from "../../types";
import { getDayName, formatTime } from "../../lib/date-utils";

export default function ProjectTeamCard({ team }: { team: TeamWithMembers }) {
  return (
    <div className="space-y-8">
      <div className="border-t border-border pt-8">
        <h3 className="text-2xl font-bold text-foreground mb-4">{team.id}</h3>

        <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
          {/* Members */}
          <div className="bg-card rounded-2xl p-6 space-y-4 border border-transparent hover:border-border/50 transition-all shadow-sm">
            <div className="flex items-center gap-3 text-foreground">
              <Users2 className="w-6 h-6 text-primary" />
              <h2 className="text-xl font-bold tracking-tight">Members</h2>
            </div>
            <div className="space-y-3 pl-9">
              {team.memberNames.length > 0 ? (
                team.memberNames.map((name, index) => (
                  <div key={index} className="flex items-center gap-3 text-foreground/90">
                    <div className="bg-muted p-1 rounded-full">
                      <User className="w-4 h-4" />
                    </div>
                    <span className="text-lg">{name}</span>
                  </div>
                ))
              ) : (
                <p className="text-muted-foreground italic">No members assigned yet.</p>
              )}
            </div>
          </div>

          {/* Team Meetings */}
          <div className="bg-card rounded-2xl p-6 space-y-4 border border-transparent hover:border-border/50 transition-all shadow-sm">
            <div className="flex items-center gap-3 text-foreground">
              <Calendar className="w-6 h-6 text-primary" />
              <h2 className="text-xl font-bold tracking-tight">Team meetings</h2>
            </div>
            <div className="space-y-3 pl-9">
              <div className="flex items-center gap-3 text-foreground/80">
                <Clock className="w-4 h-4 text-primary/70" />
                <span className="text-lg">
                  {getDayName(team.weeklyMeetingDay)} {formatTime(team.weeklyMeetingTime)}
                </span>
              </div>
              <div className="flex items-center gap-3 text-foreground/80">
                <MapPin className="w-4 h-4 text-primary/70" />
                <span className="text-lg">Evosoft Miskolc (Default)</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
