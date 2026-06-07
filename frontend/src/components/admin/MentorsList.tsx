import type { MentorDTO } from "../../api/api";
import { GenericEntityList } from "./GenericEntityList";

export default function MentorsList({
  mentorsPromise,
  onEdit,
  onDelete,
}: {
  mentorsPromise: Promise<MentorDTO[]>;
  onEdit: (mentor: MentorDTO) => void;
  onDelete: (id: string) => void;
}) {
  return (
    <GenericEntityList
      dataPromise={mentorsPromise}
      onEdit={onEdit}
      onDelete={onDelete}
      renderContent={(mentor) => (
        <div className="flex flex-col gap-1">
          <span className="text-lg font-medium text-foreground/90">{mentor.name ?? "Unknown"}</span>
          <span className="text-sm text-muted-foreground">{mentor.email ?? "No email"}</span>
        </div>
      )}
      emptyMessage="No mentors found."
    />
  );
}
