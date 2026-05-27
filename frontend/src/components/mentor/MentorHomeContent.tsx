import { Users2, ChevronRight } from "lucide-react";
import { use } from "react";
import { Link } from "react-router";
import { type MentorHomeData } from "../../types";

export default function MentorHomeContent({ dataPromise }: { dataPromise: Promise<MentorHomeData> }) {
  const { mentorName, teams } = use(dataPromise);

  return (
    <div className="max-w-5xl mx-auto py-8">
      <h1 className="text-3xl font-bold mb-8 text-foreground">Welcome {mentorName}!</h1>

      <div className="space-y-6">
        <h2 className="text-xl font-semibold text-foreground">Your teams:</h2>

        <div className="grid gap-3">
          {teams.length > 0 ? teams.map((team) => (
            <Link
              key={team.id}
              to={`/mentor/projects/${team.projectId}`}
              className="relative flex items-center justify-between p-5 bg-card rounded-2xl cursor-pointer hover:shadow-md transition-all group border border-transparent hover:border-border/50 shadow-sm"
            >
              <div className="flex items-center gap-3">
                <div className="p-2 text-foreground/80 bg-muted/50 rounded-xl">
                    <Users2 className="w-5 h-5 text-primary" />
                </div>
                <span className="text-lg font-medium text-foreground/90">{team.name}</span>
              </div>
              <ChevronRight className="w-5 h-5 text-muted-foreground group-hover:text-primary transition-colors" />
            </Link>
          )) : (
            <div className="p-8 text-center bg-card rounded-2xl border border-dashed border-border/60">
              <p className="text-muted-foreground">No teams assigned yet.</p>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
