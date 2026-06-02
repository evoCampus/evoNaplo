import { Trash2 } from "lucide-react";
import { use } from "react";
import type { MentorDTO } from "../../api/api";

export default function MentorsList({
  mentorsPromise,
  onEdit,
  onDelete,
}: {
  mentorsPromise: Promise<MentorDTO[]>;
  onEdit: (mentor: MentorDTO) => void;
  onDelete: (id: string) => void;
}) {
  const mentors = use(mentorsPromise);

  return (
    <div className="grid gap-3">
      {mentors.map((mentor) => (
        <div
          key={mentor.id}
          className="flex items-center justify-between p-5 bg-card rounded-2xl border border-transparent hover:border-border/50 transition-all group shadow-sm hover:shadow-md cursor-pointer"
          onClick={() => onEdit(mentor)}
        >
          <div className="flex flex-col gap-1">
            <span className="text-lg font-medium text-foreground/90">{mentor.name || "Unknown"}</span>
            <span className="text-sm text-muted-foreground">{mentor.email}</span>
          </div>
          <button
            onClick={(e) => {
              e.stopPropagation();
              onDelete(mentor.id!);
            }}
            className="text-muted-foreground hover:text-destructive transition-colors p-2 rounded-full hover:bg-destructive/10 hover:cursor-pointer"
          >
            <Trash2 className="w-5 h-5" />
          </button>
        </div>
      ))}
      {mentors.length === 0 && (
         <div className="text-center p-8 text-muted-foreground">No mentors found.</div>
      )}
    </div>
  );
}
