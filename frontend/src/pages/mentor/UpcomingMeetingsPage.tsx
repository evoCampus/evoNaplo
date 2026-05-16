import { Users2, ChevronRight, Calendar } from "lucide-react";

const meetings = [
  { location: "Evosoft Miskolc", date: "Sep 13, 2025", id: "1" },
  { location: "Evosoft Miskolc", date: "Sep 20, 2025", id: "2" },
];

export default function UpcomingMeetingsPage() {
  return (
    <div className="max-w-5xl mx-auto py-8">
      <h1 className="text-3xl font-bold mb-8 text-foreground">Welcome MentorName!</h1>

      <div className="space-y-6">
        <h2 className="text-xl font-semibold text-foreground">Upcoming meetings:</h2>

        <div className="grid gap-3">
          {meetings.map((meeting) => (
            <div
              key={meeting.id}
              className="relative flex items-center justify-between p-5 bg-card rounded-2xl cursor-pointer hover:shadow-md transition-all group border border-transparent hover:border-border/50"
            >
              <div className="flex items-center gap-4">
                <div className="p-1.5 text-foreground/80">
                    <Users2 className="w-5 h-5" />
                </div>
                <div>
                  <span className="text-lg font-medium text-foreground/90 block">{meeting.location}</span>
                  <div className="flex items-center gap-1.5 text-sm text-muted-foreground mt-0.5">
                    <Calendar className="w-3.5 h-3.5" />
                    <span>{meeting.date}</span>
                  </div>
                </div>
              </div>
              <ChevronRight className="w-5 h-5 text-muted-foreground group-hover:text-primary transition-colors" />
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}
