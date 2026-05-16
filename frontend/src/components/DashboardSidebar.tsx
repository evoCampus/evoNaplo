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
  SidebarMenuSub,
  SidebarMenuSubButton,
  SidebarMenuSubItem,
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
  ChevronDown
} from "lucide-react"
import { useState } from "react";

export function DashboardSidebar({ user }: SidebarProps) {
    const location = useLocation();
    const [date, setDate] = useState<Date | undefined>(new Date());
    const [expandedTeams, setExpandedTeams] = useState<string[]>(["evoNapló"]);

    const toggleTeam = (teamName: string) => {
      setExpandedTeams(prev =>
        prev.includes(teamName)
          ? prev.filter(t => t !== teamName)
          : [...prev, teamName]
      );
    };

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
    { title: "Upcoming Meetings", url: "/mentor/meetings", icon: Calendar },
  ];

  const teams = [
    {
      name: "evoNapló",
      id: "1",
      subItems: [
        { title: "Project description", url: "/mentor/projects/1#description" },
        { title: "Team members", url: "/mentor/projects/1#members" },
        { title: "Team meetings", url: "/mentor/projects/1#meetings" },
      ]
    },
    {
      name: "evoStory",
      id: "2",
      subItems: [
        { title: "Project description", url: "/mentor/projects/2#description" },
        { title: "Team members", url: "/mentor/projects/2#members" },
        { title: "Team meetings", url: "/mentor/projects/2#meetings" },
      ]
    },
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

                  {teams.map((team) => (
                    <SidebarMenuItem key={team.name}>
                      <SidebarMenuButton
                        onClick={() => toggleTeam(team.name)}
                        className="w-full justify-between"
                        isActive={location.pathname.startsWith(`/mentor/projects/${team.id}`)}
                      >
                        <div className="flex items-center gap-2">
                          <Users2 className="w-4 h-4" />
                          <span>{team.name}</span>
                        </div>
                        <ChevronDown className={`w-3 h-3 transition-transform ${expandedTeams.includes(team.name) ? "rotate-180" : ""}`} />
                      </SidebarMenuButton>
                      {expandedTeams.includes(team.name) && (
                        <SidebarMenuSub>
                          {team.subItems.map((subItem) => (
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
