import type { SidebarProps } from "../types";
import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarGroup,
  SidebarGroupContent,
  SidebarGroupLabel,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
  Calendar as CalendarComponent
} from "@evonaplo/ui-library";
import { Link, useLocation } from "react-router";
import {
  Users,
  UserSquare2,
  FolderRoot,
  Users2,
  CalendarDays,
  Settings,
  LayoutDashboard,
  Calendar,
  Loader2
} from "lucide-react";
import { Suspense, useState, useTransition, useMemo } from "react";
import MentorDynamicProjectsList from "./mentor/MentorDynamicProjectsList";
import { type UIMentorProject } from "../types";
import { useApiClient } from "src/hooks/use-api-client";

export function DashboardSidebar({ user }: SidebarProps) {
  const location = useLocation();
  const apiClient = useApiClient();
  const [date, setDate] = useState<Date | undefined>(new Date());
  const [expandedProjects, setExpandedProjects] = useState<string[]>([]);
  const [,startTransition] = useTransition();

  const toggleProject = (projectName: string) => {
    startTransition(() => {
      setExpandedProjects(prev =>
        prev.includes(projectName)
          ? prev.filter(p => p !== projectName)
          : [...prev, projectName]
      );
    });
  };

  const projectsPromise = useMemo(() => {
    if (user.role !== "mentor") return Promise.resolve([]);

    return (async (): Promise<UIMentorProject[]> => {
      const { data: mentor } = await apiClient.mentors.apiMentorsIdGet(user.id);
      if (!mentor.projects || mentor.projects.length === 0) return [];

      const projectPromises = mentor.projects.map(async (projectId) => {
        const { data: project } = await apiClient.projects.apiProjectsIdGet(projectId);

        const mappedProject: UIMentorProject = {
          id: project.id || projectId,
          name: project.name || "Unknown Project",
          subItems: [
            { title: "Project description", url: `/mentor/projects/${project.id}#description` },
            { title: "Team members", url: `/mentor/projects/${project.id}#members` },
            { title: "Team meetings", url: `/mentor/projects/${project.id}#meetings` },
          ]
        };

        return mappedProject;
      });

      return Promise.all(projectPromises);
    })();
  }, [apiClient, user.id, user.role]);

  const adminItems = [
    { title: "Dashboard", url: "/admin", icon: LayoutDashboard },
    { title: "Students", url: "/admin/students", icon: Users },
    { title: "Mentors", url: "/admin/mentors", icon: UserSquare2 },
    { title: "Teams", url: "/admin/teams", icon: Users2 },
    { title: "Projects", url: "/admin/projects", icon: FolderRoot },
    { title: "Semesters", url: "/admin/semesters", icon: CalendarDays },
    { title: "Settings", url: "/admin/settings", icon: Settings },
  ];

  const mentorItems = [
    { title: "Dashboard", url: "/mentor", icon: LayoutDashboard },
    { title: "Upcoming Meetings", url: "/mentor/meetings", icon: Calendar },
  ];

  const mentorFooterItems = [
    { title: "Semesters", url: "/mentor/semesters", icon: CalendarDays },
    { title: "Settings", url: "/mentor/settings", icon: Settings },
  ];

  return (
    <Sidebar>
      <SidebarContent className="flex-1 overflow-y-auto scrollbar-thumb-accent scrollbar-auto">
        <SidebarGroup>
          <SidebarGroupLabel>{user.role.toUpperCase()} MENU</SidebarGroupLabel>
          <SidebarGroupContent>
            <SidebarMenu className="gap-1">
              {user.role === "admin" ? (
                adminItems.map((item) => (
                  <SidebarMenuItem key={item.title}>
                    <SidebarMenuButton asChild isActive={location.pathname === item.url}>
                      <Link to={item.url}>
                        <item.icon className="w-4 h-4" />
                        <span>{item.title}</span>
                      </Link>
                    </SidebarMenuButton>
                  </SidebarMenuItem>
                ))
              ) : (
                <>
                  {mentorItems.map((item) => (
                    <SidebarMenuItem key={item.title}>
                      <SidebarMenuButton asChild isActive={location.pathname === item.url}>
                        <Link to={item.url}>
                          <item.icon className="w-4 h-4" />
                          <span>{item.title}</span>
                        </Link>
                      </SidebarMenuButton>
                    </SidebarMenuItem>
                  ))}

                  <Suspense fallback={
                    <div className="flex items-center gap-2 p-2 px-4 text-sm text-muted-foreground">
                      <Loader2 className="w-4 h-4 animate-spin" />
                      <span>Loading projects...</span>
                    </div>
                  }>
                    <MentorDynamicProjectsList
                      projectsPromise={projectsPromise}
                      expandedProjects={expandedProjects}
                      onToggleProject={toggleProject}
                    />
                  </Suspense>

                  {mentorFooterItems.map((item) => (
                    <SidebarMenuItem key={item.title}>
                      <SidebarMenuButton asChild isActive={location.pathname === item.url}>
                        <Link to={item.url}>
                          <item.icon className="w-4 h-4" />
                          <span>{item.title}</span>
                        </Link>
                      </SidebarMenuButton>
                    </SidebarMenuItem>
                  ))}
                </>
              )}
            </SidebarMenu>
          </SidebarGroupContent>
        </SidebarGroup>
      </SidebarContent>

      <SidebarFooter className="p-4">
        <CalendarComponent
          mode="single"
          selected={date}
          onSelect={setDate}
          className="rounded-lg border w-full bg-card"
        />
      </SidebarFooter>
    </Sidebar>
  );
}