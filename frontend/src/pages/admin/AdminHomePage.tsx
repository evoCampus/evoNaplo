import { Search, Plus, Download, Upload } from "lucide-react";
import { Button, Input } from "@evonaplo/ui-library";
import { useNavigate } from "react-router";

export default function AdminHomePage() {
  const navigate = useNavigate();

  return (
    <div className="max-w-4xl mx-auto py-10 space-y-12">
      {/* Search Bar */}
      <div className="relative w-full">
        <Search className="absolute left-3 top-1/2 -translate-y-1/2 h-4 w-4 text-muted-foreground" />
        <Input 
          placeholder="Search students, mentors, teams, semesters..." 
          className="pl-10 h-12 bg-muted/50 border-none rounded-full text-lg focus-visible:ring-1 focus-visible:ring-primary"
        />
      </div>

      {/* Quick Actions */}
      <div className="space-y-6">
        <h2 className="text-2xl font-semibold tracking-tight">Quick Actions</h2>
        <div className="flex flex-col gap-3 max-w-md">
          <Button 
            variant="secondary" 
            className="justify-start h-12 text-lg font-medium bg-muted/40 hover:bg-muted/60"
            onClick={() => navigate("/admin/students")}
          >
            <Plus className="mr-3 h-5 w-5" /> Add Student
          </Button>
          <Button 
            variant="secondary" 
            className="justify-start h-12 text-lg font-medium bg-muted/40 hover:bg-muted/60"
            onClick={() => navigate("/admin/mentors")}
          >
            <Plus className="mr-3 h-5 w-5" /> Add Mentor
          </Button>
          <Button 
            variant="secondary" 
            className="justify-start h-12 text-lg font-medium bg-muted/40 hover:bg-muted/60"
            onClick={() => navigate("/admin/teams")}
          >
            <Plus className="mr-3 h-5 w-5" /> Create Team
          </Button>
          <Button 
            variant="secondary" 
            className="justify-start h-12 text-lg font-medium bg-muted/40 hover:bg-muted/60"
            onClick={() => navigate("/admin/projects")}
          >
            <Plus className="mr-3 h-5 w-5" /> Add Project
          </Button>
          <Button 
            variant="secondary" 
            className="justify-start h-12 text-lg font-medium bg-muted/40 hover:bg-muted/60"
            onClick={() => navigate("/admin/semesters")}
          >
            <Plus className="mr-3 h-5 w-5" /> Add Semester
          </Button>
        </div>
      </div>

      {/* Data Import/Export */}
      <div className="space-y-6">
        <h2 className="text-2xl font-semibold tracking-tight">Data Import/Export</h2>
        <div className="grid grid-cols-2 gap-4">
          <Button 
            variant="secondary" 
            className="h-12 text-lg font-medium bg-muted/40 hover:bg-muted/60"
          >
            <Download className="mr-3 h-5 w-5" /> Import from XLSX
          </Button>
          <Button 
            variant="secondary" 
            className="h-12 text-lg font-medium bg-muted/40 hover:bg-muted/60"
          >
            <Upload className="mr-3 h-5 w-5" /> Export to XLSX
          </Button>
        </div>
      </div>
    </div>
  );
}
