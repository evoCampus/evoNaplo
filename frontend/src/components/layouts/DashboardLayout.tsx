import { Outlet } from "react-router";
import { SidebarProvider } from "@evonaplo/ui-library";
import { DashboardSidebar } from "../DashboardSidebar";
import { Navigate } from "react-router";
import type { User } from "../../types";
import { Suspense } from "react";
import { Loader2 } from "lucide-react";

export default function DashboardLayout({ user }: { user: User }) {

    if (!user) return <Navigate to="/" replace />;
    return (
        <SidebarProvider className="h-full">
            <DashboardSidebar user={user} />
            <main className="flex-1 overflow-y-auto p-6">
                <Suspense fallback={
                    <div className="flex items-center justify-center h-full">
                        <Loader2 className="w-10 h-10 animate-spin text-primary" />
                    </div>
                }>
                    <Outlet />
                </Suspense>
            </main>
        </SidebarProvider>
    );
}
