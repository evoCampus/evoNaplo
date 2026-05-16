import { Button } from "@evonaplo/ui-library";
import { Plus, SlidersHorizontal, Trash2 } from "lucide-react";

const students = [
  "Varga Eszter",
  "Gyulai Milán",
  "Vaszil Valentin",
  "Szabon Benjamin",
  "Pál Bence Marcell",
  "Stark Ádám",
  "Ruttkay Zoltán Gergő",
];

export default function StudentsPage() {
  return (
    <div className="max-w-6xl mx-auto py-4">
      <div className="flex items-center justify-between mb-8">
        <h1 className="text-3xl font-bold tracking-tight">Students</h1>
        <div className="flex items-center gap-6">
          <button className="flex items-center gap-2 text-muted-foreground hover:text-foreground transition-colors">
            <span className="text-sm font-medium">Filters</span>
            <SlidersHorizontal className="w-4 h-4" />
          </button>
          <Button className="bg-primary hover:bg-primary/90 text-primary-foreground px-5 h-10 rounded-lg">
            <Plus className="w-4 h-4 mr-1" />
            Add
          </Button>
        </div>
      </div>

      <div className="grid gap-3">
        {students.map((student) => (
          <div
            key={student}
            className="flex items-center justify-between p-5 bg-card rounded-2xl border border-transparent hover:border-border/50 transition-all group shadow-sm hover:shadow-md"
          >
            <span className="text-lg font-medium text-foreground/90">{student}</span>
            <button className="text-muted-foreground hover:text-destructive transition-colors p-2 rounded-full hover:bg-destructive/10">
              <Trash2 className="w-5 h-5" />
            </button>
          </div>
        ))}
      </div>
    </div>
  );
}
