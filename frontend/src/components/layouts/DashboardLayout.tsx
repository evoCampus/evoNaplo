import { Outlet } from "react-router";
import { SidebarProvider } from "@evonaplo/ui-library";
import { DashboardSidebar } from "../DashboardSidebar";
import { Navigate } from "react-router";
import { Suspense } from "react";
import { Loader2 } from "lucide-react";
import { useUser } from "../../hooks/use-user";

export default function DashboardLayout() {
    const { user } = useUser();

    if (!user) return <Navigate to="/" replace />;
    return (
        <SidebarProvider className="h-full">
            <DashboardSidebar />
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
