import type { SidebarProps } from "../types";
import {
  Sidebar,
} from "ui-library/components/ui/sidebar";

export function DashboardSidebar({ user }: SidebarProps) {
  return (
    <Sidebar>
        {user.role}
    </Sidebar>
  );
}
