import type { SidebarProps } from "../types";
import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarGroup,
  SidebarGroupContent,
  SidebarGroupLabel,
  SidebarHeader,
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
  Calendar
} from "lucide-react"
import { useState } from "react";

export function DashboardSidebar({ user }: SidebarProps) {
    const location = useLocation();
    const [date, setDate] = useState<Date | undefined>(new Date());

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
    { title: "Semesters", url: "/mentor/semesters", icon: CalendarDays },
    { title: "Settings", url: "/mentor/settings", icon: Settings },
  ];

  const items = user.role === "admin" ? adminItems : mentorItems;

  return (
    <Sidebar>
      <SidebarHeader className="p-4">
        <div className="flex items-center gap-2 font-bold text-xl">
          <div className="w-8 h-8 bg-primary rounded flex items-center justify-center text-primary-foreground">
            E
          </div>
          evoNaplo
        </div>
      </SidebarHeader>
      <SidebarContent>
        <SidebarGroup>
          <SidebarGroupLabel>{user.role.toUpperCase()} MENU</SidebarGroupLabel>
          <SidebarGroupContent>
            <SidebarMenu>
              {items.map((item) => (
                <SidebarMenuItem key={item.title}>
                  <SidebarMenuButton asChild isActive={location.pathname === item.url}>
                    <Link to={item.url}>
                      <item.icon className="w-4 h-4" />
                      <span>{item.title}</span>
                    </Link>
                  </SidebarMenuButton>
                </SidebarMenuItem>
              ))}
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
