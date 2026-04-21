import type { SidebarProps } from "../types";
import {
  Sidebar,
} from "@evonaplo/ui-library";

export function DashboardSidebar({ user }: SidebarProps) {
  return (
    <Sidebar>
        {user.role}
    </Sidebar>
  );
}
