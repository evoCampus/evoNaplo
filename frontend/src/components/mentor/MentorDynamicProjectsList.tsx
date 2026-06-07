import { useState, useEffect } from "react";
import { useLocation, Link } from "react-router";
import { SidebarMenuItem, SidebarMenuButton, SidebarMenuSub, SidebarMenuSubItem, SidebarMenuSubButton } from "@evonaplo/ui-library";
import { FolderRoot, ChevronDown, Loader2 } from "lucide-react";
import { type UIMentorProject } from "../../types";

export default function MentorDynamicProjectsList({
  projectsPromise,
  expandedProjects,
  onToggleProject
}: {
  projectsPromise: Promise<UIMentorProject[]>;
  expandedProjects: string[];
  onToggleProject: (projectName: string) => void;
}) {
  const location = useLocation();
  const [projects, setProjects] = useState<UIMentorProject[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    let cancelled = false;
    projectsPromise
      .then(result => {
        if (!cancelled) {
          setError(null);
          setProjects(result);
          setIsLoading(false);
        }
      })
      .catch(err => {
        if (!cancelled) {
          console.error("Failed to load projects:", err);
          setError("Failed to load projects.");
          setIsLoading(false);
        }
      });
    return () => { cancelled = true; };
  }, [projectsPromise]);

  if (isLoading) {
    return (
      <div className="flex items-center gap-2 p-2 px-4 text-sm text-muted-foreground">
        <Loader2 className="w-4 h-4 animate-spin" />
        <span>Loading projects...</span>
      </div>
    );
  }

  if (error) {
    return (
      <div className="px-4 py-2 text-sm text-destructive">{error}</div>
    );
  }

  if (projects.length === 0) {
    return <div className="px-4 py-2 text-sm text-muted-foreground">No assigned projects found.</div>;
  }

  return (
    <>
      {projects.map((project) => (
        <SidebarMenuItem key={project.id}>
          <SidebarMenuButton
            onClick={() => onToggleProject(project.name)}
            className="w-full justify-between"
            isActive={location.pathname.startsWith(`/mentor/projects/${project.id}`)}
          >
            <div className="flex items-center gap-2">
              <FolderRoot className="w-4 h-4" />
              <span>{project.name}</span>
            </div>
            <ChevronDown className={`w-3 h-3 transition-transform ${expandedProjects.includes(project.name) ? "rotate-180" : ""}`} />
          </SidebarMenuButton>

          {expandedProjects.includes(project.name) && (
            <SidebarMenuSub>
              {project.subItems.map((subItem) => (
                <SidebarMenuSubItem key={subItem.title}>
                  <SidebarMenuSubButton asChild isActive={location.pathname + location.hash === subItem.url}>
                    <Link to={subItem.url}>
                      <span>{subItem.title}</span>
                    </Link>
                  </SidebarMenuSubButton>
                </SidebarMenuSubItem>
              ))}
            </SidebarMenuSub>
          )}
        </SidebarMenuItem>
      ))}
    </>
  );
}
