import type { ProjectDTO } from "../../api/api";
import { GenericEntityList } from "./GenericEntityList";

export default function ProjectsList({
  projectsPromise,
  onEdit,
  onDelete,
}: {
  projectsPromise: Promise<ProjectDTO[]>;
  onEdit: (project: ProjectDTO) => void;
  onDelete: (id: string) => void;
}) {
  return (
    <GenericEntityList
      dataPromise={projectsPromise}
      onEdit={onEdit}
      onDelete={onDelete}
      renderContent={(project) => (
        <div className="flex flex-col gap-1">
          <span className="text-lg font-medium text-foreground/90">{project.name || "Unknown"}</span>
          <span className="text-sm text-muted-foreground line-clamp-1">{project.description}</span>
        </div>
      )}
      emptyMessage="No projects found."
    />
  );
}
