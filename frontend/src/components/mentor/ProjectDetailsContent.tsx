import { useState, useEffect } from "react";
import { type ProjectDetailedData } from "../../types";
import { Button } from "@evonaplo/ui-library";
import { Plus, FileText, Users2, Loader2, AlertCircle } from "lucide-react";
import ProjectTeamCard from "./ProjectTeamCard";

export default function ProjectDetailsContent({
  dataPromise
}: {
  dataPromise: Promise<ProjectDetailedData>
}) {
  const [data, setData] = useState<ProjectDetailedData | null>(null);
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
          console.error("Failed to load project details:", err);
          setError("Failed to load project details. Please try again.");
          setIsLoading(false);
        }
      });
    return () => { cancelled = true; };
  }, [dataPromise]);

  if (isLoading) {
    return (
      <div className="flex flex-col items-center justify-center min-h-96 gap-4">
        <Loader2 className="w-8 h-8 animate-spin text-primary" />
        <p className="text-muted-foreground font-medium">Loading project details...</p>
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

  const { project, teams } = data;

  return (
    <div className="max-w-5xl mx-auto py-8 space-y-8">
      <div className="flex items-center justify-between">
        <h1 className="text-3xl font-bold text-foreground">{project.name}</h1>
        <Button className="bg-primary hover:bg-primary/90 text-primary-foreground rounded-lg h-9 px-4 shadow-sm">
          <Plus className="w-4 h-4 mr-1" />
          Add demo ppt
        </Button>
      </div>

      <div className="bg-card rounded-2xl p-6 space-y-4 border border-transparent hover:border-border/50 transition-all shadow-sm">
        <div className="flex items-center gap-3 text-foreground">
          <FileText className="w-6 h-6 text-primary" />
          <h2 className="text-xl font-bold tracking-tight">Project description</h2>
        </div>
        <p className="text-lg text-foreground/80 pl-9">
          {project.description || "No description available for this project."}
        </p>
      </div>

      {teams.length > 0 ? (
        teams.map((team) => (
          <ProjectTeamCard key={team.id} team={team} />
        ))
      ) : (
        <div className="bg-muted/30 rounded-2xl p-12 text-center border-2 border-dashed border-border">
          <Users2 className="w-12 h-12 mx-auto text-muted-foreground mb-4" />
          <h3 className="text-xl font-semibold text-foreground">No teams assigned</h3>
          <p className="text-muted-foreground mt-2">There are currently no teams working on this project.</p>
        </div>
      )}
    </div>
  );
}
