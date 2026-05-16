import { Button } from "@evonaplo/ui-library";
import { 
  FileText, 
  Users2, 
  Calendar, 
  User, 
  Clock, 
  MapPin, 
  Plus 
} from "lucide-react";

export default function ProjectDetailsPage() {
  return (
    <div className="max-w-5xl mx-auto py-8 space-y-8">
      <div className="flex items-center justify-between">
        <h1 className="text-3xl font-bold text-foreground">evoNapló</h1>
        <Button className="bg-primary hover:bg-primary/90 text-primary-foreground rounded-lg h-9 px-4">
          <Plus className="w-4 h-4 mr-1" />
          Add demo ppt
        </Button>
      </div>

      {/* Project Description */}
      <div className="bg-card rounded-2xl p-6 space-y-4 shadow-sm border border-transparent hover:border-border/50 transition-all">
        <div className="flex items-center gap-3 text-foreground">
          <FileText className="w-6 h-6" />
          <h2 className="text-xl font-bold tracking-tight">Project description</h2>
        </div>
        <p className="text-lg text-foreground/80 pl-9">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit
        </p>
      </div>

      {/* Members */}
      <div className="bg-card rounded-2xl p-6 space-y-4 shadow-sm border border-transparent hover:border-border/50 transition-all">
        <div className="flex items-center gap-3 text-foreground">
          <Users2 className="w-6 h-6" />
          <h2 className="text-xl font-bold tracking-tight">Members</h2>
        </div>
        <div className="space-y-3 pl-9">
          <div className="flex items-center gap-3 text-foreground/90">
            <div className="bg-muted p-1 rounded-full">
              <User className="w-4 h-4" />
            </div>
            <span className="text-lg">Sándor József Benedek</span>
          </div>
          <div className="flex items-center gap-3 text-foreground/90">
            <div className="bg-muted p-1 rounded-full">
              <User className="w-4 h-4" />
            </div>
            <span className="text-lg">Nagy Gabriella</span>
          </div>
        </div>
      </div>

      {/* Team Meetings */}
      <div className="bg-card rounded-2xl p-6 space-y-4 shadow-sm border border-transparent hover:border-border/50 transition-all">
        <div className="flex items-center gap-3 text-foreground">
          <Calendar className="w-6 h-6" />
          <h2 className="text-xl font-bold tracking-tight">Team meetings</h2>
        </div>
        <div className="space-y-3 pl-9">
          <div className="flex items-center gap-3 text-foreground/80">
            <Clock className="w-4 h-4 text-muted-foreground" />
            <span className="text-lg">2026. 03. 25. 17:00</span>
          </div>
          <div className="flex items-center gap-3 text-foreground/80">
            <MapPin className="w-4 h-4 text-muted-foreground" />
            <span className="text-lg">Evosoft Miskolc</span>
          </div>
        </div>
      </div>
    </div>
  );
}
