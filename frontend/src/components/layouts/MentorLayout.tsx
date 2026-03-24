import { Outlet } from "react-router";
import { SidebarProvider } from "../ui/sidebar";
import { MentorSidebar } from "../MentorSidebar";

export default function MentorLayout() {
  return (
    <SidebarProvider className="h-full">
      <MentorSidebar />
      <main className="flex-1 overflow-y-auto p-6">
        <Outlet />
      </main>
    </SidebarProvider>
  );
}
