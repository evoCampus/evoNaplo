import { Trash2 } from "lucide-react";
import { use } from "react";
import type { ProjectDTO } from "../../api/api";

export default function ProjectsList({
  projectsPromise,
  onEdit,
  onDelete,
}: {
  projectsPromise: Promise<ProjectDTO[]>;
  onEdit: (project: ProjectDTO) => void;
  onDelete: (id: string) => void;
}) {
  const projects = use(projectsPromise);

  return (
    <div className="grid gap-3">
      {projects.map((project) => (
        <div
          key={project.id}
          className="flex items-center justify-between p-5 bg-card rounded-2xl border border-transparent hover:border-border/50 transition-all group shadow-sm hover:shadow-md cursor-pointer"
          onClick={() => onEdit(project)}
        >
          <div className="flex flex-col gap-1">
            <span className="text-lg font-medium text-foreground/90">{project.name || "Unknown"}</span>
            <span className="text-sm text-muted-foreground line-clamp-1">{project.description}</span>
          </div>
          <button
            onClick={(e) => {
              e.stopPropagation();
              onDelete(project.id!);
            }}
            className="text-muted-foreground hover:text-destructive transition-colors p-2 rounded-full hover:bg-destructive/10 hover:cursor-pointer"
          >
            <Trash2 className="w-5 h-5" />
          </button>
        </div>
      ))}
      {projects.length === 0 && (
         <div className="text-center p-8 text-muted-foreground">No projects found.</div>
      )}
    </div>
  );
}
