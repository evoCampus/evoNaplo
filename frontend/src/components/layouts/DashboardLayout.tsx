import { Outlet } from "react-router";
import { SidebarProvider } from "ui-library/components/ui/sidebar";
import { DashboardSidebar } from "../DashboardSidebar";
import { Navigate } from "react-router";
import type { User } from "../../types";

export default function DashboardLayout({ user }: { user: User }) {

    if (!user) return <Navigate to="/" replace />;
    return (
        <SidebarProvider className="h-full">
            <DashboardSidebar user={user} />
            <main className="flex-1 overflow-y-auto p-6">
                <Outlet />
            </main>
        </SidebarProvider>
    );
}
