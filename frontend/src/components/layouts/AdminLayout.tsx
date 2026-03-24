import { Outlet } from "react-router";
import { SidebarProvider } from "../ui/sidebar";
import { AdminSidebar } from "../AdminSidebar";

export default function AdminLayout() {
  return (
    <SidebarProvider className="h-full">
      <AdminSidebar />
      <main className="flex-1 overflow-y-auto p-6">
        <Outlet />
      </main>
    </SidebarProvider>
  );
}
