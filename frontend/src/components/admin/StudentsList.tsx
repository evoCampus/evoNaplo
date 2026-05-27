import { Trash2 } from "lucide-react";
import { use } from "react";
import type { StudentDTO } from "../../api/api";

export default function StudentsList({
  studentsPromise,
  onEdit,
  onDelete,
}: {
  studentsPromise: Promise<StudentDTO[]>;
  onEdit: (student: StudentDTO) => void;
  onDelete: (id: string) => void;
}) {
  const students = use(studentsPromise);

  return (
    <div className="grid gap-3">
      {students.map((student) => (
        <div
          key={student.id}
          className="flex items-center justify-between p-5 bg-card rounded-2xl border border-transparent hover:border-border/50 transition-all group shadow-sm hover:shadow-md cursor-pointer"
          onClick={() => onEdit(student)}
        >
          <span className="text-lg font-medium text-foreground/90">{student.name || "Unknown"}</span>
          <button
            onClick={(e) => {
              e.stopPropagation();
              onDelete(student.id!);
            }}
            className="text-muted-foreground hover:text-destructive transition-colors p-2 rounded-full hover:bg-destructive/10"
          >
            <Trash2 className="w-5 h-5" />
          </button>
        </div>
      ))}
      {students.length === 0 && (
         <div className="text-center p-8 text-muted-foreground">No students found.</div>
      )}
    </div>
  );
}
