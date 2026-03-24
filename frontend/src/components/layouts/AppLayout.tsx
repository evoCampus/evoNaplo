import { Outlet } from "react-router";
import { Navbar } from "../Navbar";
import { TooltipProvider } from "../ui/tooltip";
import { ThemeProvider } from "../ThemeProvider";

export default function AppLayout() {
  return (
    <ThemeProvider defaultTheme="light" storageKey="vite-ui-theme">
      <TooltipProvider>
        <div
          className="flex flex-col h-screen overflow-hidden"
          style={{ "--header-height": "3.5rem" } as React.CSSProperties}
        >
          <Navbar />
          <div className="flex flex-1 overflow-hidden">
            <Outlet />
          </div>
        </div>
      </TooltipProvider>
    </ThemeProvider>
  );
}
