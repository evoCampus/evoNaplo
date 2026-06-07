import { useState, useEffect } from "react";
import { Users2, ChevronRight, Loader2, AlertCircle } from "lucide-react";
import { Link } from "react-router";
import { type MentorHomeData } from "../../types";

export default function MentorHomeContent({ dataPromise }: { dataPromise: Promise<MentorHomeData> }) {
  const [data, setData] = useState<MentorHomeData | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    let cancelled = false;
    dataPromise
      .then(result => {
        if (!cancelled) {
          setError(null);
          setData(result);
          setIsLoading(false);
        }
      })
      .catch(err => {
        if (!cancelled) {
          console.error("Failed to load home data:", err);
          setError("Failed to load dashboard. Please try again.");
          setIsLoading(false);
        }
      });
    return () => { cancelled = true; };
  }, [dataPromise]);

  if (isLoading) {
    return (
      <div className="flex flex-col items-center justify-center min-h-96 gap-4">
        <Loader2 className="w-8 h-8 animate-spin text-primary" />
        <p className="text-muted-foreground font-medium">Loading your dashboard...</p>
      </div>
    );
  }

  if (error) {
    return (
      <div className="flex flex-col items-center justify-center min-h-96 gap-4">
        <AlertCircle className="w-8 h-8 text-destructive" />
        <p className="text-destructive font-medium">{error}</p>
      </div>
    );
  }

  if (!data) return null;

  const { mentorName, teams } = data;

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
