import { CircleUser } from "lucide-react";
import { ThemeToggle } from "./ThemeToggle";

export function Navbar() {
  return (
    <nav className="h-14 border-b bg-secondary flex items-center px-4">
      <div className="flex w-full justify-between items-center">
        <div className="flex items-center">
          <a href="/" className="text-lg font-bold">
            Logo
          </a>
        </div>
        <div className="flex flex-row items-center gap-2">
          <a href="/" className="text-sm">
            <CircleUser className="w-6 h-6" />
          </a>
          <ThemeToggle />
        </div>
      </div>
    </nav>
  );
}
