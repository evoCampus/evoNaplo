import { Users2, ChevronRight } from "lucide-react";

const teams = [
  { name: "evoNapló", id: "1" },
  { name: "evoStory", id: "2" },
];

export default function MentorHomePage() {
  return (
    <div className="max-w-5xl mx-auto py-8">
      <h1 className="text-3xl font-bold mb-8 text-foreground">Welcome MentorName!</h1>

      <div className="space-y-6">
        <h2 className="text-xl font-semibold text-foreground">Your teams:</h2>

        <div className="grid gap-3">
          {teams.map((team) => (
            <div
              key={team.id}
              className="relative flex items-center justify-between p-5 bg-card rounded-2xl cursor-pointer hover:shadow-md transition-all group border border-transparent hover:border-border/50"
            >
              <div className="flex items-center gap-3">
                <div className="p-1.5 text-foreground/80">
                    <Users2 className="w-5 h-5" />
                </div>
                <span className="text-lg font-medium text-foreground/90">{team.name}</span>
              </div>
              <ChevronRight className="w-5 h-5 text-muted-foreground group-hover:text-primary transition-colors" />
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}
