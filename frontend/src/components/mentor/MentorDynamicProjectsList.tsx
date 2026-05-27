import { useLocation, Link } from "react-router";
import { use } from "react";
import { SidebarMenuItem, SidebarMenuButton, SidebarMenuSub, SidebarMenuSubItem, SidebarMenuSubButton } from "@evonaplo/ui-library";
import { FolderRoot, ChevronDown } from "lucide-react";
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
  const projects = use(projectsPromise);

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
